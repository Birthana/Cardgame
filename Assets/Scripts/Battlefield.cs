using UnityEngine;

public class Battlefield : MonoBehaviour
{
    public GameObject playerHealth;
    [SerializeField]private CardManager cards;

    private void Start()
    {
        cards = FindObjectOfType<CardManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<Card>().hasTarget)
        {
            CardEffectManager effects = collision.gameObject.GetComponent<CardEffectManager>();
            effects.SetTargets(playerHealth);
            effects.AddToStack();
            CardEffectStack.instance.ResolveCardEffect();
            cards.PlayCard(collision.gameObject.GetComponent<Card>());
            collision.gameObject.SetActive(false);
        }
    }
}