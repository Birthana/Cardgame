using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static Hand instance = null;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Deck.OnDraw += Draw;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Draw(Card card)
    {
        this.GetComponent<LayoutManager>().AddCard(card);
    }

    public void RemoveCard(Card card)
    {
        this.GetComponent<LayoutManager>().RemoveCard(card);
        Player.instance.SpendEnergy(1);
    }

    public void RemoveToDiscard(Card card)
    {
        this.GetComponent<LayoutManager>().RemoveToDiscard(card);
        Player.instance.SpendEnergy(1);
    }
}
