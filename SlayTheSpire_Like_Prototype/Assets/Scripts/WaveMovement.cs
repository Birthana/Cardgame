using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public GameObject testMarker;
    public float maxHorizontalDistance;
    public float maxDegree;
    public float horizontalSpeed;
    public float verticalSpeed;
    public float timeSpeed;
    public float travelSpeed;
    public float maxAmp;
    public float lowestAmpOffset;
    public List<Vector3> maxAmpPositions;
    public LineRenderer line;
    [SerializeField] private Vector3 startPosition;

    private void Start()
    {
        startPosition = this.transform.position;
    }

    private void Update()
    {
        if (Mathf.Clamp(Mathf.Sin(2 * Mathf.PI * Mathf.Deg2Rad * Mathf.PingPong(Time.time * travelSpeed, maxDegree) * timeSpeed), 0.9f, 0.99f) == 0.99f)
        {
            /**
            if (!this.GetComponent<Animator>().GetBool("IsOpen"))
            {
                this.GetComponent<Animator>().SetBool("IsOpen", true);
            }
            */
            //Instantiate(testMarker, this.transform.position, Quaternion.identity);
            //Debug.Log("Max Amp");
            maxAmpPositions.Add(this.transform.position);
            line.positionCount = maxAmpPositions.Count;
            line.SetPositions(maxAmpPositions.ToArray());
        }
        else
        {
            /**
            if (this.GetComponent<Animator>().GetBool("IsOpen"))
            {
                this.GetComponent<Animator>().SetBool("IsOpen", false);
            }
            */
        }
        this.transform.position = startPosition + new Vector3(
                Mathf.PingPong(Time.time * horizontalSpeed, maxHorizontalDistance),
                //(Mathf.PingPong(Time.time * verticalSpeed, maxAmp) + lowestAmpOffset) * Mathf.Sin(2 * Mathf.PI * Mathf.Deg2Rad * Mathf.PingPong(Time.time * travelSpeed, maxDegree) * timeSpeed),
                (Mathf.PingPong(Time.time * verticalSpeed, maxAmp) + lowestAmpOffset) * Mathf.Cos(2 * Mathf.PI * Mathf.Deg2Rad * Mathf.PingPong(Time.time * travelSpeed, maxDegree) * timeSpeed + 0.25f * timeSpeed),
                0.0f
            );
    }
}
