using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    //Likely to be more than one type of item a player can buy.
    public GameObject item;
    public int cost;

    public void Buy()
    {
        //Checks type of item and takes appropriate action.
        /*if (item.GetComponent<CardAvatar>() != null)
        {
            //Deck.instance.Add(item.displaying);
        }*/

        Currency.SubtractCurrency(cost);
        Debug.Log(Currency.GetCurrency());
    }
}
