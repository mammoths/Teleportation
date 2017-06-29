using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{

    LineRenderer lr;

    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetColor(Color color)
    {
        lr.startColor = color;
        lr.endColor = color;
    }

    public void SetWaypoints(Vector3[] waypoints)
    {

        lr.positionCount = waypoints.Length;
        lr.SetPositions(waypoints);
    }
}
