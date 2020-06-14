using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantUIModified : MonoBehaviour
{
    public float CARD_SPACING;
    public int numCardsToSell;
    public float costVariancePercent;   //Card cost will vary by up to this percent.
    public CardAvatar cardAvatarPrefab;

    [SerializeField]
    private List<MerchantCard> cardsForSell = new List<MerchantCard>();

    private List<MerchantCard> chosenCards = new List<MerchantCard>();
    private List<MerchantCard> leftoverCards = new List<MerchantCard>();

    private void Start()
    {
        ChooseCards();
        SetCost();
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
                    if (Currency.GetCurrency() >= cardToBuy.actualCost)
                    {
                        Deck.instance.Add(Instantiate(cardToBuy.card));
                        Currency.SubtractCurrency(cardToBuy.actualCost);
                        Debug.Log(Currency.GetCurrency());

                        chosenCards.Remove(cardToBuy);
                        //Destroy(cardToBuy.card);
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

    private void ChooseCards()
    {
        int curIndex;
        List<int> chosenIndices = new List<int>();
        leftoverCards = cardsForSell;
        for (int i = 0; i < numCardsToSell; i++)
        {
            do
            {
                curIndex = Random.Range(0, cardsForSell.Count);
            } while (chosenIndices.Contains(curIndex));

            chosenIndices.Add(curIndex);
            chosenCards.Add(cardsForSell[curIndex]);
            leftoverCards.Remove(cardsForSell[curIndex]);
        }
    }

    private void SetCost()
    {
        int newCost;
        int operatorToUse;
        float actualVariance;

        for (int i = 0; i < chosenCards.Count; i++)
        {
            operatorToUse = Random.Range(0, 2);
            actualVariance = Random.Range(0f, costVariancePercent);

            if (operatorToUse == 0)
            {
                newCost = Mathf.RoundToInt(chosenCards[i].cost * (1 + (actualVariance / 100)));
            }
            else
            {
                newCost = Mathf.RoundToInt(chosenCards[i].cost * (1 - (actualVariance / 100)));
            }

            chosenCards[i].actualCost = newCost;
        }
    }

    public MerchantCard GetMerchantCard(CardAvatar card)
    {
        MerchantCard result = chosenCards[0];
        for (int i = 0; i < chosenCards.Count; i++)
        {
            if (card.Equals(chosenCards[i].card))
            {
                result = chosenCards[i];
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
        //Gets rid of cards not chosen.
        for (int i = 0; i < leftoverCards.Count; i++)
        {
            //leftoverCards[i].card.gameObject.SetActive(false);
        }

        for (int i = 0; i < chosenCards.Count; i++)
        {
            float transformAmount = ((float)i) - ((float)chosenCards.Count - 1) / 2;
            float angle = transformAmount * 3.0f;
            Vector3 position = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad),
                0,
                0
                ) * CARD_SPACING;
            //chosenCards[i].card.transform.localPosition = position;
            //cardsForSell[i].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
    }

    [System.Serializable]
    public class MerchantCard
    {
        public Card card;
        public int cost;

        [HideInInspector]
        public int actualCost;
    }
}
