using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAvatar : MonoBehaviour
{
    public CardAvatar cardAvatarPrefab;
    public float CARD_WIDTH, CARD_HEIGHT;
    public float CARD_SPACING = 400.0f;
    public float CARD_ROTATION = 3.0f;
    public float EMPHASIZED_SCALE = 1.8f;

    private List<Card> cards = new List<Card>();
    private List<CardAvatar> cardAvatars = new List<CardAvatar>();
    private CardAvatar emphasized, waitingToBeEmphasized;

    void OnEnable()
    {
        Deck.instance.OnDraw += MakeCardAvatar;
        Deck.instance.OnDiscard += RemoveCardAvatar;
    }

    void OnDisable()
    {
        Deck.instance.OnDraw -= MakeCardAvatar;
        Deck.instance.OnDiscard -= RemoveCardAvatar;
    }

    private void MakeCardAvatar(Card forCard)
    {
        CardAvatar avatar = Instantiate(cardAvatarPrefab, this.transform);
        avatar.displaying = forCard;
        avatar.OnHover += Emphasize;
        avatar.OnBlur += Deemphasize;
        cards.Add(forCard);
        cardAvatars.Add(avatar);
        UpdateAvatarTransforms();
    }

    private void RemoveCardAvatar(Card forCard)
    {
        int index = cards.IndexOf(forCard);
        if (index == -1) return;
        // This doesn't have any negative effects if the card wasn't emphasized in the first place.
        Deemphasize(cardAvatars[index]);
        cards.RemoveAt(index);
        cardAvatars.RemoveAt(index);
        UpdateAvatarTransforms();
    }

    private void Emphasize(CardAvatar avatar)
    {
        if (emphasized == null)
        {
            emphasized = avatar;
            UpdateAvatarTransforms();
        }
        else
        {
            waitingToBeEmphasized = avatar;
        }
    }

    private void Deemphasize(CardAvatar avatar)
    {
        if (avatar == emphasized)
        {
            emphasized = waitingToBeEmphasized;
            UpdateAvatarTransforms();
        }
        else if (avatar == waitingToBeEmphasized)
        {
            waitingToBeEmphasized = null;
        }
    }

    private void UpdateAvatarTransforms()
    {
        int emphasizedIndex = cardAvatars.IndexOf(emphasized);
        // Handles both cases of `emphasized` being null and of being something not in the list.
        bool somethingIsEmphasized = emphasizedIndex > -1;
        for (int index = 0; index < cardAvatars.Count; index++)
        {
            float transformAmount = ((float)index) - ((float)cardAvatars.Count - 1) / 2.0f;
            if (somethingIsEmphasized)
            {
                if (index < emphasizedIndex)
                {
                    transformAmount -= 0.2f;
                }
                else if (index > emphasizedIndex)
                {
                    transformAmount += 0.2f;
                }
            }
            float angle = transformAmount * CARD_ROTATION;
            Vector3 position = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad),
                Mathf.Cos(angle * Mathf.Deg2Rad) - 1,
                // The emphasized card should render on top of everything else.
                index == emphasizedIndex ? 0 : 1
            ) * CARD_SPACING;

            CardAvatar avatar = cardAvatars[index];
            avatar.transform.localPosition = position;
            avatar.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.back);

            Vector3 scale;
            if (index == emphasizedIndex)
            {
                scale = new Vector3(EMPHASIZED_SCALE, EMPHASIZED_SCALE, EMPHASIZED_SCALE);
            }
            else
            {
                scale = new Vector3(1, 1, 1);
            }
            avatar.transform.localScale = scale;
        }
    }

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
        Gizmos.color = Color.yellow;
        DrawGizmosImpl();
    }
}
