using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}