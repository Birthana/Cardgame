using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public float SCREEN_HORIZONTAL_SPACING = 60f;
    public float SCREEN_VERTICAL_SPACING = 15f;
    public float EVENT_OFFSET = 25f;

    public int maxRows;
    public int minCols;
    public int maxCols;

    public GameObject enemyEncounter;
    public GameObject town;
    public GameObject boss;
    public GameObject start;
    public GameObject line;

    private List<GameObject> mapEvents;
    private GameObject[][] otherEvents;

    private void Start()
    {
        mapEvents = new List<GameObject>();
        otherEvents = new GameObject[maxRows][];
        CreateEndPoints();
        Fill();
        ConnectEvents();
    }

    public void CreateEndPoints()
    {
        Vector3 bottomPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2,
            SCREEN_VERTICAL_SPACING));
        GameObject bottom = Instantiate(start, this.transform);
        bottom.transform.localPosition = new Vector3(bottomPosition.x, 
            bottomPosition.y, 0);

        Vector3 topPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2,
            Screen.height - SCREEN_VERTICAL_SPACING));
        GameObject top = Instantiate(boss, this.transform);
        top.transform.localPosition = new Vector3(topPosition.x, 
            topPosition.y, 0);

        mapEvents.Add(bottom);
        mapEvents.Add(top);
    }

    public void Fill()
    {
        for (int i = 0; i < maxRows; i++)
        {
            int numberOfCols = Random.Range(minCols, maxCols + 1);
            otherEvents[i] = new GameObject[numberOfCols];
            Vector3 leftMostPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                SCREEN_HORIZONTAL_SPACING, 0));
            Vector3 rightMostPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                Screen.width - SCREEN_HORIZONTAL_SPACING, 0));
            Vector3 bottomPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                Screen.width / 2, SCREEN_VERTICAL_SPACING));
            Vector3 topPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                Screen.width / 2, Screen.height - SCREEN_VERTICAL_SPACING));
            float horizontalSpacing = Vector3.Distance(leftMostPosition, rightMostPosition) / (numberOfCols + 1);
            float verticalSpacing = (topPosition.y - bottomPosition.y) / (maxRows + 1);
            for (int j = 0; j < numberOfCols; j++)
            {
                GameObject tempEvent = Instantiate(enemyEncounter, this.transform);
                tempEvent.transform.localPosition = new Vector3(
                    leftMostPosition.x + ((j + 1) * horizontalSpacing),
                    bottomPosition.y + ((i + 1) * verticalSpacing),
                    0);
                otherEvents[i][j] = tempEvent;
            }
        }
    }

    public void ConnectEvents()
    {
        ConnectEndEvents(mapEvents[0], 0);
        ConnectEndEvents(mapEvents[1], otherEvents.Length - 1);

        for (int i = 0; i < otherEvents.Length - 1; i++)
        {
            for (int j = 0; j < otherEvents[i].Length; j++)
            {
                int nextIndex = Mathf.Min(j, otherEvents[i + 1].Length - 1);
                SpawnLine(otherEvents[i][j], otherEvents[i + 1][nextIndex]);
                RandomConnectEvents(i, j);
            }

            if (otherEvents[i].Length < otherEvents[i + 1].Length)
            {
                for (int k = otherEvents[i].Length; k < otherEvents[i + 1].Length; k++)
                {
                    SpawnLine(otherEvents[i][otherEvents[i].Length - 1], otherEvents[i + 1][k]);
                }
            }
        }
    }

    private void ConnectEndEvents(GameObject endPoint, int eventRow)
    {
        for (int i = 0; i < otherEvents[eventRow].Length; i++)
        {
            SpawnLine(endPoint, otherEvents[eventRow][i]);
        }
    }

    public void RandomConnectEvents(int row, int col)
    {
        int rng = Random.Range(0, 5);
        if (rng == 0)
        {
            if (col == 0 && otherEvents[row + 1].Length > 1)
            {
                SpawnLine(otherEvents[row][col], otherEvents[row + 1][col + 1]);
            }
            else if (col > 0 && col < otherEvents[row + 1].Length - 1)
            {
                rng = Random.Range(0, 2);
                if(rng == 0)
                {
                    SpawnLine(otherEvents[row][col], otherEvents[row + 1][col + 1]);
                }
                else
                {
                    SpawnLine(otherEvents[row][col], otherEvents[row + 1][col - 1]);
                }
            }
            else if(col > 0 && col == otherEvents[row + 1].Length - 1)
            {
                SpawnLine(otherEvents[row][col], otherEvents[row + 1][col - 1]);
            }
        }
    }

    public void SpawnLine(GameObject a, GameObject b)
    {
        Vector3 difference = b.transform.position - a.transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Vector3 linePosition = new Vector3(
            a.transform.position.x + (difference.x / 2),
            a.transform.position.y + (difference.y / 2),
            0);
        GameObject newLine = Instantiate(line, linePosition, Quaternion.Euler(0f, 0f, angle));
        newLine.transform.localScale = new Vector3(difference.magnitude - 1.75f, 1f, 1f);
        newLine.transform.SetParent(this.transform);
    }
}
