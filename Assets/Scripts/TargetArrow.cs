using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    public static TargetArrow instance = null;
    public GameObject linePrefab;
    public Vector3 startPosition;
    public Vector3 arrowPosition;

    private GameObject line;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SetStartPosition(Vector3 position)
    {
        if (line == null)
            SpawnLine(position);
        startPosition = position;
        line.GetComponent<LineRenderer>().SetPosition(0, position);
    }

    public void SetArrowPosition(Vector3 position)
    {
        arrowPosition = position;
        line.GetComponent<LineRenderer>().SetPosition(1, position);
    }

    public void SpawnLine(Vector3 position)
    {
        line = GameObject.Instantiate(linePrefab, position, Quaternion.identity);
    }

    public void DestroyLine()
    {
        Destroy(line);
        line = null;
    }
}
