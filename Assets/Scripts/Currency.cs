using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Currency
{
    private static int currency = 50;

    public static void AddCurrency(int amount)
    {
        currency += amount;
    }

    public static void SubtractCurrency(int amount)
    {
        currency -= amount;
    }

    public static int GetCurrency()
    {
        return currency;
    }
}
