using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    private static BattleManager _instance = null;
    public static BattleManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BattleManager();
            }
            return _instance;
        }
    }
    private List<Enemy> _enemies = new List<Enemy>();
    public List<Enemy> enemies { get => _enemies; }
    private List<Card> drawPile = new List<Card>();
    private List<Card> hand = new List<Card>();
    private List<Card> discardPile = new List<Card>();

    public event Action OnReset;
    public event Action<Card> OnDraw;
    public event Action<Card> OnDiscard;
    public event Action OnShuffle;

    // TODO: Enemy attacks.

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        if (_enemies.Count == 0)
        {
            Debug.Log("Battle complete! TODO: Do anything");
        }
    }

    private void MoveDiscardToDraw()
    {
        if (drawPile.Count == 0)
        {
            drawPile = discardPile;
            discardPile = drawPile;
        }
        else
        {
            drawPile.AddRange(discardPile);
            discardPile.Clear();
        }
    }

    private void ShuffleDraw()
    {
        for (int index = 0; index < drawPile.Count; index++)
        {
            int swapIndex = UnityEngine.Random.Range(0, drawPile.Count);
            (drawPile[index], drawPile[swapIndex]) = (drawPile[swapIndex], drawPile[index]);
        }
        OnShuffle?.Invoke();
    }

    public void DrawCard()
    {
        if (drawPile.Count == 0)
        {
            MoveDiscardToDraw();
            ShuffleDraw();
        }
        Card drawn = drawPile[0];
        drawPile.RemoveAt(0);
        hand.Add(drawn);
        OnDraw?.Invoke(drawn);
    }

    /// <summary>
    /// Draws <c>quantity</c> cards, triggering OnDraw after each card is drawn.
    /// </summary>
    public void DrawCards(int quantity) {
        for (int card = 0; card < quantity; card++) {
            DrawCard();
        }
    }

    /// <summary>
    /// Adds a card to the discard pile. If the card was in the hand, removes that card from the
    /// hand. If not, an error is written to the console, but no exception is thrown.
    /// </summary>
    public void DiscardCard(Card card)
    {
        if (!hand.Remove(card)) {
            Debug.LogError("A card was Discard()ed without being in the current hand:", card);
        }
        discardPile.Add(card);
        OnDiscard?.Invoke(card);
    }

    /// <summary>
    /// Performs everything necessary to start a new battle. The draw pile will be set to the
    /// contents of the deck. Five cards will be drawn from the pile.
    /// </summary>
    public void NewBattle() {
        OnReset.Invoke();
        drawPile = new List<Card>(Deck.instance.GetCards());
        ShuffleDraw();
        hand.Clear();
        discardPile.Clear();
        DrawCards(5);
    }
}
