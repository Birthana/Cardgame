using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionIndicator : MonoBehaviour
{
    public SpriteRenderer iconRenderer;
    public Sprite attackSprite;
    public Sprite blockSprite;
    public TextMeshPro valueText;

    public void ShowAttack(int amount)
    {
        gameObject.SetActive(true);
        iconRenderer.sprite = attackSprite;
        valueText.text = "" + amount;
    }

    public void ShowBlock()
    {
        gameObject.SetActive(true);
        iconRenderer.sprite = blockSprite;
        valueText.text = "";
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
