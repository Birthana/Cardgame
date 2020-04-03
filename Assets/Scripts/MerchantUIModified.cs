using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantUIModified : MonoBehaviour
{
    public float CARD_SPACING;
    public int numCardsToSell;

    [SerializeField]
    private List<MerchantCard> cardsForSell;
    private List<MerchantCard> chosenCards;
    private List<MerchantCard> leftoverCards;

    private void Start()
    {
        ChooseCards();
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

    private void ChooseCards()
    {
        int curIndex;
        List<int> chosenIndices = new List<int>();

        chosenCards = new List<MerchantCard>();
        leftoverCards = new List<MerchantCard>();

        numCardsToSell = Mathf.Clamp(numCardsToSell, 0, cardsForSell.Count);

        //Populate leftoverCards.
        for (int i = 0; i < cardsForSell.Count; i++)
        {
            leftoverCards.Add(cardsForSell[i]);
        }

        //Choose cards.
        for (int i = 0; i < numCardsToSell; i++)
        {
            //Search for unique index (prevents repeating cards).
            do
            {
                curIndex = Random.Range(0, cardsForSell.Count);
            } while (chosenIndices.Contains(curIndex));

            chosenIndices.Add(curIndex);
            chosenCards.Add(cardsForSell[curIndex]);
            leftoverCards.Remove(cardsForSell[curIndex]);
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
        //Gets rid of cards not chosen.
        for (int i = 0; i < leftoverCards.Count; i++)
        {
            leftoverCards[i].card.gameObject.SetActive(false);
        }

        for (int i = 0; i < numCardsToSell; i++)
        {
            float transformAmount = ((float)i) - ((float)numCardsToSell - 1) / 2;
            float angle = transformAmount * 3.0f;
            Vector3 position = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad),
                0,
                0
                ) * CARD_SPACING;
            chosenCards[i].card.transform.localPosition = position;
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




/* Old Code 
public class MerchantUIModified : MonoBehaviour
{
    public float CARD_SPACING;

    [SerializeField]
    private List<CardAvatar> cardsForSell;
    public Card cardInfoPlaceholder;    //Used for testing purposes.

    private void Start()
    {
        DisplayUI();
    }

    public void AddToMerchantShop(CardAvatar card)
    {
        cardsForSell.Add(card);
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
            cardsForSell[i].transform.localPosition = position;
            //cardsForSell[i].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            cardsForSell[i].displaying = cardInfoPlaceholder;   //Temporary, for testing purposes.

            cardsForSell[i].gameObject.AddComponent<BuyItem>();
            SetBuyAttributes(cardsForSell[i]);
        }
    }

    private void SetBuyAttributes(CardAvatar curCard)
    {
        //Idea: we could have a nested class in a style similar to Josh's DeckTester class.
        //Then, we could set a cost for each item added to be bought, instead of needing to check
        //the name. This class would be the type for the cardsForSell list instead of the
        //current CardAvatar. (This would require some tweaking in the code to work properly.)
        switch (curCard.displaying.cardName)
        {
            case "Damage10":
                curCard.GetComponent<BuyItem>().SetCost(10);
                break;
            default:
                Debug.Log("Card has no set cost.");
                break;
        }
    }
}   */
