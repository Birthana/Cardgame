using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPEnergyIndicator : MonoBehaviour
{
    public Player useEnergyFrom;
    public TextMeshPro displayOn;

    // Start is called before the first frame update
    void Start()
    {
        useEnergyFrom.OnEnergyChange += UpdateEnergyText;
        UpdateEnergyText();
    }

    private void UpdateEnergyText()
    {
        displayOn.text = "" + useEnergyFrom.energy + " / " + useEnergyFrom.maxEnergy;
    }
}
