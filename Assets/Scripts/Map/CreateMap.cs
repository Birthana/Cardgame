using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateMap : MonoBehaviour
{
    public float SCREEN_HORIZONTAL_SPACING = 60f;
    public float SCREEN_VERTICAL_SPACING = 30f;
    public float EVENT_OFFSET = 25f;

    public int maxRows;
    public int minCols;
    public int maxCols;

    public GameObject enemyEncounter;
    public GameObject town;
    public GameObject boss;
    public GameObject start;
    public GameObject line;

    private List<MapEvent> mapEvents;
    private MapEvent[][] otherEvents;

    public GameObject currentMapIcon;
    public MapEvent currentEvent;

    public MapEvent selectedEvent;

    private void Start()
    {
        if (MapStatus.instance.GetMapEvents() == null)
        {
            mapEvents = new List<MapEvent>();
            otherEvents = new MapEvent[maxRows][];
            CreateEndPoints();
            Fill();
            ConnectEvents();
        }
        else
        {
            mapEvents = MapStatus.instance.GetMapEvents();
            otherEvents = MapStatus.instance.GetOtherEvents();
            currentMapIcon = MapStatus.instance.GetCurrentMapIcon();
            currentEvent = MapStatus.instance.GetCurrentEvent();
            RespawnEvents();
        }
        ActivateAnim();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D mouseHit = Physics2D.Raycast(mouseRay.origin, Vector2.zero);
            if (mouseHit)
            {
                GameObject hitObject = mouseHit.collider.gameObject;
                if (CheckIsEvent(hitObject))
                {
                    if (CheckIsConnected())
                    {
                        currentMapIcon = hitObject;
                        currentEvent = selectedEvent;
                        MapStatus.instance.SetMapStatus(mapEvents, otherEvents, currentMapIcon, currentEvent);
                        SceneManager.LoadScene(selectedEvent.sceneToLoad);
                    }
                }
            }
        }
    }

    public void RespawnEvents()
    {
        GameObject newEvent = Instantiate(start, this.transform);
        newEvent.transform.localPosition = mapEvents[0].position;
        mapEvents[0].correspondingObject = newEvent;
        foreach (Vector3 linePosition in mapEvents[0].connectedPositions)
        {
            RespawnLine(mapEvents[0].position, linePosition);
        }
        newEvent = Instantiate(boss, this.transform);
        newEvent.transform.localPosition = mapEvents[1].position;
        mapEvents[1].correspondingObject = newEvent;
        for (int i = 2; i < mapEvents.Count; i++)
        {
            if (mapEvents[i].type == MapEvent.Type.EnemyEncounter)
            {
                newEvent = Instantiate(enemyEncounter, this.transform);
            }
            else if (mapEvents[i].type == MapEvent.Type.Town)
            {
                newEvent = Instantiate(town, this.transform);
            }

            newEvent.transform.localPosition = mapEvents[i].position;
            mapEvents[i].correspondingObject = newEvent;
            foreach (Vector3 linePosition in mapEvents[i].connectedPositions)
            {
                RespawnLine(mapEvents[i].position, linePosition);
            }
        }
    }

    public void RespawnLine(Vector3 a, Vector3 b)
    {
        Vector3 difference = b - a;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Vector3 linePosition = new Vector3(
            a.x + (difference.x / 2),
            a.y + (difference.y / 2),
            0);
        GameObject newLine = Instantiate(line, linePosition, Quaternion.Euler(0f, 0f, angle));
        newLine.transform.localScale = new Vector3(difference.magnitude - 1.75f, 1f, 1f);
        newLine.transform.SetParent(this.transform);
    }

    public bool CheckIsEvent(GameObject hitObject)
    {
        bool result = false;
        for (int i = 0; i < mapEvents.Count; i++)
        {
            if (hitObject.transform.position.Equals(mapEvents[i].position))
            {
                selectedEvent = mapEvents[i];
                result = true;
            }
        }
        return result;
    }

    public bool CheckIsConnected()
    {
        return currentEvent.connectedPositions.Contains(selectedEvent.position);
    }

    public void CreateEndPoints()
    {
        Vector3 bottomPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2,
            SCREEN_VERTICAL_SPACING));
        GameObject bottom = Instantiate(start, this.transform);
        bottom.transform.localPosition = new Vector3(bottomPosition.x,
            bottomPosition.y, 0);
        MapEvent startEvent = new MapEvent(bottom.transform.localPosition, bottom);

        Vector3 topPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2,
            Screen.height - SCREEN_VERTICAL_SPACING));
        GameObject top = Instantiate(boss, this.transform);
        top.transform.localPosition = new Vector3(topPosition.x,
            topPosition.y, 0);
        MapEvent bossEvent = new MapEvent(top.transform.localPosition, MapEvent.Type.Boss, top);

        mapEvents.Add(startEvent);
        mapEvents.Add(bossEvent);

        currentMapIcon = bottom;
        currentEvent = startEvent;
    }

    public void Fill()
    {
        for (int i = 0; i < maxRows; i++)
        {
            int numberOfCols = Random.Range(minCols, maxCols + 1);
            otherEvents[i] = new MapEvent[numberOfCols];
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
                GameObject tempEvent;
                MapEvent.Type type;

                if (i > 0 && Random.Range(0, 6) == 0)
                {
                    tempEvent = Instantiate(town, this.transform);
                    type = MapEvent.Type.Town;
                }
                else
                {
                    tempEvent = Instantiate(enemyEncounter, this.transform);
                    type = MapEvent.Type.EnemyEncounter;
                }
                
                tempEvent.transform.localPosition = new Vector3(
                    leftMostPosition.x + ((j + 1) * horizontalSpacing),
                    bottomPosition.y + ((i + 1) * verticalSpacing),
                    0);
                MapEvent newEvent = new MapEvent(tempEvent.transform.localPosition, type, tempEvent);
                otherEvents[i][j] = newEvent;
                mapEvents.Add(newEvent);
            }
        }
    }

    public void ConnectEvents()
    {
        ConnectStartEvents(mapEvents[0], 0);
        ConnectBossEvents(mapEvents[1], otherEvents.Length - 1);

        for (int i = 0; i < otherEvents.Length - 1; i++)
        {
            for (int j = 0; j < otherEvents[i].Length; j++)
            {
                int nextIndex = Mathf.Min(j, otherEvents[i + 1].Length - 1);
                SpawnLine(otherEvents[i][j], otherEvents[i + 1][nextIndex]);
                RandomConnectEvents(i, j);
            }

            // If next row is longer than current row, connect all remaining cols 
            // in the next row to the last col of current row.
            if (otherEvents[i].Length < otherEvents[i + 1].Length)
            {
                for (int k = otherEvents[i].Length; k < otherEvents[i + 1].Length; k++)
                {
                    SpawnLine(otherEvents[i][otherEvents[i].Length - 1], otherEvents[i + 1][k]);
                }
            }
        }
    }

    private void ConnectStartEvents(MapEvent endPoint, int eventRow)
    {
        for (int i = 0; i < otherEvents[eventRow].Length; i++)
        {
            SpawnLine(endPoint, otherEvents[eventRow][i]);
        }
    }

    private void ConnectBossEvents(MapEvent endPoint, int eventRow)
    {
        for (int i = 0; i < otherEvents[eventRow].Length; i++)
        {
            SpawnLine(otherEvents[eventRow][i], endPoint);
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
                if (rng == 0)
                {
                    SpawnLine(otherEvents[row][col], otherEvents[row + 1][col + 1]);
                }
                else
                {
                    SpawnLine(otherEvents[row][col], otherEvents[row + 1][col - 1]);
                }
            }
            else if (col > 0 && col == otherEvents[row + 1].Length - 1)
            {
                SpawnLine(otherEvents[row][col], otherEvents[row + 1][col - 1]);
            }
        }
    }

    public void SpawnLine(MapEvent a, MapEvent b)
    {
        Vector3 difference = b.position - a.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Vector3 linePosition = new Vector3(
            a.position.x + (difference.x / 2),
            a.position.y + (difference.y / 2),
            0);
        GameObject newLine = Instantiate(line, linePosition, Quaternion.Euler(0f, 0f, angle));
        newLine.transform.localScale = new Vector3(difference.magnitude - 1.75f, 1f, 1f);
        newLine.transform.SetParent(this.transform);
        a.connectedPositions.Add(b.position);
        a.connectedEvents.Add(b);
    }

    private void ActivateAnim()
    {
        foreach (MapEvent connected in currentEvent.connectedEvents)
        {
            connected.correspondingObject.GetComponent<Animation>().Play();
        }
    }
}
