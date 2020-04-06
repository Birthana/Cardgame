using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    //Idea for connecting events:
    //Connect paths from left to right. Progress by each Y coord of events.
    //Prevent overlapping, if overlap or no empty events to connect, connect to same event.

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

    private int numY = 3;   //Number of Y coords to generate and spawn an event at.
    private int minPerY = 3;    //Minimum number of events to generate for every Y coord used.
    private int maxPerY = 4;    //Maximum number of events to generate for every Y coord used.
    private bool bottomPathsFilled = false;
    private bool topPathsFilled = false;
    private bool curPathsFilled = false;

    public GameObject enemyEncounter;
    public GameObject town;
    public GameObject line;
    
    private void Start()
    {
        otherPositions = new Vector3[numY][];
        otherObjects = new GameObject[numY][];

        CreateEndpoints();
        Fill();
        EnsureAngles();

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

    //Ensures (and if necessary, corrects) angles between the bottomPositions and
    //the first row of map locations are not too close, which would cause paths
    //to be too close together. Does same thing for top position and last row of
    //map locations.
    private void EnsureAngles()
    {
        Vector3 curVector;
        float curAngle;
        Vector3 vectorToCompare;
        float angleToCompare;

        //////////////Check bottomPositions angles.
        for (int i = 0; i < otherPositions[0].Length; i++)
        {
            curVector = otherPositions[0][i] - bottomPosition;
            curAngle = Mathf.Atan2(curVector.y, curVector.x) * Mathf.Rad2Deg;

            for (int j = i + 1; j < otherPositions[0].Length; j++)
            {
                vectorToCompare = otherPositions[0][j] - bottomPosition;
                angleToCompare = Mathf.Atan2(vectorToCompare.y, vectorToCompare.x) * Mathf.Rad2Deg;

                if (Mathf.Abs(curAngle - angleToCompare) < 10f)
                {
                    Vector3 newPosition = otherObjects[0][j].transform.position + new Vector3(0f, 3f, 0f);

                    otherObjects[0][j].transform.position = newPosition;
                    otherPositions[0][j] = newPosition;
                }
            }
        }

        curVector = otherPositions[0][otherPositions[0].Length - 1] - bottomPosition;
        curAngle = Mathf.Atan2(curVector.y, curVector.x) * Mathf.Rad2Deg;
        vectorToCompare = otherPositions[0][otherPositions[0].Length - 2] - bottomPosition;
        angleToCompare = Mathf.Atan2(vectorToCompare.y, vectorToCompare.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(curAngle - angleToCompare) < 10f)
        {
            Vector3 newPosition = otherObjects[0][otherPositions[0].Length - 1].transform.position 
                + new Vector3(0f, 3f, 0f);

            otherObjects[0][otherPositions[0].Length - 1].transform.position = newPosition;
            otherPositions[0][otherPositions[0].Length - 1] = newPosition;
        }
        //////////////

        //////////////Check topPositions angles.
        for (int i = 0; i < otherPositions[otherPositions.Length - 1].Length; i++)
        {
            curVector = otherPositions[otherPositions.Length - 1][i] - topPosition;
            curAngle = Mathf.Atan2(curVector.y, curVector.x) * Mathf.Rad2Deg;

            for (int j = i + 1; j < otherPositions[otherPositions.Length - 1].Length; j++)
            {
                vectorToCompare = otherPositions[otherPositions.Length - 1][j] - topPosition;
                angleToCompare = Mathf.Atan2(vectorToCompare.y, vectorToCompare.x) * Mathf.Rad2Deg;

                if (Mathf.Abs(curAngle - angleToCompare) < 10f)
                {
                    Vector3 newPosition = otherObjects[otherPositions.Length - 1][j].transform.position
                        - new Vector3(0f, 3f, 0f);

                    otherObjects[otherPositions.Length - 1][j].transform.position = newPosition;
                    otherPositions[otherPositions.Length - 1][j] = newPosition;
                }
            }
        }

        curVector = otherPositions[otherPositions.Length - 1][otherPositions[otherPositions.Length - 1].Length - 1] 
            - topPosition;
        curAngle = Mathf.Atan2(curVector.y, curVector.x) * Mathf.Rad2Deg;
        vectorToCompare = otherPositions[otherPositions.Length - 1][otherPositions[otherPositions.Length - 1].Length - 2]
            - topPosition;
        angleToCompare = Mathf.Atan2(vectorToCompare.y, vectorToCompare.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(curAngle - angleToCompare) < 10f)
        {
            Vector3 newPosition = 
                otherObjects[otherPositions.Length - 1][otherPositions[otherPositions.Length - 1].Length - 1].transform.position
                - new Vector3(0f, 3f, 0f);

            otherObjects[otherPositions.Length - 1][otherPositions[otherPositions.Length - 1].Length - 1].transform.position = 
                newPosition;
            otherPositions[otherPositions.Length - 1][otherPositions[otherPositions.Length - 1].Length - 1] = newPosition;
        }
        ////////////
    }

    private void GeneratePath(int curIndex)
    {
        int startIndex = curIndex;
        float distMod = -1.75f;

        curIndex = Mathf.Min(startIndex, otherPositions[0].Length - 1);

        GameObject curObject;
        GameObject curLine;
        Vector3 curVector = otherPositions[0][curIndex] - bottomPosition;
        float distance = curVector.magnitude;
        float angle = Mathf.Atan2(curVector.y, curVector.x) * Mathf.Rad2Deg;

        if (startIndex > otherPositions[0].Length - 1)
        {
            bottomPathsFilled = true;
        }

        //Generates paths by spawning line in middle of two events, then scaling up.
        //Generate line from bottomObject to curObject.
        if (!bottomPathsFilled)
        {
            curLine = Instantiate(line, new Vector3(
                bottomPosition.x + (curVector.x / 2),
                bottomPosition.y + (curVector.y / 2), 0), Quaternion.Euler(0f, 0f, angle));
            curLine.transform.localScale = new Vector3(distance + distMod, 1f, 1f);
        }
        curObject = otherObjects[0][curIndex];

        //Generate lines for filler objects.
        for (int i = 1; i < numY; i++)
        {
            curIndex = Mathf.Min(startIndex, otherPositions[i].Length - 1);
            if (startIndex > otherPositions[i].Length - 1 
                && otherPositions[i].Length - 1 == otherPositions[i - 1].Length - 1)
            {
                curPathsFilled = true;
            }
            else
            {
                curPathsFilled = false;
            }
            curVector = otherPositions[i][curIndex] - curObject.transform.position;
            distance = curVector.magnitude;
            angle = Mathf.Atan2(curVector.y, curVector.x) * Mathf.Rad2Deg;

            if (!curPathsFilled)
            {
                curLine = Instantiate(line, new Vector3(
                    curObject.transform.position.x + (curVector.x / 2),
                    curObject.transform.position.y + (curVector.y / 2), 0), Quaternion.Euler(0f, 0f, angle));
                curLine.transform.localScale = new Vector3(distance + distMod, 1f, 1f);
            }
            curObject = otherObjects[i][curIndex];
        }

        curIndex = Mathf.Min(startIndex, otherPositions[numY - 1].Length - 1);
        if (startIndex > otherPositions[numY - 1].Length - 1)
        {
            topPathsFilled = true;
        }

        //Generate line from curObject to topObject.
        if (!topPathsFilled)
        {
            curVector = topPosition - curObject.transform.position;
            distance = curVector.magnitude;
            angle = Mathf.Atan2(curVector.y, curVector.x) * Mathf.Rad2Deg;

            curLine = Instantiate(line, new Vector3(
                curObject.transform.position.x + (curVector.x / 2),
                curObject.transform.position.y + (curVector.y / 2), 0), Quaternion.Euler(0f, 0f, angle));
            curLine.transform.localScale = new Vector3(distance + distMod, 1f, 1f);
        }
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
        Vector3 angleToOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        angleToOffset.Normalize();
        Vector3 convertedPosition = Camera.main.WorldToScreenPoint(position);
        Vector3 offsetPosition = Camera.main.ScreenToWorldPoint(
            convertedPosition + (Random.Range(0f, EVENT_OFFSET) * angleToOffset));

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
