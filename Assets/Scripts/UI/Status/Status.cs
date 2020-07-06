using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Status : MonoBehaviour
{
    public SpriteRenderer statusImage;
    public TextMeshPro turnCount;

    public Sprite GetSprite()
    {
        return statusImage.sprite;
    }

    public void SetStatusImage(Sprite sprite) => statusImage.sprite = sprite;

    public void SetTurnCount(int turns) => turnCount.text = "" + turns;
}
