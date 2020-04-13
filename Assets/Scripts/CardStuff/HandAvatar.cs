using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// The on-screen representation of the cards currently in the player's hands.
public class HandAvatar : MonoBehaviour
{
    [Tooltip("A prefab with a CardAvatar component to be used to represent all cards in the hand.")]
    public CardAvatar cardAvatarPrefab;
    public LineRenderer linePrefab;
    [Tooltip("Approximate width and height of cardAvatarPrefab.")]
    public float CARD_WIDTH, CARD_HEIGHT;
    [Tooltip("Radius of the circule to lay out cards with.")]
    public float CARD_SPACING = 400.0f;
    [Tooltip("Number of degrees to separate each card by.")]
    public float CARD_ROTATION = 3.0f;
    [Tooltip("How much to scale up a card by when the player is hovering over it.")]
    public float EMPHASIZED_SCALE = 1.8f;
    [Tooltip("How much to scale up a card by when the player has selected it and is about to play it.")]
    public float SELECTED_SCALE = 2.1f;

    private List<Card> cards = new List<Card>();
    private List<CardAvatar> cardAvatars = new List<CardAvatar>();
    // Selected = a card the user has clicked on and is about to play. The user can put it back by
    // clicking on it again or by clicking on another card to select.
    private CardAvatar selected = null;
    private Card.TargetMode selectionMode;
    // This is set to true when some kind of animation is playing, e.g. when a card is being played
    // or when a new hand is being dealt. It prevents the player from interacting with any of the 
    // cards.
    private bool ignoreInteraction = false;

    void OnEnable()
    {
        BattleManager.instance.OnReset += Reset;
        BattleManager.instance.OnDraw += MakeCardAvatar;
        BattleManager.instance.OnDiscard += RemoveCardAvatar;
    }

    void OnDisable()
    {
        BattleManager.instance.OnReset -= Reset;
        BattleManager.instance.OnDraw -= MakeCardAvatar;
        BattleManager.instance.OnDiscard -= RemoveCardAvatar;
    }

    private ComponentClass GetUnderCursor<ComponentClass>()
    {
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D mouseHit = Physics2D.Raycast(mouseRay.origin, Vector2.zero);
        if (!mouseHit)
        {
            return default(ComponentClass);
        }
        return mouseHit.collider.gameObject.GetComponent<ComponentClass>();
    }

