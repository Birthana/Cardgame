using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    private void Start()
    {
        ResourceManager.instance.SubtractHealth(10);
        ResourceManager.instance.AddGold(15);
    }
}
