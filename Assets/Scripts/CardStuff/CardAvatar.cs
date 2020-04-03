﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// The physical representation of a card. This is completely separate from the
/// Card class, which describes the information and effects a particular card
/// has, while this class is only designed to show that information on screen.
public class CardAvatar : MonoBehaviour
{
    /// Text meshes for displaying the card name, its effects, and how much energy it costs.
    public TextMeshPro nameText, effectText, levelText;
    /// OnHover is triggered when the mouse enters the card. OnBlur is triggered when the mouse
    /// leaves the card.
    public event Action<CardAvatar> OnHover, OnBlur;
    private Card _displaying;
    private bool ready = false, updatePending = false;

    /// The actual Card object that this avatar is representing. Writing to this variable will cause
    /// this avatar to show the information for the provided card.
    public Card displaying
    {
        set
        {
            _displaying = value;
            if (ready)
            {
                DisplayCard();
            }
            else
            {
                updatePending = true;
            }
        }
        get
        {
            return _displaying;
        }
    }

    void Start()
    {
        ready = true;
        if (updatePending)
        {
            DisplayCard();
            updatePending = false;
        }
    }

    private void DisplayCard()
    {
        nameText.text = _displaying.cardName;
        effectText.text = _displaying.cardEffectText;
        levelText.text = _displaying.level.ToString();
    }

    void OnMouseEnter()
    {
        OnHover?.Invoke(this);
    }

    void OnMouseExit()
    {
        OnBlur?.Invoke(this);
    }
}