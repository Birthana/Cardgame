using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Health playerHealth;

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
        playerHealth.EndTurn();
        StartTurn();
    }
}
