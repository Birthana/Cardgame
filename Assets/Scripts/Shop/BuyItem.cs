using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    public GameObject item;
    public int cost;

    public void Buy()
    {
        Currency.SubtractCurrency(cost);
        Debug.Log(Currency.GetCurrency());
    }
}
