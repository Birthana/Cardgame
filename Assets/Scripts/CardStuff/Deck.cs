using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private static Deck _instance = null;
    public event Action<Card> OnDraw;
    public event Action<Card> OnDiscard;
    public event Action OnShuffle;
    [SerializeField] private List<Card> deck = new List<Card>();
    [SerializeField] private List<Card> hand = new List<Card>();
    [SerializeField] private List<Card> discard = new List<Card>();

    /// <summary>
    /// Returns a single instance of Deck. If no instance of Deck exists, one will be created.
    /// </summary>
    public static Deck instance {
        get {
            if (_instance == null) {
                _instance = new Deck();
            }
            return _instance;
        }
    }

    /// <summary>
    /// Moves all discarded cards into the draw pile, then shuffles the draw pile. Triggers
    /// OnShuffle once completed.
    /// </summary>
    private void ShuffleDiscardIntoDeck() {
        // First, add the discarded cards back into the deck.
        if (deck.Count == 0) {
            deck = discard;
            discard = deck;
        } else {
            deck.AddRange(discard);
            discard.Clear();
        }
        // Then, shuffle the deck.
        for (int index = 0; index < deck.Count; index++) {
            int swapIndex = UnityEngine.Random.Range(0, deck.Count);
            (deck[index], deck[swapIndex]) = (deck[swapIndex], deck[index]);
        }
        OnShuffle?.Invoke();
    }

    /// <summary>
    /// Adds the specified card to the draw pile.
    /// </summary>
    public void Add(Card card) {
        deck.Add(card);
    }

    /// <summary>
    /// Moves one card from the deck to the hand. Triggers OnDraw after drawing the card.
    /// </summary>
    public void Draw()
    {
        if (deck.Count == 0)
        {
            ShuffleDiscardIntoDeck();
        }
        Card drawn = deck[0];
        deck.RemoveAt(0);
        hand.Add(drawn);
        OnDraw?.Invoke(drawn);
    }

    /// <summary>
    /// Draws <c>quantity</c> cards, triggering OnDraw after each card is drawn.
    /// </summary>
    public void Draw(int quantity) {
        for (int card = 0; card < quantity; card++) {
            Draw();
        }
    }

    /// <summary>
    /// Adds a card to the discard pile. If the card was in the hand, removes that card from the
    /// hand. If not, an error is written to the console, but no exception is thrown.
    /// </summary>
    public void Discard(Card card)
    {
        if (!hand.Remove(card)) {
            Debug.LogError("A card was Discard()ed without being in the current hand:", card);
        }
        discard.Add(card);
        OnDiscard?.Invoke(card);
    }

    /// <summary>
    /// Moves everything in the hand and discard pile into the draw pile, then shuffles the 
    /// draw pile. Emits OnShuffle after everything is done.
    /// </summary>
    public void Reset() {
        foreach (Card card in hand) {
            OnDiscard?.Invoke(card);
        }
        discard.AddRange(hand);
        hand.Clear();
        ShuffleDiscardIntoDeck();
    }
}
