using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public static ResourceManager _instance = null;
    public int MAX_HEALTH, MAX_GOLD;
    private int currentHealth, currentGold;
    public static event Action<int, int> OnHealthChange;
    public static event Action<int, int> OnGoldChange;

    public ResourceManager(int maxHealth, int maxGold)
    {
        MAX_HEALTH = currentHealth = maxHealth;
        MAX_GOLD = currentGold = maxGold;
        OnHealthChange?.Invoke(currentHealth, MAX_HEALTH);
        OnGoldChange?.Invoke(currentGold, MAX_GOLD);
    }

    public static ResourceManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new ResourceManager(50, 100);
            return _instance;
        }
    }

    public void AddHealth(int health)
    {
        currentHealth += health;
        if (currentHealth > MAX_HEALTH)
            currentHealth = MAX_HEALTH;
        OnHealthChange?.Invoke(currentHealth, MAX_HEALTH);
    }

    public void SubtractHealth(int health)
    {
        currentHealth -= health;
        if (currentHealth < 0)
            currentHealth = 0;
        OnHealthChange?.Invoke(currentHealth, MAX_HEALTH);
    }

    public int GetGold()
    {
        return currentGold;
    }

    public void AddGold(int gold)
    {
        currentGold += gold;
        OnGoldChange?.Invoke(currentGold, MAX_GOLD);
    }

    public void SubtractGold(int gold)
    {
        currentGold -= gold;
        if (currentGold < 0)
            currentGold = 0;
        OnGoldChange?.Invoke(currentGold, MAX_GOLD);
    }
}
