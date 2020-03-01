using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyAttack[] enemyAttacks;

    public void Attack()
    {
        enemyAttacks[Random.Range(0, enemyAttacks.Length)].Attack();
    }

    private void OnDestroy()
    {
        EnemyManager.instance.Remove(this);
    }
}
