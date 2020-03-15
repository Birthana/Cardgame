using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Manages everything that happens during a battle: keeping track of enemies, keeping track of 
/// the player's draw, hand, and discard piles, and keeping track of the player's energy.
public class BattleManager
{
    private static BattleManager _instance = null;
    /// The single, global instance of BattleManager.
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
    /// A list of all enemies currently in the battle.
    public List<Enemy> enemies { get => _enemies; }
    private List<Card> drawPile = new List<Card>();
    private List<Card> hand = new List<Card>();
    private List<Card> discardPile = new List<Card>();
    private int _energy = 0;
    /// How much energy the current player has.
    public int energy { get => _energy; }

    /// Invoked when the entire battle is reset to start a new battle.
    public event Action OnReset;
    /// Invoked when a new card is drawn.
    public event Action<Card> OnDraw;
    /// Invoked when a card is discarded.
    public event Action<Card> OnDiscard;
    /// Invoked when the draw pile is shuffled.
    public event Action OnShuffle;
    /// Invoked when the amount of energy the player has is changed.
    public event Action OnEnergyChange;

    /// Adds an enemy to the battle. This method is automatically called by Enemy.
    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        enemy.UpdateActionIndicatorWrapper();
    }

    /// Removes an enemy from the battle. This method is automatically called by Enemy.
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

    /// Shuffles the draw pile and invokes OnShuffle
    private void ShuffleDraw()
    {
        for (int index = 0; index < drawPile.Count; index++)
        {
            int swapIndex = UnityEngine.Random.Range(0, drawPile.Count);
            (drawPile[index], drawPile[swapIndex]) = (drawPile[swapIndex], drawPile[index]);
        }
        OnShuffle?.Invoke();
    }

    /// Moves the top card of the draw pile to the player's hand. Invokes OnDraw.
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

    /// Draws <c>quantity</c> cards, triggering OnDraw after each card is drawn.
    public void DrawCards(int quantity)
    {
        for (int card = 0; card < quantity; card++)
        {
            DrawCard();
        }
    }

    /// Adds a card to the discard pile. If the card was in the hand, removes that card from the
    /// hand. If not, an error is written to the console, but no exception is thrown.
    public void DiscardCard(Card card)
    {
        if (!hand.Remove(card))
        {
            Debug.LogError("A card was Discard()ed without being in the current hand:", card);
        }
        discardPile.Add(card);
        OnDiscard?.Invoke(card);
    }

    /// Subtracts the given amount of energy. If this results in energy being negative, an error
    /// is written to the console but no exception is thrown.
    public void SpendEnergy(int amount)
    {
        if (amount > _energy)
        {
            Debug.LogError("More energy was spent than we currently have!");
        }
        _energy -= amount;
        OnEnergyChange?.Invoke();
    }

    /// Does all the necessary stuff to end the player's turn:
    /// 1. Moves all cards in the hand to the discard pile, invoking OnDiscard every time.
    /// 2. Removes block from all enemies.
    /// 3. Enemies perform their intended attacks or actions.
    /// 4. A new hand is drawn. (OnDraw is invoked for every card drawn.)
    /// 5. Player's energy is restored.
    public void EndTurn()
    {
        // We have to iterate manually here because every time we call Discard(), an item will
        // be removed from the list which would make a foreach loop choke.
        for (int index = hand.Count; index > 0; index--) {
            DiscardCard(hand[index - 1]);
        }

        foreach (Enemy enemy in enemies) {
            enemy.ClearBlock();
        }

        foreach (Enemy enemy in enemies) {
            enemy.DoAttackWrapper();
        }

        foreach (Enemy enemy in enemies) {
            enemy.UpdateActionIndicatorWrapper();
        }

        Player player = Player.instance;
        player.ClearBlock();
        _energy = player.maxEnergy;
        OnEnergyChange?.Invoke();
        DrawCards(5);
    }

    /// Performs everything necessary to start a new battle. The draw pile will be set to the
    /// contents of the deck. Five cards will be drawn from the pile, invoking OnDraw each time.
    public void NewBattle()
    {
        OnReset.Invoke();
        drawPile = new List<Card>(Deck.instance.GetCards());
        ShuffleDraw();
        hand.Clear();
        discardPile.Clear();

        _energy = Player.instance.maxEnergy;
        OnEnergyChange?.Invoke();
        DrawCards(5);
    }
}
