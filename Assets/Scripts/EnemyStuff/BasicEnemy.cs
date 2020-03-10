using System.Collections;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public int strength = 5;

    public override void DoAttack(ActionContext context)
    {
        // Simply deal [strenth] damage to the target.
        context.targets[0].TakeDamage(strength);
    }
}
