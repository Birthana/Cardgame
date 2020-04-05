using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    //WARNING: Script does not yet have safety checks
    //If EVENT_SPACING is too large, or numToGenerate is too big, script may result in an infinite loop.
    //Maybe implement a better way to generate positions?
    //For example, make sure position is within acceptable range at generation time,
    //so no need for checking for space.
    //Also might want to make elipse from top and bottom similar to gif in discord.
    //To do this, could generate y first, and then figure out acceptable bounds for x.
    //
    //Idea for connecting events:
    //Connect paths from left to right. Progress by each Y coord of events.
    //Prevent overlapping, if overlap or no empty events to connect, connect to same event.

    private float SCREEN_HORIZONTAL_SPACING = 60f;  //Minimum spacing between sides of screen and events.
    private float SCREEN_VERTICAL_SPACING = 15f;  //Minimum spacing between top and bottom of screen and events.
    private float EVENT_SPACING = 15f;  //Minimum spacing between each event.

    private Vector3 bottomPosition;
    private Vector3 topPosition;
    private List<Vector3> otherPositions = new List<Vector3>();
    private int numY = 3;   //Number of Y coords to generate and spawn an event at.
    private int minPerY = 1;    //Minimum number of events to generate for every Y coord used.
    private int maxPerY = 2;    //Maximum number of events to generate for every Y coord used.

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
        float newY;
        float newX;
        float verticalSpacing = (topPosition.y - bottomPosition.y) / (numY + 1);

        //Used to calculate horizontal spacing between events.
        Vector3 leftMostPosition = Camera.main.ScreenToWorldPoint(new Vector3(
            SCREEN_HORIZONTAL_SPACING, 0));
        Vector3 rightMostPosition = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width - SCREEN_HORIZONTAL_SPACING, 0));
        float horizontalSpacing;

        int numX;

        for (int i = 1; i <= numY; i++)
        {
            numX = Random.Range(minPerY, maxPerY + 1);
            for (int j = 1; j <= numX; j++)
            {
                horizontalSpacing = Vector3.Distance(leftMostPosition, rightMostPosition) / (numX + 1);
                newY = bottomPosition.y + (i * verticalSpacing);
                /*newPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                    Random.Range(SCREEN_HORIZONTAL_SPACING, Screen.width - SCREEN_HORIZONTAL_SPACING),
                    0));*/
                newX = leftMostPosition.x + (j * horizontalSpacing);
                newPosition = new Vector3(newX, newY, 0);
                /*newPosition.y = newY;
                newPosition.x = newX;
                newPosition.z = 0;*/
                Instantiate(enemyEncounter, newPosition, Quaternion.identity);
            }
        }

        /*
        Vector3 newPosition;
        Vector3 temp;

        for (int i = 0; i < numToGenerate; i++)
        {
            int counter = 0;
            do
            {
                temp = Camera.main.ScreenToWorldPoint(new Vector3(
                    Random.Range(SCREEN_HORIZONTAL_SPACING, Screen.width - SCREEN_HORIZONTAL_SPACING),
                    Random.Range(SCREEN_VERTICAL_SPACING + EVENT_SPACING, 
                    Screen.height - SCREEN_VERTICAL_SPACING - EVENT_SPACING)));
                newPosition = new Vector3(temp.x, temp.y, 0);

                counter++;
                if (counter > 10)
                {
                    Debug.LogError("Infinite loop?");
                    break;
                }
            } while (!EnoughSpacing(newPosition));

            otherPositions.Add(newPosition);
            Instantiate(enemyEncounter, newPosition, Quaternion.identity);
        }*/
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
