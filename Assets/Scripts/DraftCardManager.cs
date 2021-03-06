﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftCardManager : MonoBehaviour
{
    public CardAvatar cardAvatarPrefab;
    public List<Card> cardPool;
    public int chooseFromNumber;
    public float CARD_SCALE = 1.2f;
    public float CARD_SPACING = 500.0f;
    [SerializeField] private List<Card> draftableCards = new List<Card>();
    [SerializeField] private List<CardAvatar> cardAvatars = new List<CardAvatar>();
    private bool drafting;

    private void Start()
    {
        //test
        Draft();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (drafting)
            {
                var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D mouseHit = Physics2D.Raycast(mouseRay.origin, Vector2.zero);
                if (mouseHit)
                {
                    CardAvatar chosenCard = mouseHit.collider.gameObject.GetComponent<CardAvatar>();
                    if (CheckIsDraftableCard(chosenCard))
                    {
                        Debug.Log("Add to deck: " + chosenCard.displaying);
                        Deck.instance.Add(chosenCard.displaying);
                        drafting = false;
                        foreach (CardAvatar card in cardAvatars)
                        {
                            Destroy(card.gameObject);
                        }
                        cardAvatars.Clear();
                    }
                }
            }
        }
    }

    public bool CheckIsDraftableCard(CardAvatar chosenCard)
    {
        bool isDraftableCard = false;
        if (chosenCard == null)
            return false;
        foreach (CardAvatar card in cardAvatars)
        {
            if (card == chosenCard)
                isDraftableCard = true;
        }
        return isDraftableCard;
    }

    public void Draft()
    {
        drafting = true;
        for (int i = 0; i < chooseFromNumber; i++)
        {
            ChooseFromCardPool();
        }
        DisplayDraftCards();
    }

    public void ChooseFromCardPool()
    {
        bool isUnique = false;
        while (!isUnique)
        {
            int randomNumber = Random.Range(0, cardPool.Count);
            Card draftCard = cardPool[randomNumber];
            isUnique = CheckDraftCardUniqueness(draftCard);
            if (isUnique)
                draftableCards.Add(draftCard);
        }
    }

    public bool CheckDraftCardUniqueness(Card draftCard)
    {
        bool isUnique = true;
        foreach (Card card in draftableCards)
        {
            if (card == draftCard)
                isUnique = false;
        }
        return isUnique;
    }

    public void DisplayDraftCards()
    {
        for (int i = 0; i < draftableCards.Count; i++)
        {
            //Adds to child of gameobject
            CardAvatar avatar = Instantiate(cardAvatarPrefab, this.transform);
            avatar.displaying = draftableCards[i];
            cardAvatars.Add(avatar);

            float transformAmount = ((float)i) - ((float)draftableCards.Count - 1) / 2;
            float angle = transformAmount * 3.0f;
            Vector3 position = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad),
                0,
                0
                ) * CARD_SPACING;
            avatar.transform.localPosition = position;
            avatar.transform.localScale = new Vector3(CARD_SCALE, CARD_SCALE, CARD_SCALE);
        }
    }
}
