using System.Collections;
using UnityEngine;

public class Enemy : FieldEntity
{
    public EnemyAttack[] enemyAttacks;

    void OnEnable()
    {
        BattleManager.instance.AddEnemy(this);
    }

    void OnDisable()
    {
        BattleManager.instance.AddEnemy(this);
    }

    public void Attack()
    {
        enemyAttacks[Random.Range(0, enemyAttacks.Length)].Attack();
    }
}
