using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name + " collides with " + this.name);
        if (collision.gameObject.GetComponent<AttackCard>())
        {
            /**
            int damage = collision.gameObject.GetComponent<AttackCard>().damage;
            this.GetComponent<Health>().TakeDamage(damage);
            Destroy(collision.gameObject);
            **/
        }
    }
}
