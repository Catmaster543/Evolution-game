using NUnit.Framework;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class Splinebuilder : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float interval = 0.6f;

    private List<Waypoint> waypoints = new();

    private void Start()
    {
        if (splineContainer == null || splineContainer.Splines.Count == 0)
        {
            Debug.LogError("SplineBuilder: spline is null or empty list.");
            enabled = false;
            return;
        }
        BuildWaypoints();
    }

    private void BuildWaypoints()
    {
        waypoints.Clear();

        float length = splineContainer.CalculateLength();
        Debug.Log($"SplineBuilder: spline legth={length:F2}, interval={interval}");

        float distance = 0f;
        while (distance < length)
        {
            float3 splinePosition = splineContainer.EvaluatePosition(distance / length);
            waypoints.Add(new Waypoint(new Vector2(splinePosition.x, splinePosition.y)));
            distance += interval;
        }
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count == 0) { return; }

        Gizmos.color = Color.yellow;
        foreach (var wp in waypoints)
        {
            Gizmos.DrawSphere(wp.Position, 0.15f);
        }
    }
}
