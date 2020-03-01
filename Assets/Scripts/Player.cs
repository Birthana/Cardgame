using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    public int maxEnergy;
    public TextMeshPro energyCounter;
    private int currentEnergy;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            currentEnergy = maxEnergy;
            energyCounter.text = currentEnergy + " / " + maxEnergy;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public int GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public void SpendEnergy(int energyCost)
    {
        currentEnergy -= energyCost;
        energyCounter.text = currentEnergy + " / " + maxEnergy;
    }

    public void StartTurn()
    {
        currentEnergy = maxEnergy;
        energyCounter.text = currentEnergy + " / " + maxEnergy;
    }
}
