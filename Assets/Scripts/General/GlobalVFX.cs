using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Rendering.PostProcessing;

public class GlobalVFX : MonoBehaviour
{
    private static GlobalVFX _instance = null;
    public static GlobalVFX instance { get => _instance; }

    public PostProcessVolume targetVolume;

    private ChromaticAberration aberrationLayer;
    private ColorGrading colorGradingLayer;

    void Start()
    {
        targetVolume.profile.TryGetSettings(out aberrationLayer);
        targetVolume.profile.TryGetSettings(out colorGradingLayer);
    }

    void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("More than one object was instantiated with the GlobalVFX behavior!");
            Destroy(this);
        }
    }

    void OnDisable()
    {
        _instance = null;
    }

    /// Dims everything and applies some chromatic aberrition. This is used when portals are opened
    /// to make them appear so bright that they briefly overwhelm everything else. Strength goes 
    /// from zero to one.
    public void TooBright(float strength)
    {
        aberrationLayer.intensity.value = strength * 0.6f;
        colorGradingLayer.brightness.value = strength * -30;
        colorGradingLayer.contrast.value = strength * 40;
    }
}
