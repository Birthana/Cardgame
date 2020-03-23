using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuyItem : MonoBehaviour
{
    //Possible solution: In script that adds and displays buyable cards (and other items), add
    //BuyItem script to each and take appropriate action. May need to have UI functions from 
    //this script (ie MouseOver, click to buy, etc.)

    //Likely to be more than one type of item a player can buy.
    public GameObject item;
    public int cost;   
    public Card cardInfo;   //Likely a placeholder variable. Currently necessary when testing.

    private void Start()
    {
        //Likely not going to be needed in final version. Currently necessary when testing.
        item.GetComponent<CardAvatar>().displaying = cardInfo;
    }

    public void Buy()
    {
        //Checks if player can afford item.
        if (Currency.GetCurrency() >= cost)
        {
            //Checks type of item and takes appropriate action.
            if (item.GetComponent<CardAvatar>() != null)
            {
                Deck.instance.Add(item.GetComponent<CardAvatar>().displaying);
            }

            Currency.SubtractCurrency(cost);
            Debug.Log(Currency.GetCurrency());
        }
        else
        {
            Debug.Log("Not enough currency to buy!");
        }
    }

    //For debug purposes. Puts player in battle where the new bought card will eventually be seen.
    public void Change()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
