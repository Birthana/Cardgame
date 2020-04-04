using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatIndicator : MonoBehaviour
{
    public FieldEntity statSource;
    public TextMeshPro healthBarText;
    public GameObject healthBarMask;
    public float maskWidth = 5.12f;
    public TextMeshPro blockText;
    public Renderer blockIcon;

    void Start()
    {
        statSource.OnStatsChanged += UpdateDisplay;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        float healthAmount = statSource.health / (float)statSource.maxHealth;
        healthBarMask.transform.localPosition = Vector3.left * (1 - healthAmount) * maskWidth;
        healthBarText.text = "" + statSource.health + "/" + statSource.maxHealth;

        int block = statSource.block;
        blockText.text = "" + block;
        blockText.gameObject.GetComponent<Renderer>().enabled = block > 0;
        blockIcon.enabled = block > 0;
    }
}
