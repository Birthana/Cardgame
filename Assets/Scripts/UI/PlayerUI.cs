using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI playerHealth;
    public TextMeshProUGUI goldAmount;
    public TextMeshProUGUI deckCount;

    // Start is called before the first frame update
    void Awake()
    {
        UITest.OnHealthChange += SetPlayerHealth;
        UITest.OnGoldChange += SetGoldAmount;
        UITest.OnDeckChange += SetDeckCount;
    }

    public void SetPlayerHealth(int currentHealth, int maxHealth)
    {
        playerHealth.text = "" + currentHealth + " / " + maxHealth;
    }

    public void SetGoldAmount(int gold)
    {
        goldAmount.text = "" + gold;
    }

    public void SetDeckCount(int currentDeckCount)
    {
        deckCount.text = "" + currentDeckCount;
    }
}
