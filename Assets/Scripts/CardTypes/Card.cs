using UnityEngine;
using TMPro;
public abstract class Card : MonoBehaviour
{
    public string cardName;
    public int level;
    public bool hasTarget;
    [TextArea(1, 3)]
    public string cardEffectText;

    void Start()
    {
        TextMeshPro[] texts = this.GetComponentsInChildren<TextMeshPro>();
        texts[0].text = cardName;
        texts[1].text = cardEffectText;
        texts[2].text = "" + level;
    }
}
