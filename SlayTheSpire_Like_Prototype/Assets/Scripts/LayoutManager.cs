using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LayoutManager : MonoBehaviour
{
    public float padding;
    public float totalTwist;
    public float scalingFactor;
    private Vector3 startPosition;
    private float startTwist;
    private float twistPerCard;
    private List<Card> groupedCards;

    private void Start()
    {
        startPosition = this.transform.position;
        groupedCards = new List<Card>();
    }

    public void AddCard(Card card)
    {
        if (card != null)
        {
            card.gameObject.transform.SetParent(this.gameObject.transform);
            groupedCards.Add(card);
            startTwist = -1f * (totalTwist / 2f);
            twistPerCard = totalTwist / groupedCards.Count;
        }
        LayoutCards(groupedCards.ToArray());
    }

    public void LayoutCards(Card[] cards)
    {
        ResetRotation(cards);
        if (cards.Length == 0)
            return;
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].transform.position = startPosition;
            cards[i].transform.position += new Vector3(i * padding, 0, i);
            cards[i].transform.Rotate(0, 0, startTwist + (i * twistPerCard));
            cards[i].transform.Translate(0, -1 * Mathf.Abs((startTwist + (i * twistPerCard))) * scalingFactor , 0);
        }
    }

    public void ResetRotation(Card[] cards)
    {
        foreach (Card card in cards)
        {
            card.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void RemoveCard(Card card)
    {
        groupedCards.Remove(card);
        LayoutCards(groupedCards.ToArray());
    }

    public void RemoveAll()
    {
        foreach (Card card in groupedCards.ToArray())
        {
            RemoveToDiscard(card);
            //Destroy(card.gameObject);
            card.gameObject.SetActive(false);
        }
    }

    public void RemoveToDiscard(Card card)
    {
        Deck.instance.AddToDiscard(card);
        RemoveCard(card);
    }
}
