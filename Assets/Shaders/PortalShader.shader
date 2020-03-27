Shader "Unlit/PortalShader"
{
    Properties
    {
        _MainTex ("Pattern", 2D) = "white" {}
        _NoiseTex ("Noise", 2D) = "white" {}
        _FgColor ("FG Color", Color) = (1, 0.1, 0.05)
        _BgTint ("BG Tint", Color) = (1.0, 0.9, 0.8)
        _Speed ("Speed", Range(0, 5.0)) = 1.0
        _Intensity ("Intensity", Range(0, 4)) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 100
             
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 ogPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;
            fixed3 _FgColor;
            fixed3 _BgTint;
            float _Speed;
            float _Intensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.ogPos = v.vertex.xz;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float map11to01(float value) {
                return (value * 0.5 + 0.5);
            }

            float sin01(float angle) {
                return map11to01(sin(angle));
            }

            float intensity0(float radialPosition, float phase) {
                float amplitude = sin01(radialPosition * 3 - phase * 2) * 0.05;
                float offset = sin(radialPosition * 12 + phase * 3);
                return amplitude * offset;
            }
            float intensity1(float radialPosition, float phase) {
                float amplitude = sin01(radialPosition * 3 - phase * 2);
                amplitude = lerp(0.2, 1, amplitude) * 0.1;
                float offset = sin(radialPosition * 20 + phase * 10);
                return amplitude * offset;
            }
            float intensity2(float radialPosition, float phase) {
                float amplitude = sin01(radialPosition * 3 - phase * 2);
                amplitude = lerp(0.2, 1, amplitude) * 0.2;
                float offset = sin(radialPosition * 10 + phase * 3) * sin(radialPosition * 20 - phase * 3);
                return amplitude * offset;
            }
            float intensity3(float radialPosition, float phase) {
                float amplitude = sin01(radialPosition * 2 - phase * 2);
                amplitude = lerp(0.4, 1, amplitude) * 0.4;
                float offset = sin(radialPosition * 4 + phase * 2) * sin(radialPosition * 8 + phase * 3) * sin(radialPosition * 20 - phase * 2);
                return amplitude * offset;
            }
            float intensity4(float radialPosition, float phase) {
                float amplitude = 0.5;
                float offset = sin(radialPosition * 3 + phase * 2) * sin(radialPosition * 5 - phase * 2) * sin(radialPosition * 9 - phase * 2);
                offset += sin(radialPosition * 60) * saturate(offset + 0.4) * 0.2;
                return amplitude * offset;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float brightnessAmount = 1;
                float transparency = 1;

                // Pick a random offset to be used for a variety of things.
                float noiseLookupOffset = _Time.g * _Speed * 0.01;
                float2 posOffset = tex2D(_NoiseTex, i.ogPos / 30 + noiseLookupOffset * float2(2, 0)).rg - float2(0.5, 0.5);
                float2 detailedPosOffset = tex2D(_NoiseTex, i.ogPos / 10 + noiseLookupOffset * float2(-9, 9)).rg - float2(0.5, 0.5);

                // Apply the WHEE texture.
                float2 uvOffset = float2(_Time.g, _Time.g) * float2(-0.1, -0.1) * _Speed + posOffset * 0.2;
                brightnessAmount *= tex2D(_MainTex, i.uv + uvOffset).a;

                // Find out the distance from the center.
                float dist = length(i.ogPos + detailedPosOffset * 0.5) - 1.4;
                // Get brighter towards the center.
                brightnessAmount = lerp(1, brightnessAmount, saturate(dist + 0.3));
                transparency = pow(lerp(transparency, 0, saturate(-dist * 1)), 3);

                // Get brighter towards the edge.
                dist = 3.0 - length(i.ogPos);
                float radialPosition = atan2(i.ogPos.y, i.ogPos.x);
                float phase = _Time.g * _Speed;
                // Different edge patterns for different intensities.
                if (_Intensity < 1) {
                    dist += lerp(intensity0(radialPosition, phase), intensity1(radialPosition, phase), _Intensity - 0);
                } else if (_Intensity < 2) {
                    dist += lerp(intensity1(radialPosition, phase), intensity2(radialPosition, phase), _Intensity - 1);
                } else if (_Intensity < 3) {
                    dist += lerp(intensity2(radialPosition, phase), intensity3(radialPosition, phase), _Intensity - 2);
                } else if (_Intensity < 4) {
                    dist += lerp(intensity3(radialPosition, phase), intensity4(radialPosition, phase), _Intensity - 3);
                } else {
                    dist += intensity4(radialPosition, phase);
                }

                brightnessAmount = lerp(1, brightnessAmount, pow(saturate(dist / 1), 1));
                if (dist < 0) transparency = 0;

                fixed3 color = lerp(
                    _FgColor * 3 * _BgTint,
                    _FgColor * 6,
                    brightnessAmount
                );
                return fixed4(color, transparency);
                // return fixed4(detailedPosOffset, 1, 1);
            }
            ENDCG
        }
    }
}
