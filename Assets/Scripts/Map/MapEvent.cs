using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEvent
{
    public enum Type { EnemyEncounter, Town, Boss, Other};

    public Type type;
    public Vector3 position;
    public GameObject correspondingObject;
    public string sceneToLoad;
    public List<Vector3> connectedPositions;
    public List<MapEvent> connectedEvents;

    public MapEvent(Vector3 position, Type type, GameObject correspondingObject)
    {
        this.position = position;
        this.type = type;
        this.correspondingObject = correspondingObject;
        
        connectedPositions = new List<Vector3>();
        connectedEvents = new List<MapEvent>();

        if (type == Type.EnemyEncounter)
        {
            sceneToLoad = "EnemyEncounter";
        }
        else if (type == Type.Town)
        {
            sceneToLoad = "Town2";
        }
        else if (type == Type.Boss)
        {
            sceneToLoad = "Boss";
        }
    }

    public MapEvent(Vector3 position, GameObject correspondingObject)
    {
        type = Type.Other;
        this.position = position;
        this.correspondingObject = correspondingObject;
        sceneToLoad = null;
        connectedPositions = new List<Vector3>();
        connectedEvents = new List<MapEvent>();
    }

    public MapEvent()
    {
        type = Type.Other;
        position = Vector3.zero;
        correspondingObject = null;
        sceneToLoad = null;
        connectedPositions = new List<Vector3>();
        connectedEvents = new List<MapEvent>();
    }
}
