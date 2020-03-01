using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : EnemyAttack
{
    public int damage;

    public override void Attack()
    {
        Player.instance.GetComponent<Health>().TakeDamage(damage);
    }
}
