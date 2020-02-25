using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name + " collides with " + this.name);
        if (collision.gameObject.GetComponent<DefenseCard>())
        {
            int block = collision.gameObject.GetComponent<DefenseCard>().health;
            player.GetComponent<Health>().AddBlock(block);
            Hand.instance.RemoveToDiscard(collision.gameObject.GetComponent<Card>());
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
