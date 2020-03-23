using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script is attached to a buyable item.

public class BuyItem : MonoBehaviour
{
    //Cost for item.
    private int cost;

    private void OnMouseDown()
    {
        Buy();
    }

    private void Buy()
    {
        //Checks if player can afford item.
        if (Currency.GetCurrency() >= cost)
        {
            //Checks type of item and takes appropriate action.
            //Currently, only item type is card, so checks for CardAvatar.
            if (gameObject.GetComponent<CardAvatar>() != null)
            {
                Deck.instance.Add(gameObject.GetComponent<CardAvatar>().displaying);
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
}
