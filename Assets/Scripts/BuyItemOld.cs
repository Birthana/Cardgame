using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//OUTDATED SCRIPT!!! Currently keeping for safety purposes. For new script, go to BuyItem.


public class BuyItemOld : MonoBehaviour
{
    //Possible solution: In script that adds and displays buyable cards (and other items), add
    //BuyItem script to each and take appropriate action. May need to have UI functions from 
    //this script (ie MouseOver, click to buy, etc.)

    //Likely to be more than one type of item a player can buy.
    public GameObject item;
    public int cost;        //Likely will be private later on. Currently public for testing convenience.
    public Card cardInfo;   //Likely a placeholder variable. Currently necessary when testing.

    private void Start()
    {
        //Likely not going to be needed in final version. Currently necessary when testing.
        //item.GetComponent<CardAvatar>().displaying = cardInfo;
    }

    private void OnMouseDown()
    {
        Buy();
    }

    public void Buy()
    {
        item = gameObject;

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

    public void SetCost(int cost)
    {
        this.cost = cost;
    }

    //For debug purposes. Puts player in battle where the new bought card will eventually be seen.
    public void Change()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
