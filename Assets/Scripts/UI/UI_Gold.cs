using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Gold : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    private void OnEnable()
    {
        ResourceManager.OnGoldChange += ChangeUI;
        ResourceManager.RefreshUI();
    }

    private void OnDisable()
    {
        ResourceManager.OnGoldChange -= ChangeUI;
    }

    public void ChangeUI(int currentGold)
    {
        goldText.text = "" + currentGold;
    }
}
