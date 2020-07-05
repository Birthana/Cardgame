using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPanel : MonoBehaviour
{
    public Status statusPrefab;
    private List<Status> statuses = new List<Status>();
    public enum StatusType { WEAK, VUL, THIRD }
    public Sprite[] statusSprites;
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
        //Set transform below the field entities transform.
    }

    public void ChangeStatusUI(StatusType statusType, int turns)
    {
        Status newStatus = Instantiate(statusPrefab, this.transform);
        newStatus.SetStatusImage(statusSprites[(int)statusType]);
        newStatus.SetTurnCount(turns);
        DisplayStatusUI();
    }

    public void DisplayStatusUI()
    {

    }
}
