using System.Collections;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public int strength = 5;
    private bool attacking = true;

    protected override void UpdateActionIndicator(ActionIndicator indicator, ActionContext context)
    {
        if (attacking)
        {
            indicator.ShowAttack(context.ComputeDamage(strength));
        }
        else
        {
            indicator.ShowBlock(strength);
        }
    }

    protected override IEnumerator DoAttack(ActionContext context)
    {
        if (attacking)
        {
            attacking = Random.value < 0.5f; // 50% chance to block next turn.
            // Simply deal [strenth] damage to the target.
            context.targets[0].TakeDamage(context.ComputeDamage(strength));
            context.targets[0].TriggerDamagedAnim();
            TriggerAttackAnim();
        }
        else
        {
            attacking = true; // Attack next turn.
            this.AddBlock(strength);
        }
        // Fixed delay to wait for animations.
        yield return new WaitForSeconds(0.5f);
    }
}
