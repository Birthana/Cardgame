using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://forum.unity.com/threads/how-to-create-your-own-c-script-template.459977/#post-5365599
public class Parry : Card
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AddToIndex()
    {
        // Add an instance of this card to the card index so that it shows up in the game.
        CardIndex.instance.Add(ScriptableObject.CreateInstance<Parry>());
    }

    public Parry()
    {
        title = "Parry"; // Name of the card.
        level = 1; // Amount of energy required to play the card.
        effectText = "Gain 6 block.\nReduce incoming damage by 25%."; // Text shown on the lower half of the card.
        targetMode = TargetMode.Player; // What the card should target (player, enemy, all enemies, etc.)
    }

    /// This is called when the card is played. Use it to apply the effects of the card. The
    /// variable "context" contains many useful fields and functions, view the documentation for
    /// ActionContext to find out about them.
    protected override IEnumerator Play(ActionContext context)
    {
        context.targets[0].AddBlock(6);
        yield return new WaitForSeconds(0.5f);
        context.targets[0].ModifySEIncomingDamageMultiplier(-0.25f);
        yield return new WaitForSeconds(0.5f);
    }
}