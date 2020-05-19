using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// This provided information like the targets of an action and multipliers for things like damage. 
/// "Action" refers to things like playing a card or an enemy performing an attack.
public class ActionContext
{
    public int damageBoost = 0;
    public float damageMultiplier = 1;
    public int magicUsed = 0;
    private List<FieldEntity> _targets;
    public List<FieldEntity> targets { get => _targets; }

    /// Create a context with only one target.
    public ActionContext(FieldEntity target) 
    {
        _targets = new List<FieldEntity>(new FieldEntity[] { target });
    }

    /// Create a context with a list of targets.
    public ActionContext(List<FieldEntity> targets)
    {
        _targets = targets;
    }

    /// Returns the amount of damage received based on [baseAmount] and any active multipliers.
    public int ComputeDamage(int baseAmount)
    {
        baseAmount = Math.Max(baseAmount + damageBoost, 0);
        return Mathf.FloorToInt(baseAmount * damageMultiplier);
    }
}
