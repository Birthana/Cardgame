using System.Collections;
using UnityEngine;

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

    public abstract void DoAttack(ActionContext context);
}
