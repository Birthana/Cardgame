using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LayoutManager))]
public class CardManager : MonoBehaviour
{
    [SerializeField] private List<Card> deck;
    [SerializeField] private List<Card> drop;
    [SerializeField] private List<Card> hand;
    [SerializeField] private EnergyManager energy;
    private bool allCardInstantiated;

    private void Start()
    {
        energy = FindObjectOfType<EnergyManager>();
    }

    public void StartEncounter()
    {
        SetDeck(PlayerInformation.GetDeck());
        Shuffle();
        DrawHand();
    }

    public void DrawHand()
    {
        for (int i = 0; i < 5; i++)
        {
            Draw();
        }
    }

    public void SetDeck(List<Card> playerDeck)
    {
        deck = playerDeck;
    }

    public void Shuffle()
    {
        Card[] tempDeck = deck.ToArray();
        for (int i = 0; i < deck.Count; i++)
        {
            int j = UnityEngine.Random.Range(i, deck.Count);
            Card tempCard = tempDeck[i];
            tempDeck[i] = tempDeck[j];
            tempDeck[j] = tempCard;
        }
        deck = new List<Card>(tempDeck);
    }

    public void Draw()
    {
        if (deck.Count == 0)
        {
            deck = drop;
            drop = new List<Card>();
            Shuffle();
            allCardInstantiated = true;
        }
        GameObject tempCard;
        if (allCardInstantiated)
        {
            tempCard = deck[0].gameObject;
            tempCard.SetActive(true);
        }
        else
        {
            tempCard = Instantiate(deck[0].gameObject, transform.position, Quaternion.identity);
        }
        AddToHand(tempCard.GetComponent<Card>());
        deck.Remove(deck[0]);
    }

    public void AddToDrop(Card card)
    {
        drop.Add(card);
    }

    public void AddToHand(Card card)
    {
        if (hand == null)
            hand = new List<Card>();
        hand.Add(card);
        GetComponent<LayoutManager>().AddCard(card);
    }

    public void PlayCard(Card card)
    {
        hand.Remove(card);
        GetComponent<LayoutManager>().RemoveToDiscard(card);
        energy.SpendEnergy(card.level);
    }
}
