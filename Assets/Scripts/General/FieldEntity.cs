using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// This represents anything that participates in battle. It is the base class for both Player
/// and Enemy.
public class FieldEntity : MonoBehaviour
{
    public int maxHealth;
    public int health { get => Math.Max(_health, 0); }
    public int block { get => _block; }
    public bool dead { get => health == 0; }

    /// Invoked whenever health or block is changed.
    public event Action OnStatsChanged;
    /// Invoked when health reaches zero.
    public event Action OnDeath;

    private int _health;
    private int _block = 0;

    void Start()
    {
        _health = maxHealth;
        OnStatsChanged?.Invoke();
    }

    /// Applies the given amount of damage. 
    public void TakeDamage(int damage)
    {
        if (block >= damage)
        {
            _block -= damage;
            OnStatsChanged?.Invoke();
        }
        else
        {
            int unblockedDamage = damage - block;
            _block = 0;
            _health -= unblockedDamage;
            OnStatsChanged?.Invoke();
            if (_health <= 0)
            {
                OnDeath?.Invoke();
                Destroy(this.gameObject);
            }
        }
    }

    public void AddBlock(int block)
    {
        _block += block;
        OnStatsChanged?.Invoke();
    }

    public void ClearBlock() {
        _block = 0;
        OnStatsChanged?.Invoke();
    }

    public virtual void StartTurn()
    {
        _block = 0;
    }
}
