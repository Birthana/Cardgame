﻿using System.Collections;
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
    //
    //TODO: Make sure angle from bottomPosition to first event is not the same angle for
    //the bottomPosition to another event. Same for topPosition.

    private float SCREEN_HORIZONTAL_SPACING = 60f;  //Minimum spacing between sides of screen and events.
    private float SCREEN_VERTICAL_SPACING = 15f;  //Minimum spacing between top and bottom of screen and events.
    private float EVENT_OFFSET = 25f;  //Maximum offset applied to each event.

    private Vector3 bottomPosition;
    private Vector3 topPosition;
    private Vector3[][] otherPositions;  //Array of other positions. otherPositions[Y Positions][X Positions]
    //Each correspond with above declared position variables.
    private GameObject bottomObject;
    private GameObject topObject;
    private GameObject[][] otherObjects;
    //private List<Vector3> otherPositions = new List<Vector3>();
    private int numY = 3;   //Number of Y coords to generate and spawn an event at.
    private int minPerY = 5;    //Minimum number of events to generate for every Y coord used.
    private int maxPerY = 5;    //Maximum number of events to generate for every Y coord used.

    public GameObject enemyEncounter;
    public GameObject town;
    public GameObject line;
    
    private void Start()
    {
        otherPositions = new Vector3[numY][];
        otherObjects = new GameObject[numY][];

        CreateEndpoints();
        Fill();

        for (int i = 0; i < maxPerY; i++)
        {
            GeneratePath(i);
        }
    }

    //Create top and bottom events.
    private void CreateEndpoints()
    {
        bottomPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2,
            SCREEN_VERTICAL_SPACING));
        bottomObject = Instantiate(enemyEncounter, new Vector3(bottomPosition.x, 
            bottomPosition.y, 0), Quaternion.identity);

        topPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 
            Screen.height - SCREEN_VERTICAL_SPACING));
        topObject = Instantiate(enemyEncounter, new Vector3(topPosition.x, 
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

            //Creates jagged array based on number of X vals used.
            otherPositions[i - 1] = new Vector3[numX];
            otherObjects[i - 1] = new GameObject[numX];

            for (int j = 1; j <= numX; j++)
            {
                horizontalSpacing = Vector3.Distance(leftMostPosition, rightMostPosition) / (numX + 1);
                newY = bottomPosition.y + (i * verticalSpacing);
                /*newPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                    Random.Range(SCREEN_HORIZONTAL_SPACING, Screen.width - SCREEN_HORIZONTAL_SPACING),
                    0));*/
                newX = leftMostPosition.x + (j * horizontalSpacing);
                //newPosition = new Vector3(newX, newY, 0);
                newPosition = Offset(new Vector3(newX, newY, 0));
                /*newPosition.y = newY;
                newPosition.x = newX;
                newPosition.z = 0;*/
                otherPositions[i - 1][j - 1] = newPosition;
                otherObjects[i - 1][j - 1] = Instantiate(enemyEncounter, newPosition, Quaternion.identity);
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

    private void GeneratePath(int curIndex)
    {
        float distMod = -1.75f;

        GameObject curObject;
        GameObject curLine;
        Vector3 curVector = otherPositions[0][curIndex] - bottomPosition;
        float distance = curVector.magnitude;
        float angle = Mathf.Atan2(curVector.y, curVector.x) * Mathf.Rad2Deg;

        //Generates paths by spawning line in middle of two events, then scaling up.
        //Generate line from bottomObject to curObject.
        curLine = Instantiate(line, new Vector3(
            bottomPosition.x + (curVector.x / 2), 
            bottomPosition.y + (curVector.y / 2), 0), Quaternion.Euler(0f, 0f, angle));
        curLine.transform.localScale = new Vector3(distance + distMod, 1f, 1f);
        curObject = otherObjects[0][curIndex];

        //Generate lines for filler objects.
        for (int i = 1; i < numY; i++)
        {
            curVector = otherPositions[i][curIndex] - curObject.transform.position;
            distance = curVector.magnitude;
            angle = Mathf.Atan2(curVector.y, curVector.x) * Mathf.Rad2Deg;

            curLine = Instantiate(line, new Vector3(
                curObject.transform.position.x + (curVector.x / 2),
                curObject.transform.position.y + (curVector.y / 2), 0), Quaternion.Euler(0f, 0f, angle));
            curLine.transform.localScale = new Vector3(distance + distMod, 1f, 1f);
            curObject = otherObjects[i][curIndex];
        }

        //Generate line from curObject to topObject.
        curVector = topPosition - curObject.transform.position;
        distance = curVector.magnitude;
        angle = Mathf.Atan2(curVector.y, curVector.x) * Mathf.Rad2Deg;

        curLine = Instantiate(line, new Vector3(
            curObject.transform.position.x + (curVector.x / 2),
            curObject.transform.position.y + (curVector.y / 2), 0), Quaternion.Euler(0f, 0f, angle));
        curLine.transform.localScale = new Vector3(distance + distMod, 1f, 1f);
    }

    /*private void GeneratePath(int index)
    {
        int curIndex = 0;
        //int curIndex = Random.Range(0, otherPositions[0].Length);
        GameObject curObject;

        //Generate line from bottomObject to curObject.
        bottomObject.GetComponent<LineRenderer>().SetPosition(0, bottomPosition);
        /*bottomObject.GetComponent<LineRenderer>().SetPosition(1,
            otherPositions[0][curIndex]);
        bottomObject.GetComponent<LineRenderer>().SetPosition(1, otherPositions[0][index]);
        curObject = otherObjects[0][curIndex];

        //Generate lines for filler objects.
        for (int i = 1; i < numY; i++)
        {
            curObject.GetComponent<LineRenderer>().SetPosition(0, curObject.transform.position);
            //curIndex = Random.Range(0, otherPositions[i].Length);
            //curObject.GetComponent<LineRenderer>().SetPosition(1, otherPositions[i][curIndex]);
            curObject.GetComponent<LineRenderer>().SetPosition(1, otherPositions[i][index]);
            curObject = otherObjects[i][index];
        }

        //Generate line from curObject to topObject.
        curObject.GetComponent<LineRenderer>().SetPosition(0, curObject.transform.position);
        curObject.GetComponent<LineRenderer>().SetPosition(1, topPosition);
    }*/

    private Vector3 Offset(Vector3 position)
    {
        Vector3 angle = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        angle.Normalize();
        Vector3 convertedPosition = Camera.main.WorldToScreenPoint(position);
        Vector3 offsetPosition = Camera.main.ScreenToWorldPoint(
            convertedPosition + (Random.Range(0f, EVENT_OFFSET) * angle));

        return offsetPosition;
    }

    //Returns true if a position has enough spacing from other already used.
    /*private bool EnoughSpacing(Vector3 curPosition)
    {
        bool enough = true;

        for (int i = 0; i < otherPositions.Count; i++)
        {
            if ((curPosition.x >= otherPositions[i].x - EVENT_SPACING
                && curPosition.x <= otherPositions[i].x + EVENT_SPACING) ||
                (curPosition.y >= otherPositions[i].y - EVENT_SPACING
                && curPosition.y <= otherPositions[i].y + EVENT_SPACING))
                {

                }
            if (Vector3.Distance(curPosition, otherPositions[i]) < EVENT_OFFSET)
            {
                enough = false;
            }
        }

        return enough;
    }*/
}
