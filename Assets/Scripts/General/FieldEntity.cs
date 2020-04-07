using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// This represents anything that participates in battle. It is the base class for both Player
/// and Enemy.
public class FieldEntity : MonoBehaviour
{
    [Tooltip("This should have triggers named Attack and Damaged.")]
    public Animator animator;
    [Tooltip("Used when spacing out entities on the battlefield.")]
    public float WIDTH = 20f;
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

    private float seOutgoingDamageMultiplier = 1.0f;
    private float seIncomingDamageMultiplier = 1.0f;

    void Start()
    {
        _health = maxHealth;
        OnStatsChanged?.Invoke();
    }

    public void ModifyActionContextAsSource(ActionContext context)
    {
        context.damageMultiplier *= seOutgoingDamageMultiplier;
    }

    /// Applies the given amount of damage. 
    public void TakeDamage(int damage)
    {
        damage = ComputeDamageReceived(damage);
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

    /// Applies the given amount of damage as if this entity has no block.
    public void TakeDamageIgnoringBlock(int damage)
    {
        damage = ComputeDamageReceived(damage);
        _health -= damage;
        OnStatsChanged?.Invoke();
        if (_health <= 0)
        {
            OnDeath?.Invoke();
            Destroy(this.gameObject);
        }
    }

    /// Takes the amount of dealt damage and returns a predicted amount of damage received, taking
    /// things like status effects into account.
    public int ComputeDamageReceived(int damageDealt)
    {
        return Mathf.FloorToInt(damageDealt * seIncomingDamageMultiplier);
    }

    public void AddBlock(int block)
    {
        _block += block;
        OnStatsChanged?.Invoke();
    }

    public void ClearBlock()
    {
        _block = 0;
        OnStatsChanged?.Invoke();
    }

    /// Sets a trigger named "Damaged" on this field entity's Animator instance. This animation
    /// should be used when the field entity receives damage.
    public void TriggerDamagedAnim()
    {
        animator.SetTrigger("Damaged");
    }

    /// Sets a trigger named "Attack" on this field entity's Animator instance. This animation
    /// should be used when the field entity deals damage.
    public void TriggerAttackAnim()
    {
        animator.SetTrigger("Attack");
    }

    /// Increases / decreases the outgoing damage multiplier by a certain amount. 0.1 == 10%.
    public void ModifySEOutgoingDamageMultiplier(float changeAmount)
    {
        seOutgoingDamageMultiplier += changeAmount;
    }

    /// Increases / decreases the incoming damage multiplier by a certain amount. 0.1 == 10%.
    public void ModifySEIncomingDamageMultiplier(float changeAmount)
    {
        seIncomingDamageMultiplier += changeAmount;
    }

    public virtual void StartTurn()
    {
        _block = 0;
        seOutgoingDamageMultiplier = 1.0f;
        seIncomingDamageMultiplier = 1.0f;
    }
}
