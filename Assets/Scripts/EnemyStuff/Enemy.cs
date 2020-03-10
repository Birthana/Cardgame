using System.Collections;
using UnityEngine;

/// Represents an enemy on the battlefield.
public abstract class Enemy : FieldEntity
{
    void OnEnable()
    {
        BattleManager.instance.AddEnemy(this);
    }

    void OnDisable()
    {
        BattleManager.instance.RemoveEnemy(this);
    }

    /// This method is called whenever it is this enemy's turn to attack.
    public abstract void DoAttack(ActionContext context);
}
