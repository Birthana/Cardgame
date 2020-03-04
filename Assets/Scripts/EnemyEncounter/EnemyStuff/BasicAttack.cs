using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : EnemyAttack
{
    public int damage;

    public override void Attack(GameObject target)
    {
        target.GetComponent<Health>().TakeDamage(damage);
    }
}
