using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Card
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AddToIndex()
    {
        // Add an instance of this card to the card index so that it shows up in the game.
        CardIndex.instance.Add(ScriptableObject.CreateInstance<BlackHole>());
    }

    public BlackHole()
    {
        title = "Black Hole"; // Name of the card.
        level = 2; // Amount of energy required to play the card.
        magicCost = Card.ANY_MAGIC_COST;
        effectText = "Deal X damage to EVERY enemy."; // Text shown on the lower half of the card.
        targetMode = TargetMode.AllEnemies; // What the card should target (player, enemy, all enemies, etc.)
        // art = "BlackHole"; // The name of a sprite in Assets/Resources/CardArt/
    }

    /// This is called when the card is played. Use it to apply the effects of the card. The
    /// variable "context" contains many useful fields and functions, view the documentation for
    /// ActionContext to find out about them.
    protected override IEnumerator Play(ActionContext context)
    {
        Player.instance.TriggerAttackAnim();
        int damageAmount = context.magicUsed;
        foreach (Enemy target in context.targets)
        {
            target.TriggerDamagedAnim();
            target.TakeDamage(context.ComputeDamage(damageAmount));
        }
        yield return new WaitForSeconds(0.5f);
    }
}