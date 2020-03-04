using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public ParticleSystem controlling;
    public const float emissionRate = 200.0f;

    private List<ParticleSystem.EmitParams> emitters = new List<ParticleSystem.EmitParams>();
    private float angle = 0.0f;
    private float orbitCycle = 0.0f;
    private float emissionRemainder = 3.0f;

    void Start()
    {
        for (int i = 0; i < 4; i++) {
            var emitter = new ParticleSystem.EmitParams();
            emitter.applyShapeToPosition = true;
            if (i % 2 == 0) {
                emitter.velocity = new Vector3(10.0f, 0.0f, 0.0f);
            } else {
                emitter.velocity = new Vector3(-10.0f, 0.0f, 0.0f);
            }
            emitters.Add(emitter);
        }
        controlling.Play();
    }

    void Update()
    {
        var dt = Time.deltaTime;
        var main = controlling.main;
        var shape = controlling.shape;
        var origin = shape.position;
        var velocityOverTime = controlling.velocityOverLifetime;

        angle += dt * 0.0f;
        if (angle > Mathf.PI * 2.0f) {
            // Keep it small and managable.
            angle -= Mathf.PI * 2.0f;
        }
        orbitCycle += dt * 7.0f;
        if (orbitCycle > Mathf.PI * 2.0f) {
            // Keep it small and managable.
            orbitCycle -= Mathf.PI * 2.0f;
        }

        velocityOverTime.orbitalXMultiplier = Mathf.Sin(orbitCycle) * 10.0f + 30.0f;

        float newParticles = emissionRate * dt + emissionRemainder;
        emissionRemainder = newParticles % 1.0f;
    }
}
