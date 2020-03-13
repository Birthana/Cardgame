using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownUI : MonoBehaviour
{
    public GameObject initialUI;
    public GameObject shopUI;
    public GameObject restUI;

    public void DisplayInitial()
    {
        shopUI.SetActive(false);
        restUI.SetActive(false);
        initialUI.SetActive(true);
    }

    public void DisplayShop()
    {
        initialUI.SetActive(false);
        shopUI.SetActive(true);
    }

    public void DisplayRest()
    {
        initialUI.SetActive(false);
        restUI.SetActive(true);
    }
}
