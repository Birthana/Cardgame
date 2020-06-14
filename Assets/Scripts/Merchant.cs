using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    public int numCardsToSell;
    public float costVariancePercent;
    public static event Action<List<MerchantCard>> OnWaresChange;

    public List<MerchantCard> allCards = new List<MerchantCard>();
    private List<MerchantCard> chosenCards = new List<MerchantCard>();

    [System.Serializable]
    public class MerchantCard
    {
        public string cardName;
        public int cost;
        private int actualCost;

        public MerchantCard(string card, int startCost)
        {
            cardName = card;
            cost = startCost;
        }

        public int GetActualCost() { return actualCost; }

        public void SetActualCost(int cost) => actualCost = cost;
    }

    private void Start()
    {
        SelectCards();
        SetCost();
        OnWaresChange?.Invoke(chosenCards);
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
                    MerchantCard cardToBuy = GetMerchantCardFromCardAvatar(selectedCard);
                    if (ResourceManager.instance.GetGold() >= cardToBuy.GetActualCost())
                    {
                        Deck.instance.Add(Instantiate(selectedCard.displaying));
                        ResourceManager.instance.SubtractGold(cardToBuy.GetActualCost());
                        Debug.Log("Brought for: " + cardToBuy.GetActualCost());
                        chosenCards.Remove(cardToBuy);
                        Destroy(selectedCard.gameObject);
                    }
                    else
                    {
                        Debug.Log("Costs: " + cardToBuy.GetActualCost());
                        Debug.Log("Only have: " + ResourceManager.instance.GetGold());
                    }
                }
            }
        }
    }

    public MerchantCard GetMerchantCardFromCardAvatar(CardAvatar card)
    {
        MerchantCard result = null;
        foreach (MerchantCard merchantCard in chosenCards)
        {
            if (merchantCard.cardName == card.displaying.title)
            {
                result = merchantCard;
                break;
            }
        }
        return result;
    }

    public void SelectCards()
    {
        List<int> chosenIndices = new List<int>();
        int index;
        for (int i = 0; i < numCardsToSell; i++)
        {
            do
            {
                index = UnityEngine.Random.Range(0, allCards.Count);
            } while (chosenIndices.Contains(index));
            chosenIndices.Add(index);
            chosenCards.Add(allCards[index]);
        }
    }

    public void SetCost()
    {
        float variance;
        foreach (MerchantCard card in chosenCards)
        {
            variance = UnityEngine.Random.Range(0f, costVariancePercent);
            float varienceValue = (UnityEngine.Random.Range(0, 2) == 0) ? 
                                  (1 + (variance / 100)) :
                                  (1 - (variance / 100));
            card.SetActualCost(Mathf.RoundToInt(card.cost * varienceValue));
        }
    }
}
