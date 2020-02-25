using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public void StartTurn()
    {
        for (int i = 0; i < 5; i++)
        {
            Deck.instance.Draw();
        }
        Player.instance.StartTurn();
    }

    public void EndTurn()
    {
        Hand.instance.GetComponent<LayoutManager>().RemoveAll();
        StartTurn();
    }
}
