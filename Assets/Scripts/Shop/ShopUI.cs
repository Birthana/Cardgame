using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject initialUI;
    public GameObject shopUI;

    public void DisplayShop()
    {
        initialUI.SetActive(false);
        shopUI.SetActive(true);
    }

    public void DisplayInitial()
    {
        shopUI.SetActive(false);
        initialUI.SetActive(true);
    }
}
