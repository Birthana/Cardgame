using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    private float SCREEN_HORIZONTAL_SPACING = 60f;  //Minimum spacing between sides of screen and events.
    private float SCREEN_VERTICAL_SPACING = 15f;  //Minimum spacing between top and bottom of screen and events.
    private float EVENT_SPACING = 5f;  //Minimum spacing between each event.

    private Vector3 bottomPosition;
    private Vector3 topPosition;
    private List<Vector3> otherPositions = new List<Vector3>();
    private int numToGenerate = 10;

    public GameObject enemyEncounter;
    public GameObject town;
    
    private void Start()
    {
        CreateEndpoints();
        Fill();
    }

    //Create top and bottom events.
    private void CreateEndpoints()
    {
        bottomPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2,
            SCREEN_VERTICAL_SPACING));
        Instantiate(enemyEncounter, new Vector3(bottomPosition.x, 
            bottomPosition.y, 0), Quaternion.identity);

        topPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 
            Screen.height - SCREEN_VERTICAL_SPACING));
        Instantiate(enemyEncounter, new Vector3(topPosition.x, 
            topPosition.y, 0), Quaternion.identity);
    }

    //Fill screen with events.
    private void Fill()
    {
        Vector3 newPosition;
        Vector3 temp;

        for (int i = 0; i < numToGenerate; i++)
        {
            do
            {
                temp = Camera.main.ScreenToWorldPoint(new Vector3(
                    Random.Range(SCREEN_HORIZONTAL_SPACING, Screen.width - SCREEN_HORIZONTAL_SPACING),
                    Random.Range(SCREEN_VERTICAL_SPACING,Screen.height - SCREEN_VERTICAL_SPACING)));
                newPosition = new Vector3(temp.x, temp.y, 0);
            } while (!EnoughSpacing(newPosition));

            otherPositions.Add(newPosition);
            Instantiate(enemyEncounter, newPosition, Quaternion.identity);
        }
    }

    //Returns true if a position has enough spacing from other already used.
    private bool EnoughSpacing(Vector3 curPosition)
    {
        bool enough = true;

        for (int i = 0; i < otherPositions.Count; i++)
        {
            /*if ((curPosition.x >= otherPositions[i].x - EVENT_SPACING
                && curPosition.x <= otherPositions[i].x + EVENT_SPACING) ||
                (curPosition.y >= otherPositions[i].y - EVENT_SPACING
                && curPosition.y <= otherPositions[i].y + EVENT_SPACING))
                {

                }*/
            if (Vector3.Distance(curPosition, otherPositions[i]) < EVENT_SPACING)
            {
                enough = false;
            }
        }

        return enough;
    }
}
