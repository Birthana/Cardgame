﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://forum.unity.com/threads/how-to-create-your-own-c-script-template.459977/#post-5365599
public class Breakthrough : Card
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AddToIndex()
    {
        // Add an instance of this card to the card index so that it shows up in the game.
        CardIndex.instance.Add(ScriptableObject.CreateInstance<Breakthrough>());
    }

    public Breakthrough()
    {
        title = "Breakthrough"; // Name of the card.
        level = 1; // Amount of energy required to play the card.
        effectText = "Deal 3 damage.\nRemove all block."; // Text shown on the lower half of the card.
        targetMode = TargetMode.SpecificEnemy; // What the card should target (player, enemy, all enemies, etc.)
    }

    /// This is called when the card is played. Use it to apply the effects of the card. The
    /// variable "context" contains many useful fields and functions, view the documentation for
    /// ActionContext to find out about them.
    protected override IEnumerator Play(ActionContext context)
    {
        Player.instance.TriggerAttackAnim();
        context.targets[0].TriggerDamagedAnim();
        context.targets[0].TakeDamage(context.ComputeDamage(3));
        yield return new WaitForSeconds(0.5f);
        // Don't continue if the damage already killed the target.
        if (context.targets[0].dead) yield break;
        context.targets[0].ClearBlock();
        yield return new WaitForSeconds(0.5f);
    }
}