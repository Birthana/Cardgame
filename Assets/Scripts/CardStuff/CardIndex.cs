using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Holds a set containing one of each possible card in the game.
public class CardIndex
{
    private static CardIndex _instance = null;
    private List<Card> index = new List<Card>();

    /// Returns a single instance of CardIndex. If no instance of CardIndex exists, one will be created.
    public static CardIndex instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CardIndex();
            }
            return _instance;
        }
    }

    /// Adds the specified card to the index. Each kind of card in the game should only be added
    /// once.
    public void Add(Card card)
    {
        index.Add(card);
    }

    /// Returns all cards contained in the index.
    public List<Card> GetCards()
    {
        return index;
    }
}
