using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BattleManager.instance.NewBattle();
    }
}
