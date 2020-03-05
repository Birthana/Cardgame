using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FieldEntity
{
    private static Player _instance = null;
    public int maxEnergy = 3;
    private int _energy;

    public event Action OnEnergyChange;

    public static Player instance { get => _instance; }
    public int energy { get => _energy; }

    void OnEnable() {
        if (_instance == null) {
            _instance = this;
        } else {
            Debug.LogError("More than one object was instantiated with the Player behavior!");
            Destroy(this);
        }
        _energy = maxEnergy;
        OnEnergyChange?.Invoke();
    }

    void OnDisable() {
        _instance = null;
    }

    public void SpendEnergy(int amount) {
        if (amount > _energy) {
            Debug.LogError("More energy was spent than we currently have!");
        }
        _energy -= amount;
        OnEnergyChange?.Invoke();
    }

    public override void StartTurn() {
        base.StartTurn();
        _energy = maxEnergy;
        OnEnergyChange?.Invoke();
    }
}
