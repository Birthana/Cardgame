using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPEnergyIndicator : MonoBehaviour
{
    public TextMeshPro displayOn;

    void OnEnable()
    {
        BattleManager.instance.OnEnergyChange += UpdateEnergyText;
        UpdateEnergyText();
    }

    void OnDisable()
    {
        BattleManager.instance.OnEnergyChange -= UpdateEnergyText;
    }

    private void UpdateEnergyText()
    {
        if (!Player.instance) return;
        displayOn.text = "" + BattleManager.instance.energy + " / " + Player.instance.maxEnergy;
    }
}
