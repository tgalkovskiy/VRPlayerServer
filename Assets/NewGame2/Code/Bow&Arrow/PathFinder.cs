using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    // Visualize the road.
    [Header("Track Visualization")]
    public Color lineColor;
    public float NodeSize = 0.5f;

    public List<PointIndicator> nodes = new List<PointIndicator>();

    // This function Draw the track with the patrol points.
    void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

        PointIndicator[] pathTransforms = GetComponentsInChildren<PointIndicator>();
        nodes = new List<PointIndicator>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].transform.position;
            Vector3 previousNode = Vector3.zero;

            if (i > 0)
                previousNode = nodes[i - 1].transform.position;

            else if (i == 0 && nodes.Count > 1)
                previousNode = nodes[nodes.Count - 1].transform.position;

            Gizmos.DrawLine(previousNode, currentNode);
            Gizmos.DrawWireSphere(currentNode, NodeSize);
        }
    }
}
