using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://forum.unity.com/threads/how-to-create-your-own-c-script-template.459977/#post-5365599
public class ShieldSlam : Card
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AddToIndex()
    {
        // Add an instance of this card to the card index so that it shows up in the game.
        CardIndex.instance.Add(ScriptableObject.CreateInstance<ShieldSlam>());
    }

    public ShieldSlam()
    {
        title = "Shield Slam"; // Name of the card.
        level = 1; // Amount of energy required to play the card.
        effectText = "Deal 9 damage.\nReduce incoming damage by 25%."; // Text shown on the lower half of the card.
        targetMode = TargetMode.SpecificEnemy; // What the card should target (player, enemy, all enemies, etc.)
        //art = "ShieldSlam"; // The name of a sprite in Assets/Resources/CardArt/
    }

    /// This is called when the card is played. Use it to apply the effects of the card. The
    /// variable "context" contains many useful fields and functions, view the documentation for
    /// ActionContext to find out about them.
    protected override IEnumerator Play(ActionContext context)
    {
        // You can use yield return to spread out effects over time or wait for things to happen
        // before continuing.
        Player.instance.TriggerAttackAnim();
        context.targets[0].TriggerDamagedAnim();
        context.targets[0].TakeDamage(context.ComputeDamage(9));
        yield return new WaitForSeconds(0.5f);
        ActionContext playerContext = new ActionContext(Player.instance);
        Player.instance.ModifyActionContextAsSource(playerContext);
        playerContext.targets[0].ModifySEIncomingDamageMultiplier(-0.25f);
    }
}