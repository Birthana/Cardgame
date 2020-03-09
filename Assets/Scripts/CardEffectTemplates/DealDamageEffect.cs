using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Game Stuff/Effects/Deal Damage", fileName = "DealDamage")]
public class DealDamageEffect : CardEffect
{
    public int amount = 5;

    public override void ApplyEffect(ActionContext context)
    {
        foreach (FieldEntity target in context.targets)
        {
            target.TakeDamage(amount);
        }
    }
}
