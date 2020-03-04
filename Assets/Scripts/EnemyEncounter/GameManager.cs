using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyManager))]
[RequireComponent(typeof(EnergyManager))]
[RequireComponent(typeof(CardManager))]
public class GameManager : MonoBehaviour
{
    private List<Card> deck;
    [SerializeField] private Health playerHealth;
    [SerializeField] private EnemyManager enemies;
    [SerializeField] private EnergyManager energy;
    [SerializeField] private CardManager cards;

    private void Start()
    {
        GetManagerComponents();
        SetPlayerInfo();
        playerHealth.SetMaxHealth(PlayerInformation.GetHealth());
        enemies.SpawnEnemies();
        energy.StartEnergy();
        cards.StartEncounter();
    }

    private void GetManagerComponents()
    {
        playerHealth = GetComponent<Health>(); 
        enemies = GetComponent<EnemyManager>(); 
        energy = GetComponent<EnergyManager>();
        cards = GetComponent<CardManager>();
    }

    private void SetPlayerInfo()
    {
        deck = PlayerInformation.GetDeck();
    }
    public void StartTurn()
    {
        energy.StartEnergy();
        cards.DrawHand();
    }

    public void EndTurn()
    {
        StartCoroutine(EndingTurn());
    }

    IEnumerator EndingTurn()
    {
        cards.GetComponent<LayoutManager>().RemoveAll();
        yield return StartCoroutine(enemies.Attacking());
        playerHealth.RemoveAllBlock();
        StartTurn();
    }
}
