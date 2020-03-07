using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public RandomEventSuperclass[] randomEvents;

    public void PickEvents()
    {
        randomEvents[Random.Range(0, randomEvents.Length)].StartEvent();
    }
}