    private CardAvatar GetHovered() {
        return GetUnderCursor<CardAvatar>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !ignoreInteraction)
        {
            var hovered = GetHovered();
            // The user clicked the mouse while a card was already selected.
            if (selected != null)
            {
                // The user clicked the selected card, deselect it.
                if (selected == hovered) Deselect(selected);
                // The user clicked on another card. Select that one instead.
                else if (hovered != null) Select(hovered);
                else
                {
                    // The user clicked outside the hand avatar. Try to play the card. If the
                    // card could not be played, deselect it instead.
                    bool success = TryPlay();
                    if (!success) Deselect(selected);
                }
            }
            else if (hovered != null)
            {
                // If a card is emphasized (enlarged because hovered), then select it.
                Select(hovered);
            }

            // Update the current selection mode based on how the selected card targets things.
            if (selected != null)
            {
                int cardIndex = cardAvatars.IndexOf(selected);
                selectionMode = cards[cardIndex].targetMode;
            }
        }
        UpdateAvatarTransforms();
    }

    /// This sets and unsets ignoreInteraction based on the provided coroutine / iterator, setting
    /// it before it starts and unsetting it once it ends.
    private IEnumerator WrapCardAnimation(IEnumerator animCoroutine)
    {
        ignoreInteraction = true;
        yield return animCoroutine;
        ignoreInteraction = false;
        foreach (Enemy enemy in BattleManager.instance.enemies)
        {
            // The card just played may have affected the numbers on an enemy's action indicator.
            enemy.UpdateActionIndicatorWrapper();
        }
        yield break;
    }

    /// Tries to play the currently selected card. If the currently selected card could not be
    /// played (usually because it requries selecting an enemy but no enemy was under the mouse)
    /// then false will be returned and the card's effects will not be played.
    private bool TryPlay()
    {
        int cardIndex = cardAvatars.IndexOf(selected);
        if (cardIndex < 0) return false;
        bool success = false;
        Card card = cards[cardIndex];
        if (selectionMode == Card.TargetMode.AllEnemies)
        {
            // Cast all the enemies to generic FieldEntitys.
            StartCoroutine(WrapCardAnimation(
                card.Play(BattleManager.instance.enemies.ConvertAll(enemy => (FieldEntity)enemy))
            ));
            success = true;
        }
        else if (selectionMode == Card.TargetMode.Player)
        {
            StartCoroutine(WrapCardAnimation(card.Play(Player.instance)));
            success = true;
        }
        else if (selectionMode == Card.TargetMode.SpecificEnemy)
        {
            var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D mouseHit = Physics2D.Raycast(mouseRay.origin, Vector2.zero);
            if (mouseHit)
            {
                Enemy possibleEnemy = mouseHit.collider.gameObject.GetComponent<Enemy>();
                if (possibleEnemy)
                {
                    StartCoroutine(WrapCardAnimation(card.Play(possibleEnemy)));
                    success = true;
                }
            }
        }

        if (success)
        {
            BattleManager.instance.DiscardCard(card);
        }
        return success;
    }

    /// Clears out all existing card avatars and resets all helper variables. Effectively makes
    /// the hand render as if it is empty.
    private void Reset()
    {
        foreach (CardAvatar avatar in cardAvatars)
        {
            Destroy(avatar.gameObject);
        }
        cards.Clear();
        cardAvatars.Clear();
        selected = null;
    }

    /// Creates a new avatar to be the visual representation of the given card.
    private void MakeCardAvatar(Card forCard)
    {
        CardAvatar avatar = Instantiate(cardAvatarPrefab, this.transform);
        avatar.displaying = forCard;
        cards.Add(forCard);
        cardAvatars.Add(avatar);
        UpdateAvatarTransforms();
    }

    /// Removes the visual representation of the given card.
    private void RemoveCardAvatar(Card forCard)
    {
        int index = cards.IndexOf(forCard);
        if (index == -1) return;
        Deselect(cardAvatars[index]);
        Destroy(cardAvatars[index].gameObject);
        cards.RemoveAt(index);
        cardAvatars.RemoveAt(index);
        UpdateAvatarTransforms();
    }

    /// Select a card as about to be played. In this state, if the user clicks on a valid target,
    /// the selected card will be played.
    private void Select(CardAvatar avatar)
    {
        int index = cardAvatars.IndexOf(avatar);
        int energyRequired = cards[index].level;
        if (cards[index].CanBePlayed())
        {
            selected = avatar;
        }
        else
        {
            Debug.Log("TODO: Tell the player they can't play the card for whatever reason.");
        }
    }

    /// Deselects the given card, if it is currently selected.
    private void Deselect(CardAvatar avatar)
    {
        if (selected == avatar)
        {
            selected = null;
        }
    }

    /// Reposition all the card avatars, taking into account their number as well as which one
    /// is emphasized or selected (if any.)
    private void UpdateAvatarTransforms()
    {
        int emphasizedIndex = cardAvatars.IndexOf(GetHovered());
        if (emphasizedIndex >= 0 && !cards[emphasizedIndex].CanBePlayed()) {
            emphasizedIndex = -1;
        }
        int selectedIndex = cardAvatars.IndexOf(selected);
        // Handles both cases of `emphasized` being null and of being something not in the list.
        bool somethingIsEmphasized = emphasizedIndex > -1;
        bool somethingIsSelected = selectedIndex > -1;

        // The split between emphasized / selected makes the logic smoother elsewhere at the cost
        // of making this update function a little more complex.
        bool somethingIsBig = somethingIsEmphasized || somethingIsSelected;
        int bigCardIndex = somethingIsSelected ? selectedIndex : emphasizedIndex;
        for (int index = 0; index < cardAvatars.Count; index++)
        {
            float transformAmount = ((float)index) - ((float)cardAvatars.Count - 1) / 2.0f;
            if (somethingIsSelected && !ignoreInteraction)
            {
                // Make all the other cards act like the selected card isn't in the hand.
                if (index < selectedIndex)
                {
                    transformAmount += 0.5f;
                }
                else if (index > selectedIndex)
                {
                    transformAmount -= 0.5f;
                }
                else
                {
                    // Selected card should appear dead-center.
                    transformAmount = 0.0f;
                }
            }

            float angle = transformAmount * CARD_ROTATION;
            Vector3 position = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad) * CARD_SPACING,
                (Mathf.Cos(angle * Mathf.Deg2Rad) - 1) * CARD_SPACING,
                // The big card should render on top of everything else.
                index == bigCardIndex ? -2 : -1
            );
            float scale;
            if (index == bigCardIndex && !ignoreInteraction)
            {
                scale = somethingIsSelected ? SELECTED_SCALE : EMPHASIZED_SCALE;
            }
            else
            {
                scale = 1;
            }

            if (somethingIsSelected && index != selectedIndex && !ignoreInteraction)
            {
                position.y -= CARD_HEIGHT / 2;
            }

            CardAvatar avatar = cardAvatars[index];
            avatar.transform.localPosition = position;
            avatar.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.back);
            avatar.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    /// Draw an in-editor gizmo representing the position a card may occupy.
    private void DrawGizmoCard(Vector3 center, float angle)
    {
        // half of width, height
        float hw = CARD_WIDTH / 2;
        float h = CARD_HEIGHT;
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1.0f));
        // top left, top right, bottom left, bottom right
        Vector3 tl = transform.TransformVector(rotation * new Vector3(-hw, h, 0) + center) + transform.position;
        Vector3 tr = transform.TransformVector(rotation * new Vector3(hw, h, 0) + center) + transform.position;
        Vector3 bl = transform.TransformVector(rotation * new Vector3(-hw, 0, 0) + center) + transform.position;
        Vector3 br = transform.TransformVector(rotation * new Vector3(hw, 0, 0) + center) + transform.position;
        Gizmos.DrawLine(tl, tr);
        Gizmos.DrawLine(tr, br);
        Gizmos.DrawLine(br, bl);
        Gizmos.DrawLine(bl, tl);
    }

    /// Draw an in-editor gizmo showing a possible layout of five cards given the parameters entered
    /// into the inspector.
    private void DrawGizmosImpl()
    {
        for (float offset = -2.0f; offset <= 2.0f; offset += 1.0f)
        {
            float angle = offset * CARD_ROTATION;
            Vector3 position = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad),
                Mathf.Cos(angle * Mathf.Deg2Rad) - 1,
                0
            ) * CARD_SPACING;
            DrawGizmoCard(position, -angle);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        DrawGizmosImpl();
    }

    void OnDrawGizmosSelected()
    {
        // Draw the gizmo in a yellow color to indicate the component is selected.
        Gizmos.color = Color.yellow;
        DrawGizmosImpl();
    }
}
