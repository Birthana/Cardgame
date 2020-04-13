using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : Card
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AddToIndex()
    {
        // Add an instance of this card to the card index so that it shows up in the game.
        CardIndex.instance.Add(ScriptableObject.CreateInstance<Bolt>());
    }

    public Bolt()
    {
        title = "Bolt"; // Name of the card.
        level = 0; // Amount of energy required to play the card.
        magicCost = 5;
        effectText = "Deal 6 damage."; // Text shown on the lower half of the card.
        targetMode = TargetMode.SpecificEnemy; // What the card should target (player, enemy, all enemies, etc.)
        // art = "Bolt"; // The name of a sprite in Assets/Resources/CardArt/
    }

    /// This is called when the card is played. Use it to apply the effects of the card. The
    /// variable "context" contains many useful fields and functions, view the documentation for
    /// ActionContext to find out about them.
    protected override IEnumerator Play(ActionContext context)
    {
        Player.instance.TriggerAttackAnim();
        context.targets[0].TriggerDamagedAnim();
        context.targets[0].TakeDamage(context.ComputeDamage(6));
        yield return new WaitForSeconds(0.5f);
    }
}