using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// Represents a single in-game card.
[CreateAssetMenu(menuName = "Card Game Stuff/Card", fileName = "New Card")]
public class Card : ScriptableObject
{
    public enum TargetMode
    {
        Player,
        AllEnemies,
        SpecificEnemy,
    }

    [Tooltip("The name that will appear at the top of the card.")]
    public string cardName = "New Card";
    [Tooltip("How much energy the card costs to play.")]
    public int level = 1;
    [TextArea]
    [Tooltip("The text that will be shown in the body (bottom half) of the card.")]
    public string cardEffectText = "Does nothing.";
    [Tooltip("How the player should select targets for this card.")]
    public TargetMode target = TargetMode.SpecificEnemy;
    [Tooltip("The effects this card will have when played. Effects are triggered in the order specified here.")]
    public CardEffect[] effects;

    /// Applies all effects of the card, in order.
    public IEnumerator Play(List<FieldEntity> targets)
    {
        ActionContext context = new ActionContext(targets);
        foreach (CardEffect effect in effects)
        {
            yield return effect.ApplyEffect(context);
        }
        yield break;
    }

    /// Plays the card against a single target instead of a list.
    public IEnumerator Play(FieldEntity target)
    {
        return Play(new List<FieldEntity>(new FieldEntity[] { target }));
    }
}
