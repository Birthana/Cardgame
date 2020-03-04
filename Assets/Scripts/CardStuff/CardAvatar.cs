using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardAvatar : MonoBehaviour
{
    public TextMeshPro nameText, effectText, levelText;
    public event Action<CardAvatar> OnHover, OnBlur;
    private Card _displaying;
    private bool ready = false, updatePending = false;

    public Card displaying {
        set {
            _displaying = value;
            if (ready) {
                DisplayCard();
            } else {
                updatePending = true;
            }
        }
        get {
            return _displaying;
        }
    }

    void Start()
    {
        ready = true;
        if (updatePending) {
            DisplayCard();
            updatePending = false;
        }
    }

    private void DisplayCard() {
        nameText.text = _displaying.cardName;
        effectText.text = _displaying.cardEffectText;
        levelText.text = _displaying.level.ToString();
    }

    void OnMouseEnter() {
        OnHover?.Invoke(this);
    }

    void OnMouseExit() {
        OnBlur?.Invoke(this);
    }
}
