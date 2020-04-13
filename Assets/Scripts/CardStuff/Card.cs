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

    /// Set magicCost to this value to allow the card to be used with any amount of magic. The 
    /// card can change its effects based on how much magic actually gets used.
    public const int ANY_MAGIC_COST = -1;

    [Tooltip("The title that will appear at the top of the card.")]
    public string title = "New Card";
    [Tooltip("How much energy the card costs to play.")]
    public int level = 1;
    [Tooltip("How much subjugation the card costs to play.")]
    public int magicCost = 0;
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
        BattleManager.instance.SpendEnergy(level);
        if (magicCost == ANY_MAGIC_COST) {
            int magicAmount = 0;
            if (BattleManager.instance.friendlyPortal != null) {
                magicAmount = BattleManager.instance.friendlyPortal.GetAmount();
                BattleManager.instance.friendlyPortal.ReduceAmount(magicAmount);
            }
            Debug.Log(magicAmount);
            context.magicUsed += magicAmount;
        } else {
            BattleManager.instance.friendlyPortal?.ReduceAmount(magicCost);
            context.magicUsed += magicCost;
        }
        Player.instance.ModifyActionContextAsSource(context);
        return Play(context);
    }

    /// Plays the card against a single target.
    public IEnumerator Play(FieldEntity target)
    {
        return Play(new List<FieldEntity>(new FieldEntity[] { target }));
    }

    public bool CanBePlayed() {
        return 
            BattleManager.instance?.energy >= level
            && (magicCost <= 0 || BattleManager.instance?.friendlyPortal?.GetAmount() >= magicCost);
    }
}
