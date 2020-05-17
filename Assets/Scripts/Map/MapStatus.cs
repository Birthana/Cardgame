using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStatus
{
    public static MapStatus _instance = null;
    private List<MapEvent> mapEvents;
    private MapEvent[][] otherEvents;
    private GameObject currentMapIcon;
    private MapEvent currentEvent;

    public static MapStatus instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MapStatus();
            }
            return _instance;
        }
    }

    public void SetMapStatus(List<MapEvent> mapEvents, MapEvent[][] otherEvents, GameObject currentMapIcon, MapEvent currentEvent)
    {
        this.mapEvents = mapEvents;
        this.otherEvents = otherEvents;
        this.currentMapIcon = currentMapIcon;
        this.currentEvent = currentEvent;
    }

    public List<MapEvent> GetMapEvents()
    {
        return mapEvents;
    }

    public MapEvent[][] GetOtherEvents()
    {
        return otherEvents;
    }

    public GameObject GetCurrentMapIcon()
    {
        return currentMapIcon;
    }

    public MapEvent GetCurrentEvent()
    {
        return currentEvent;
    }
}
