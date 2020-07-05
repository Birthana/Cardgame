using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MerchantUI : MonoBehaviour
{
    //Change Card Avatar prefab with a cost tmpro text box.
    public float CARD_SPACING;
    public CardAvatar cardAvatarPrefab;

    private void OnEnable()
    {
        Merchant.OnWaresChange += DisplayUI;
    }

    private void OnDisable()
    {
        Merchant.OnWaresChange -= DisplayUI;
    }

    public void DisplayUI(List<Merchant.MerchantCard> cards)
    {
        List<CardAvatar> cardsInShop = new List<CardAvatar>();
        foreach (Merchant.MerchantCard card in cards)
        {
            CardAvatar newCard = Instantiate(cardAvatarPrefab, this.transform);
            newCard.displaying = GetCardFromName(card.cardName);
            newCard.gameObject.GetComponent<TextMeshPro>().text = "" + card.GetActualCost();
            cardsInShop.Add(newCard);
        }

        for (int i = 0; i < cardsInShop.Count; i++)
        {
            float transformAmount = ((float)i) - ((float)cardsInShop.Count - 1) / 2;
            float angle = transformAmount * 3.0f;
            Vector3 position = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad),
                0,
                0
                ) * CARD_SPACING;
            cardsInShop[i].transform.localPosition = position;
        }
    }

    public Card GetCardFromName(string cardName)
    {
        Card result = null;
        foreach (Card card in CardIndex.instance.GetCards())
        {
            if (card.title == cardName)
            {
                result = card;
                break;
            }
        }
        return result;
    }
}