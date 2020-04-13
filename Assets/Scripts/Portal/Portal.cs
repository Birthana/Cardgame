using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Portal : MonoBehaviour
{
    // No onenable, because portals can either be added to the friendly or enemy side. It is up to
    // the creator of the portal to determine that it gets properly added.
    public TextMeshPro amountText, rateText;

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
    private float intensity = 1;
    private int amount = 0;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        ogScale = transform.localScale;
        maxBrightness = rend.material.GetFloat("_Brightness");
        UpdateAppearence(1.0f);
        StartCoroutine(IntroAnimation());
        amountText.text = "" + amount;
        rateText.text = "+" + GetRate();
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
            UpdateAppearence(extraIntensity * 0.6f);
            yield return null;
            progress += Time.deltaTime;
            extraIntensity = Mathf.Pow(40, -progress * 2);
            intensity = Mathf.Lerp(startingIntensity + 1, startingIntensity, extraIntensity);
            intensity = Mathf.Min(intensity, MAX_INTENSITY);
        }
        UpdateAppearence(0);
        yield break;
    }

    public IEnumerator Upgrade()
    {
        intensity += 1;
        rateText.text = "+" + GetRate();
        intensity -= 1;
        yield return UpgradeAnimation();
        yield return new WaitForSeconds(0.2f);
    }

    private int GetRate()
    {
        return 5 * GetIntensity();
    }

    public IEnumerator StartTurn()
    {
        amount += GetRate();
        amountText.text = "" + amount;
        float bright = 0.3f;
        float progress = 0.0f;
        while (bright > 0.01f)
        {
            bright = Mathf.Pow(20, -progress * 2) * 0.3f;
            GlobalVFX.instance.TooBright(bright);
            yield return null;
            progress += Time.deltaTime;
        }
            GlobalVFX.instance.TooBright(0);
        yield return new WaitForSeconds(0.2f);
    }

    public int GetIntensity()
    {
        return Mathf.RoundToInt(intensity);
    }

    public int GetAmount()
    {
        return amount;
    }

    public void ReduceAmount(int delta)
    {
        amount -= delta;
        amountText.text = "" + amount;
        if (amount < 0)
        {
            Debug.LogError("Portal has negative amount after ReduceAmount() was called!");
        }
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
}
