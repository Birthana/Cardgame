using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Deck : MonoBehaviour
{
    public TextMeshProUGUI deckText;

    private void OnEnable()
    {
        ResourceManager.OnDeckChange += ChangeUI;
        ResourceManager.RefreshUI();
    }

    private void OnDisable()
    {
        ResourceManager.OnDeckChange -= ChangeUI;
    }

    public void ChangeUI(int deckCount)
    {
        deckText.text = "" + deckCount;
    }
}
