using System;
using UnityEngine;

/// This is a way to have an update function outside of a monobehavior.
public class UpdateCaller : MonoBehaviour
{
    private static UpdateCaller _instance;
    public static UpdateCaller instance
    {
        get
        {

            if (_instance == null)
            {
                _instance = new GameObject("[Update Caller]").AddComponent<UpdateCaller>();
            }
            return _instance;
        }
    }

    public Action updateCallback;

    private void Update()
    {
        updateCallback();
    }
}
