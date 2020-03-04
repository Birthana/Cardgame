using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    public Card[] cards;
    public int[] count;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SetPlayerInformation();
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerInformation()
    {
        PlayerInformation.SetHealth(60);
        PlayerInformation.SetCurrency(99);
        for (int i = 0; i < cards.Length; i++)
        {
            for (int j = 0; j < count[i]; j++)
            {
                PlayerInformation.AddCard(cards[i]);
            }
        }
    }
}
