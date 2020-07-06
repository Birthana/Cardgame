using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPanel : MonoBehaviour
{
    public Status statusPrefab;
    private List<Status> statuses = new List<Status>();
    public enum StatusType { WEAK, VUL, THIRD }
    public Sprite[] statusSprites;
    public Vector3 OFFSET;
    private FieldEntity entity;

    private void OnDisable()
    {
        if (entity != null)
            entity.OnStatusChange -= ChangeStatusUI;
    }

    public void SetEntity(FieldEntity newEntitty)
    {
        entity = newEntitty;
        entity.OnStatusChange += ChangeStatusUI;
        transform.position = newEntitty.transform.position - OFFSET;
    }

    public void ChangeStatusUI(StatusType statusType, int turns)
    {
        if (StatusExists(statusType))
        {
            Status currentStatus = GetStatus(statusType);
            currentStatus.SetTurnCount(turns);
            if (turns <= 0)
            {
                statuses.Remove(currentStatus);
                Destroy(currentStatus.gameObject);
            }
        }
        else
        {
            if (turns <= 0)
                return;
            Status newStatus = Instantiate(statusPrefab, this.transform);
            newStatus.SetStatusImage(statusSprites[(int)statusType]);
            newStatus.SetTurnCount(turns);
            statuses.Add(newStatus);
        }
        DisplayStatusUI();
    }

    public bool StatusExists(StatusType statusType)
    {
        return GetStatus(statusType) != null;
    }

    public Status GetStatus(StatusType statusType)
    {
        Status result = null;
        foreach (Status currentStatus in statuses)
        {
            if (currentStatus.GetSprite().Equals(statusSprites[(int)statusType]))
            {
                result = currentStatus;
                break;
            }
        }
        return result;
    }

    public void DisplayStatusUI()
    {

    }
}
