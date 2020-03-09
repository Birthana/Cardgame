using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Card Game Stuff/Card", fileName = "New Card")]
public class Card : ScriptableObject
{
    public enum TargetMode
    {
        Player,
        AllEnemies,
        SpecificEnemy,
    }

    public string cardName = "New Card";
    public int level = 1;
    public string cardEffectText = "Does nothing.";
    public TargetMode target = TargetMode.SpecificEnemy;
    public CardEffect[] effects;

    // Applies all effects of the card, in order.
    public void Play(List<FieldEntity> targets)
    {
        ActionContext context = new ActionContext(targets);
        foreach (CardEffect effect in effects)
        {
            effect.ApplyEffect(context);
        }
    }

    // Plays the card against a single target instead of a list.
    public void Play(FieldEntity target)
    {
        Play(new List<FieldEntity>(new FieldEntity[] { target }));
    }
}
