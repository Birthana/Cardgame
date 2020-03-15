using System.Collections;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public int strength = 5;

    protected override void UpdateActionIndicator(ActionIndicator indicator, ActionContext context)
    {
        indicator.ShowAttack(context.ComputeDamage(strength));
    }

    protected override void DoAttack(ActionContext context)
    {
        // Simply deal [strenth] damage to the target.
        context.targets[0].TakeDamage(context.ComputeDamage(strength));
    }
}
