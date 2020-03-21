using System.Collections;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public int strength = 5;
    public ActionTargetAnimation attackAnimation = null;

    private const float ANIM_TIME = 0.4f;
    private float animProgress = 0;
    private Vector3 animBase;
    private bool animate = false;

    protected override void UpdateActionIndicator(ActionIndicator indicator, ActionContext context)
    {
        indicator.ShowAttack(context.ComputeDamage(strength));
    }

    protected override ActionTargetAnimation DoAttack(ActionContext context)
    {
        // Simply deal [strenth] damage to the target.
        context.targets[0].TakeDamage(context.ComputeDamage(strength));
        animProgress = 0;
        animBase = transform.localPosition;
        animate = true;
        return attackAnimation;
    }

    void Update()
    {
        if (animate)
        {
            animProgress += Time.deltaTime;
            if (animProgress < ANIM_TIME)
            {
                var offset = new Vector3((animProgress - ANIM_TIME) / ANIM_TIME, 0, 0);
                transform.localPosition = animBase + offset;
            }
            else
            {
                animate = false;
                transform.localPosition = animBase;
            }
        }
    }
}
