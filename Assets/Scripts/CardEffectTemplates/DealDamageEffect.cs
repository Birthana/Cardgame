﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// An effect which damages all targets by a certain amount when it is played.
[CreateAssetMenu(menuName = "Card Game Stuff/Effects/Deal Damage", fileName = "DealDamage")]
public class DealDamageEffect : CardEffect
{
    public int amount = 5;

    public override IEnumerator ApplyEffect(ActionContext context)
    {
        Player.instance.TriggerAttackAnim();
        foreach (FieldEntity target in context.targets)
        {
            target.TakeDamage(context.ComputeDamage(amount));
            target.TriggerDamagedAnim();
        }
        yield return new WaitForSeconds(0.5f);
    }
}
