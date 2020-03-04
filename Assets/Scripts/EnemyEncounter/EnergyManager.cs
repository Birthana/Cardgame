using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyManager : MonoBehaviour
{
    public int maxEnergy;
    public TextMeshPro energyUI;
    private int currentEnergy;

    public int GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public void SpendEnergy(int energyCost)
    {
        currentEnergy -= energyCost;
        UpdateUI();
    }

    public void StartEnergy()
    {
        currentEnergy = maxEnergy;
        UpdateUI();
    }

    private void UpdateUI()
    {
        energyUI.text = currentEnergy + " / " + maxEnergy;
    }
}
