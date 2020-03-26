using System.Collections;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public int strength = 5;

    protected override void UpdateActionIndicator(ActionIndicator indicator, ActionContext context)
    {
        indicator.ShowAttack(context.ComputeDamage(strength));
    }

    protected override IEnumerator DoAttack(ActionContext context)
    {
        // Simply deal [strenth] damage to the target.
        context.targets[0].TakeDamage(context.ComputeDamage(strength));
        context.targets[0].TriggerDamagedAnim();
        TriggerAttackAnim();
        // Fixed delay to wait for animations.
        yield return new WaitForSeconds(0.5f);
    }
}
