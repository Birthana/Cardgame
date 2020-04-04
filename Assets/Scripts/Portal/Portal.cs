using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // No onenable, because portals can either be added to the friendly or enemy side. It is up to
    // the creator of the portal to determine that it gets properly added.

    private Renderer rend;
    private Vector3 ogScale;
    private float maxBrightness;
    const int MAX_INTENSITY = 4;
    // What percentage of the original scale to take away for each step down in intensity.
    const float SCALE_STEP = 0.05f;
    // What percentage of the original brightness to take away for each step down in intensity.
    const float BRIGHTNESS_STEP = 0.1f;
    const float MAX_SPEED = 2.0f;
    // How much to reduce the speed by for each step down in intensity (amount, not percentage.)
    const float SPEED_STEP = 0.25f;
    private float intensity = 0;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        ogScale = transform.localScale;
        maxBrightness = rend.material.GetFloat("_Brightness");
        UpdateAppearence(1.0f);
        StartCoroutine(IntroAnimation());
    }

    private IEnumerator IntroAnimation()
    {
        float extraIntensity = 1.0f;
        float progress = 0.0f;
        while (extraIntensity > 0.01f)
        {
            UpdateAppearence(extraIntensity);
            yield return null;
            progress += Time.deltaTime;
            extraIntensity = Mathf.Pow(40, -progress * 3);
        }
        UpdateAppearence(0);
        yield break;
    }

    private IEnumerator UpgradeAnimation() 
    {
        float extraIntensity = 1.0f;
        float progress = 0.0f;
        float startingIntensity = intensity;
        while (extraIntensity > 0.01f)
        {
            UpdateAppearence(extraIntensity * 0.3f);
            yield return null;
            progress += Time.deltaTime;
            extraIntensity = Mathf.Pow(40, -progress * 3);
            intensity = Mathf.Lerp(startingIntensity + 1, startingIntensity, extraIntensity);
        }
        UpdateAppearence(0);
        yield break;
    }

    public IEnumerator Upgrade()
    {
        yield return UpgradeAnimation();
        yield return new WaitForSeconds(0.3f);
    }

    public int GetIntensity()
    {
        return Mathf.RoundToInt(intensity);
    }

    public bool CanBeUpgraded()
    {
        return GetIntensity() != MAX_INTENSITY;
    }

    private void UpdateAppearence(float extraIntensity)
    {
        float reduction = MAX_INTENSITY - intensity;
        transform.localScale = ogScale * (1 - SCALE_STEP * reduction + extraIntensity * 0.3f);
        rend.material.SetFloat("_Brightness", maxBrightness * (1 - BRIGHTNESS_STEP * reduction + extraIntensity * 2));
        rend.material.SetFloat("_Speed", MAX_SPEED - SPEED_STEP * reduction);
        rend.material.SetFloat("_Intensity", intensity);
        GlobalVFX.instance.TooBright(extraIntensity);
    }

    void OnDisable()
    {
        // This line causes bugs when the game is shut down.
        // BattleManager.instance.RemovePortal(this);
    }
}
