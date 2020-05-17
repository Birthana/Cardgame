using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantUI : MonoBehaviour
{
    public float CARD_SPACING;

    [SerializeField]
    private List<MerchantCard> cardsForSell;

    private void Start()
    {
        DisplayUI();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D mouseHit = Physics2D.Raycast(mouseRay.origin, Vector2.zero);
            if (mouseHit)
            {
                CardAvatar selectedCard = mouseHit.collider.gameObject.GetComponent<CardAvatar>();
                if (selectedCard != null)
                {
                    MerchantCard cardToBuy = GetMerchantCard(selectedCard);
                    if (Currency.GetCurrency() >= cardToBuy.cost)
                    {
                        Deck.instance.Add(cardToBuy.card.displaying);
                        Currency.SubtractCurrency(cardToBuy.cost);
                        Debug.Log(Currency.GetCurrency());

                        cardsForSell.Remove(cardToBuy);
                        Destroy(cardToBuy.card.gameObject);
                        DisplayUI();
                    }
                    else
                    {
                        Debug.Log("Not enough currency.");
                    }
                }
            }
        }
    }

    public MerchantCard GetMerchantCard(CardAvatar card)
    {
        MerchantCard result = cardsForSell[0];
        for (int i = 0; i < cardsForSell.Count; i++)
        {
            if (card.Equals(cardsForSell[i].card))
            {
                result = cardsForSell[i];
            }
        }
        return result;
    }

    public void AddToMerchantShop(MerchantCard card)
    {
        cardsForSell.Add(card);
        DisplayUI();
    }

    public void DisplayUI()
    {
        for (int i = 0; i < cardsForSell.Count; i++)
        {
            float transformAmount = ((float)i) - ((float)cardsForSell.Count - 1) / 2;
            float angle = transformAmount * 3.0f;
            Vector3 position = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad),
                0,
                0
                ) * CARD_SPACING;
            cardsForSell[i].card.transform.localPosition = position;
            //cardsForSell[i].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
    }

    [System.Serializable]
    public class MerchantCard
    {
        public CardAvatar card;
        public int cost;
    }
}