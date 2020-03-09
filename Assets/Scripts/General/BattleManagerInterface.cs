using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The GUI is stupid and doesn't like static methods. This is a behavior that can be attached to
// the GUI to provide a non-static way of accessing the static methods of BattleManager.
public class BattleManagerInterface : MonoBehaviour
{
    public void EndTurn()
    {
        BattleManager.instance.EndTurn();
    }
}
