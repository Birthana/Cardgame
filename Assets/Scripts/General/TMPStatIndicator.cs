using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPStatIndicator : MonoBehaviour
{
    public FieldEntity useStatsFrom;
    public TextMeshPro displayOn;

    // Start is called before the first frame update
    void Start()
    {
        useStatsFrom.OnStatsChanged += UpdateHealthText;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        displayOn.text = "" + (useStatsFrom.health + useStatsFrom.block) 
            + " / " + useStatsFrom.maxHealth;
    }
}
