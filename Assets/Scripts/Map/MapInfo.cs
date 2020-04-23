using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo
{
    public static MapInfo _instance = null;
    private List<Event> mapEvents;
    private Event[][] otherEvents;
    private GameObject currentMapIcon;
    private Event currentEvent;

    public static MapInfo instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MapInfo();
            }
            return _instance;
        }
    }

    public void SetMapInfo(List<Event> mapEvents, Event[][] otherEvents, GameObject currentMapIcon, Event currentEvent)
    {
        this.mapEvents = mapEvents;
        this.otherEvents = otherEvents;
        this.currentMapIcon = currentMapIcon;
        this.currentEvent = currentEvent;
    }

    public List<Event> GetMapEvents()
    {
        return mapEvents;
    }

    public Event[][] GetOtherEvents()
    {
        return otherEvents;
    }

    public GameObject GetCurrentMapIcon()
    {
        return currentMapIcon;
    }

    public Event GetCurrentEvent()
    {
        return currentEvent;
    }
}
