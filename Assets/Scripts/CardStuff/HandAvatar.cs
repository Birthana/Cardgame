using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAvatar : MonoBehaviour
{
    public CardAvatar cardAvatarPrefab;
    public Card[] cards;
    public float CARD_WIDTH, CARD_HEIGHT;
    public float CARD_SPACING = 2.0f;
    public float CARD_ROTATION = 5.0f;
    private List<CardAvatar> avatars = new List<CardAvatar>();

    void Start()
    {
        foreach (Card card in cards)
        {
            CardAvatar avatar = Instantiate(cardAvatarPrefab, this.transform);
            avatar.displaying = card;
            avatar.OnHover += Emphasize;
            avatar.OnBlur += Deemphasize;
            avatars.Add(avatar);
        }
        UpdateAvatarTransforms();
    }

    private void Emphasize(CardAvatar avatar)
    {
        Debug.Log("Emphasize");
    }

    private void Deemphasize(CardAvatar avatar)
    {
        Debug.Log("Deemphasize");
    }

    private void UpdateAvatarTransforms()
    {
        for (int index = 0; index < avatars.Count; index++)
        {
            float transformAmount = ((float)index) - ((float)avatars.Count - 1) / 2.0f;
            float angle = transformAmount * CARD_ROTATION;
            Vector3 position = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad),
                Mathf.Cos(angle * Mathf.Deg2Rad) - 1,
                0
            ) * CARD_SPACING;

            CardAvatar avatar = avatars[index];
            avatar.transform.localPosition = position;
            avatar.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.back);
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
