using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FieldEntity
{
    private static Player _instance = null;
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
}
