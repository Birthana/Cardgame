using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
    public BaseCardEffect[] cardEffects;

    public void AddToStack()
    {
        foreach (var cardEffect in cardEffects)
        {
            CardEffectStack.instance.AddCardEffect(cardEffect);
        }
    }

    public void SetTargets(GameObject target)
    {
        foreach (var cardEffect in cardEffects)
        {
            cardEffect.SetTarget(target);
        }
    }
}
