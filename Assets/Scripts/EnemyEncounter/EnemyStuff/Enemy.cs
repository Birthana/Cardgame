using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public EnemyAttack[] enemyAttacks;

    public void Attack()
    {
        enemyAttacks[Random.Range(0, enemyAttacks.Length)].Attack(target);
    }
}
