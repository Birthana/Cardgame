using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private static Deck _instance = null;
    private List<Card> deck = new List<Card>();

    /// <summary>
    /// Returns a single instance of Deck. If no instance of Deck exists, one will be created.
    /// </summary>
    public static Deck instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Deck();
            }
            return _instance;
        }
    }

    /// <summary>
    /// Adds the specified card to the draw pile.
    /// </summary>
    public void Add(Card card)
    {
        deck.Add(card);
    }

    public List<Card> GetCards()
    {
        return deck;
    }
}
