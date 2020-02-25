using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverable : MonoBehaviour
{
    private GameObject tempCard;

    private void OnMouseEnter()
    {
        tempCard = Instantiate(this.gameObject, this.transform.position + new Vector3(10, 20, -9), Quaternion.identity);
        tempCard.transform.localScale = new Vector3(1.5f, 1.5f, 0);
        //tempCard.GetComponent<Draggable>().AddLayers(10);
        tempCard.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnMouseExit()
    {
        Destroy(tempCard);
    }

    private void OnDestroy()
    {
        Destroy(tempCard);
    }
}
