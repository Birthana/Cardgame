using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckTester : MonoBehaviour
{
    [System.Serializable]
    public class CardCreateSpec {
        public Card card;
        public int quantity;
    }

    /// Adds each of the specified cards <c>int</c> number of times.
    public List<CardCreateSpec> deckContents = new List<CardCreateSpec>();
    public int draw = 5;

    void Start()
    {
        Deck deck = Deck.instance;
        foreach (var request in deckContents)
        {
            for (int index = 0; index < request.quantity; index++)
            {
                deck.Add(Instantiate(request.card));
            }
        }
        deck.Draw(draw);
    }
}
