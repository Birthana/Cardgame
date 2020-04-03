using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Game Stuff/Effects/Deal Damage Scaled By Portal", fileName = "DealDamageScaledByPortal")]
public class DealDamageScaledByPortal : CardEffect
{
    public int amount;

    public override void ApplyEffect(ActionContext context)
    {
        context.damageBoost = 2;
        foreach (FieldEntity target in context.targets)
        {
            target.TakeDamage(context.ComputeDamage(amount));
        }
    }
}
