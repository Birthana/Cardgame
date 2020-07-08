using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Convenience class for loading the deck with a preset set of cards and then
/// starting a new battle.
public class DeckTester : MonoBehaviour
{
    [Tooltip("This value indicates how many times to add every card in existence to the player's hand.")]
    public int everythingCopies = 0;
    [System.Serializable]
    public class CardCreateSpec
    {
        [Tooltip("The name of the card to add to the deck.")]
        public string title;
        [Tooltip("Number of times to add the card to the deck.")]
        public int quantity;
    }

    /// Adds each of the specified cards <c>int</c> number of times.
    public List<CardCreateSpec> deckContents = new List<CardCreateSpec>();

    void Start()
    {
        Deck deck = Deck.instance;
        foreach (Card card in CardIndex.instance.GetCards())
        {
            for (int index = 0; index < everythingCopies; index++)
            {  
                deck.Add(Instantiate(card));
            }
        }
        foreach (var request in deckContents)
        {
            Card template = null;
            foreach (Card card in CardIndex.instance.GetCards())
            {
                if (card.title == request.title)
                {
                    template = card;
                    break;
                }
            }

            if (template == null)
            {
                Debug.LogError("Error in deck tester: There is no such card with the title \"" + request.title + "\"!");
            }
            else
            {
                for (int index = 0; index < request.quantity; index++)
                {
                    deck.Add(Instantiate(template));
                }
            }
        }
    }
}
