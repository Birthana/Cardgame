using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantUI : MonoBehaviour
{
    public float CARD_SPACING;

    [SerializeField]
    private List<CardAvatar> cardsForSell;

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
        }
    }
}