using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Represents the player during a battle.
public class Player : FieldEntity
{
    private static Player _instance = null;
    // TODO: see about moving this somewhere else. Energy is managed in the BattleManager class so 
    // it's a bit odd to have this variable here.
    public int maxEnergy = 3;

    public static Player instance { get => _instance; }

    void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("More than one object was instantiated with the Player behavior!");
            Destroy(this);
        }
    }

    void OnDisable()
    {
        _instance = null;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        ResourceManager.instance.SetHealth(health);
    }
}
