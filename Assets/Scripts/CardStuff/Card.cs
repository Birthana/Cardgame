using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// Represents a single in-game card.
public abstract class Card : ScriptableObject
{
    public enum TargetMode
    {
        Player,
        AllEnemies,
        SpecificEnemy,
    }

    [Tooltip("The title that will appear at the top of the card.")]
    public string title = "New Card";
    [Tooltip("How much energy the card costs to play.")]
    public int level = 1;
    [TextArea]
    [Tooltip("The text that will be shown in the body (bottom half) of the card.")]
    public string effectText = "Does nothing.";
    [Tooltip("How the player should select targets for this card.")]
    public TargetMode targetMode = TargetMode.SpecificEnemy;
    [Tooltip("The name of the artwork to display. It should be the name of a sprite located in Assets/Resources/CardArt.")]
    public string art = "Placeholder";

    /// This method will play the effects of the card given a particular context.
    protected abstract IEnumerator Play(ActionContext context);

    /// Plays the card against a list of targets.
    public IEnumerator Play(List<FieldEntity> targets)
    {
        ActionContext context = new ActionContext(targets);
        Player.instance.ModifyActionContextAsSource(context);
        return Play(context);
    }

    /// Plays the card against a single target.
    public IEnumerator Play(FieldEntity target)
    {
        return Play(new List<FieldEntity>(new FieldEntity[] { target }));
    }
}
