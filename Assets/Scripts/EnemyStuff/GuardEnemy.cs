using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemy : Enemy
{
    public int strength = 20;
    public enum BattleState { ATTACK, DEFEND, STATUS }
    private BattleState currentState = BattleState.STATUS;
    private int attackDamage;
    private int blockAmount;

    protected override void UpdateActionIndicator(ActionIndicator indicator, ActionContext context)
    {
        if (currentState == BattleState.ATTACK)
        {
            attackDamage = 8;
            blockAmount = 5;
            indicator.ShowAttack(context.ComputeDamage(attackDamage));
        }
        else if (currentState == BattleState.DEFEND)
        {
            indicator.ShowBlock(strength);
        }
        else if (currentState == BattleState.STATUS)
        {
            indicator.ShowStatus();
        }
    }

    protected override IEnumerator DoAttack(ActionContext context)
    {
        if (currentState == BattleState.ATTACK)
        {
            currentState = (Random.Range(0, 2) == 0) ? BattleState.STATUS : BattleState.DEFEND;
            context.targets[0].TakeDamage(context.ComputeDamage(attackDamage));
            context.targets[0].TriggerDamagedAnim();
            TriggerAttackAnim();
            this.AddBlock(blockAmount);
        }
        else if (currentState == BattleState.DEFEND)
        {
            currentState = BattleState.ATTACK;
            this.AddBlock(strength);
        }
        else if (currentState == BattleState.STATUS)
        {
            currentState = (Random.Range(0, 2) == 0) ? BattleState.ATTACK : BattleState.DEFEND;
            context.targets[0].ModifySEOutgoingDamageMultiplier(-0.50f, 3);
            context.targets[0].ModifySEIncomingDamageMultiplier(0.50f, 3);
            BattleManager.instance.friendlyPortal?.ReduceAmount(5);
        }
        yield return new WaitForSeconds(0.5f);
    }
}
