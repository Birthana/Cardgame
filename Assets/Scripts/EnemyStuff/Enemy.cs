using System.Collections;
using UnityEngine;

/// Represents an enemy on the battlefield.
public abstract class Enemy : FieldEntity
{
    public ActionIndicator actionIndicator;

    void OnEnable()
    {
        BattleManager.instance.AddEnemy(this);
    }

    void OnDisable()
    {
        BattleManager.instance.RemoveEnemy(this);
    }

    private ActionContext CreateActionContext() {
        Player player = Player.instance;
        return new ActionContext(player);
    }

    public void UpdateActionIndicatorWrapper()
    {
        UpdateActionIndicator(actionIndicator, CreateActionContext());
    }

    /// This method should update the given ActionIndicator to show the action this enemy intends
    /// to take when DoAttack is called.
    protected abstract void UpdateActionIndicator(ActionIndicator indicator, ActionContext context);

    public IEnumerator DoAttackWrapper() {
        actionIndicator.Hide();
        UpdateActionIndicator(actionIndicator, CreateActionContext());
        yield return DoAttack(CreateActionContext());
    }

    /// This method is called whenever it is this enemy's turn to attack.
    protected abstract IEnumerator DoAttack(ActionContext context);
}
