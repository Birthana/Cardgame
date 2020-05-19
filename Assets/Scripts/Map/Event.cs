using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public Vector3 position;
    public List<Vector3> connectedEvents;

    public Event(Vector3 position)
    {
        this.position = position;
        connectedEvents = new List<Vector3>();
    }

    public Event()
    {
        position = Vector3.zero;
        connectedEvents = new List<Vector3>();
    }
}
