using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : Card
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AddToIndex()
    {
        // Add an instance of this card to the card index so that it shows up in the game.
        CardIndex.instance.Add(ScriptableObject.CreateInstance<Defend>());
    }

    public Defend()
    {
        title = "Defend";
        level = 1;
        effectText = "Gain 6 block.";
        targetMode = TargetMode.Player;
        art = "Defend";
    }

    protected override IEnumerator Play(ActionContext context)
    {
        context.targets[0].AddBlock(6);
        yield return new WaitForSeconds(0.5f);
    }
}