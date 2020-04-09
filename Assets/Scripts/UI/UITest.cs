using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITest : MonoBehaviour
{
    public static event Action<int, int> OnHealthChange;
    public static event Action<int> OnGoldChange;
    public static event Action<int> OnDeckChange;

    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.LoadSceneAsync("PlayerUI", LoadSceneMode.Additive);
    }

    private void Start()
    {
        OnHealthChange?.Invoke(90, 100);
        OnGoldChange?.Invoke(95);
        OnDeckChange?.Invoke(20);
    }
}
