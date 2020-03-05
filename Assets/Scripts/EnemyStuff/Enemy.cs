using System.Collections;
using UnityEngine;

public class Enemy : FieldEntity 
{
    public EnemyAttack[] enemyAttacks;

    void OnEnable() {
        EnemyManager.instance.Add(this);
    }

    void OnDisable() {
        EnemyManager.instance.Remove(this);
    }

    public void Attack()
    {
        enemyAttacks[Random.Range(0, enemyAttacks.Length)].Attack();
    }
}
