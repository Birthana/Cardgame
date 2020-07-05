using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatusManager : MonoBehaviour
{
    public static BattleStatusManager instance = null;
    public StatusPanel statusPanelPrefab;
    private StatusPanel playerStatusPanel;
    private List<StatusPanel> enemyStatusPanels = new List<StatusPanel>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Bind(FieldEntity entity)
    {
        if (entity is Player)
        {
            if (playerStatusPanel == null)
                playerStatusPanel = Instantiate(statusPanelPrefab, this.transform);
            playerStatusPanel.SetEntity(entity);
        }
        else if (entity is Enemy)
        {
            StatusPanel newEnemyStatusPanel = Instantiate(statusPanelPrefab, this.transform);
            newEnemyStatusPanel.SetEntity(entity);
            enemyStatusPanels.Add(newEnemyStatusPanel);
        }
    }
}
