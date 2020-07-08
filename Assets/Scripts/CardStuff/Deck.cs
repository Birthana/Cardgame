using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Holds all the cards in the player's deck. This has nothing to do with the state of the player's
/// deck during battle, for that see BattleManager. This distinction makes it easy to add cards
/// during battle as a buff / debuff without permanently adding those cards to the player's deck.
public class Deck
{
    private static Deck _instance = null;
    private List<Card> deck = new List<Card>();

    /// Returns a single instance of Deck. If no instance of Deck exists, one will be created.
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

    /// Adds the specified card to the deck. This has no effect on the current battle, if one is
    /// going on.
    public void Add(Card card)
    {
        deck.Add(card);
        ResourceManager.instance.SetDeckCount(deck.Count);
    }

    /// Returns all cards contained in the deck. This will not include cards temporarily gained
    /// during a battle.
    public List<Card> GetCards()
    {
        return deck;
    }
}
