using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckTester : MonoBehaviour
{
    /// Adds each of the specified cards <c>int</c> number of times.
    public List<(Card card, int quantity)> addCards = new List<(Card, int)>();

    void Start()
    {
        Deck deck = Deck.instance;
        foreach (var request in addCards)
        {
            for (int index = 0; index < request.quantity; index++)
            {
                // TODO: Copy card?
                deck.Add(request.card);
            }
        }
    }
}
