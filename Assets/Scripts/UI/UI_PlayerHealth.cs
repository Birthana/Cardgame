using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    private void OnEnable()
    {
        ResourceManager.OnHealthChange += ChangeUI;
        ResourceManager.RefreshUI();
    }

    private void OnDisable()
    {
        ResourceManager.OnHealthChange -= ChangeUI;
    }

    public void ChangeUI(int currentHealth, int maxHealth)
    {
        healthText.text = currentHealth + " / " + maxHealth;
    }
}
