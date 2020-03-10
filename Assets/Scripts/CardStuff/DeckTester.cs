using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Convenience class for loading the deck with a preset set of cards and then
/// starting a new battle.
public class DeckTester : MonoBehaviour
{
    [System.Serializable]
    public class CardCreateSpec
    {
        [Tooltip("The card to add to the deck.")]
        public Card card;
        [Tooltip("Number of times to add the card to the deck.")]
        public int quantity;
    }

    /// Adds each of the specified cards <c>int</c> number of times.
    public List<CardCreateSpec> deckContents = new List<CardCreateSpec>();

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
        BattleManager.instance.NewBattle();
    }
}
