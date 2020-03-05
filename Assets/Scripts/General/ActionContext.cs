using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionContext
{
    public int damageBoost = 0;
    public float damageMultiplier = 1;
    private List<FieldEntity> _targets;
    public List<FieldEntity> targets { get => _targets; }

    public ActionContext(List<FieldEntity> targets) {
        _targets = targets;
    }

    public int ComputeDamage(int baseAmount) {
        baseAmount = Math.Max(baseAmount + damageBoost, 0);
        return Mathf.FloorToInt(baseAmount * damageMultiplier);
    }
}