using UnityEngine;

public class Battlefield : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<Card>().hasTarget)
        {
            CardEffectManager effects = collision.gameObject.GetComponent<CardEffectManager>();
            effects.SetTargets(player);
            effects.AddToStack();
            CardEffectStack.instance.ResolveCardEffect();
            Hand.instance.RemoveToDiscard(collision.gameObject.GetComponent<Card>());
            collision.gameObject.SetActive(false);
        }
    }
}