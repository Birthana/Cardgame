using System.Collections.Generic;
using UnityEngine;

public class CardEffectStack : MonoBehaviour
{
    public static CardEffectStack instance = null;
    public List<BaseCardEffect> cardEffects;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void AddCardEffect(BaseCardEffect cardEffect)
    {
        cardEffects.Add(cardEffect);
    }

    public void ResolveCardEffect()
    {
        while (cardEffects.Count != 0)
        {
            BaseCardEffect currentCardEffect = cardEffects[0];
            currentCardEffect.Resolve();
            cardEffects.RemoveAt(0);
        }
    } 
}
