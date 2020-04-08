using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Card
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AddToIndex()
    {
        // Add an instance of this card to the card index so that it shows up in the game.
        CardIndex.instance.Add(ScriptableObject.CreateInstance<Attack>());
    }

    public Attack()
    {
        title = "Attack";
        level = 1;
        effectText = "Deal 8 damage.";
        targetMode = TargetMode.SpecificEnemy;
    }

    protected override IEnumerator Play(ActionContext context)
    {
        Player.instance.TriggerAttackAnim();
        context.targets[0].TakeDamage(context.ComputeDamage(8));
        context.targets[0].TriggerDamagedAnim();
        yield return new WaitForSeconds(0.5f);
    }
}