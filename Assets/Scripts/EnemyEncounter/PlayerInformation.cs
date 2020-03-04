using System.Collections.Generic;
using UnityEngine;

public static class PlayerInformation
{
    public static int health;
    private static int currency;
    private static List<Card> deck = new List<Card>();

    public static int GetHealth()
    {
        return health;
    }

    public static void SetHealth(int value)
    {
        health = value;
    }

    public static int GetCurrency()
    {
        return currency;
    }

    public static void SetCurrency(int value)
    {
        currency = value;
    }

    public static List<Card> GetDeck()
    {
        return deck;
    }

    public static void AddCard(Card card)
    {
        deck.Add(card);
    }

    public static void RemoveCard(Card card)
    {
        deck.Remove(card);
    }

    public static void PrintTestDeck()
    {
        List<Card> deck = PlayerInformation.GetDeck();
        foreach (var card in deck)
        {
            Debug.Log(card);
        }
    }
}
