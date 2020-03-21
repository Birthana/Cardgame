using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShakeAnim : ActionTargetAnimation
{
    public GameObject targetMountPoint = null;
    public float delayBefore = 0.1f;
    public float delayAfter = 0.0f;
    [Tooltip("How many seconds the animation should last for.")]
    public float time = 0.5f;
    [Tooltip("How many shakes to complete during the animation.")]
    public int numShakes = 4;
    [Tooltip("The back-and-forth distance to be covered during each shake.")]
    public float amplitude = 5;

    private float progress = 0.0f;

    protected override GameObject GetTargetMountPoint()
    {
        return targetMountPoint;
    }

    public override bool AnimationComplete()
    {
        return progress >= time;
    }

    void Update()
    {
        progress += Time.deltaTime;
        var pos = targetMountPoint.transform.localPosition;
        if (progress >= delayBefore + time)
        {
            pos.x = 0.0f;
        }
        else if (progress >= delayBefore)
        {
            pos.x = amplitude * Mathf.Sin(
                (progress - delayBefore) / time * numShakes * Mathf.PI * 2
            );
        }
        targetMountPoint.transform.localPosition = pos;
    }
}
