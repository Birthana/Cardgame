using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //ResourceManager.instance.SubtractHealth(10);
            ResourceManager.instance.AddGold(15);
        }
    }
}
