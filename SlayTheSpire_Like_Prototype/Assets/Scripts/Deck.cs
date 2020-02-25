using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static Deck instance = null;
    public static event Action<Card> OnDraw;
    public Card[] cards;
    public int[] count;
    [SerializeField] private List<Card> deck = new List<Card>();
    [SerializeField] private List<Card> discard = new List<Card>();
    private bool allCardInstantiated;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadDeck();
        for (int i = 0; i < 5; i++)
        {
            Draw();
        }
    }

    public void LoadDeck()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            for (int j = 0; j < count[i]; j++)
            {
                deck.Add(cards[i]);
            }
        }
        Shuffle();
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
            deck = discard;
            discard = new List<Card>();
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
            tempCard = Instantiate(deck[0].gameObject, this.transform.position, Quaternion.identity);
        }
        OnDraw?.Invoke(tempCard.GetComponent<Card>());
        deck.Remove(deck[0]);
    }

    public void AddToDiscard(Card card)
    {
        discard.Add(card);
    }
}
