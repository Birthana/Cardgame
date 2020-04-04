using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject enemyEncounter;
    public GameObject town;

    private Vector3 bottomPosition;
    private Vector3 topPosition;
    
    private void Start()
    {
        CreateEndpoints();
    }

    private void CreateEndpoints()
    {
        bottomPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0));
        Instantiate(enemyEncounter, new Vector3(bottomPosition.x, bottomPosition.y, 0), Quaternion.identity);

        topPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height));
        Instantiate(enemyEncounter, new Vector3(topPosition.x, topPosition.y, 0), Quaternion.identity);
    }
}
