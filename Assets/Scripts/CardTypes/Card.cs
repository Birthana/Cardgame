using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName="Card Game Stuff/Card", fileName="New Card", order=10)]
public class Card : ScriptableObject
{
    public string cardName = "New Card";
    public int level = 1;
    public string cardEffectText = "Does nothing.";
}
