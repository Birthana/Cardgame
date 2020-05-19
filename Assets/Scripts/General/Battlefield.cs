using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Represents the player during a battle.
public class Battlefield : MonoBehaviour
{
    private static Battlefield _instance = null;
    public static Battlefield instance { get => _instance; }

    [Tooltip("How much extra space should seperate the player and the enemies.")]
    public float PLAYER_ENEMY_GAP = 16;

    public GameObject[] playerPortalOrigins = new GameObject[0], enemyPortalOrigins = new GameObject[0];

    void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("More than one object was instantiated with the Battlefield behavior!");
            Destroy(this);
        }
        DoLayout();
        BattleManager.instance.OnFieldChange += DoLayout;
    }

    void OnDisable()
    {
        _instance = null;
    }

    /// Places player and enemy avatars in their proper places.
    public void DoLayout()
    {
        float currentOffset = PLAYER_ENEMY_GAP;
        foreach (Enemy enemy in BattleManager.instance.enemies)
        {
            Vector3 oldScale = enemy.transform.localScale;
            enemy.transform.parent = this.transform;
            enemy.transform.localScale = oldScale;

            enemy.transform.localPosition = Vector3.zero + Vector3.right * (currentOffset + enemy.WIDTH / 2);
            currentOffset += enemy.WIDTH;
        }

        int index = 0;
        if (BattleManager.instance.friendlyPortal != null)
        {
            Portal portal = BattleManager.instance.friendlyPortal;
            Vector3 oldScale = portal.transform.localScale;
            portal.transform.parent = playerPortalOrigins[index].transform;
            portal.transform.localScale = oldScale;
            portal.transform.localPosition = Vector3.zero;
            index++;
        }
        if (BattleManager.instance.enemyPortal != null)
        {
            Portal portal = BattleManager.instance.enemyPortal;
            Vector3 oldScale = portal.transform.localScale;
            portal.transform.parent = enemyPortalOrigins[index].transform;
            portal.transform.localScale = oldScale;
            portal.transform.localPosition = Vector3.zero;
            index++;
        }

        Player player = Player.instance;
        if (player != null)
        {
            Vector3 oldScale = player.transform.localScale;
            player.transform.parent = this.transform;
            player.transform.localScale = oldScale;
            player.transform.localPosition = Vector3.zero - Vector3.right * (player.WIDTH / 2);
        }
    }

    private Vector3 gizPoint(float x, float y)
    {
        return transform.TransformVector(Vector3.right * x + Vector3.up * y) + transform.position;
    }

    private void DrawGizmosImpl()
    {
        Gizmos.DrawLine(gizPoint(0, 0), gizPoint(0, 20));
        Gizmos.DrawLine(gizPoint(PLAYER_ENEMY_GAP, 0), gizPoint(PLAYER_ENEMY_GAP, 20));
        Gizmos.DrawLine(gizPoint(0, 0), gizPoint(-20, 0));
        Gizmos.DrawLine(gizPoint(0, 0), gizPoint(60 + PLAYER_ENEMY_GAP, 0));
        foreach (GameObject o in playerPortalOrigins)
        {
            if (o == null) continue;
            Gizmos.DrawWireSphere(o.transform.position, o.transform.lossyScale.x * 7);
        }
        foreach (GameObject o in enemyPortalOrigins)
        {
            if (o == null) continue;
            Gizmos.DrawWireSphere(o.transform.position, o.transform.lossyScale.x * 7);
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
