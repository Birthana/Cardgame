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

    private void Show()
    {
        iconRenderer.enabled = true;
    }

    public void ShowAttack(int amount)
    {
        Show();
        amount = Player.instance.ComputeDamageReceived(amount);
        iconRenderer.sprite = attackSprite;
        valueText.text = "" + amount;
    }

    public void ShowBlock(int amount)
    {
        Show();
        iconRenderer.sprite = blockSprite;
        valueText.text = "" + amount;
    }

    public void Hide() {
        iconRenderer.enabled = false;
        valueText.text = "";
    }
}
