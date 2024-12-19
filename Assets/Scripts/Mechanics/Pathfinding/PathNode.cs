using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public PathNode cameFrom;
    public List<PathNode> connections;
    public bool isBlocked = false;
    public float gScore;
    public float hScore;

    public float FScore()
    {
        return gScore + hScore;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (connections.Count > 0 )
        {
            for ( int i = 0; i < connections.Count; i++ )
            {
                Gizmos.DrawLine(transform.position, connections[i].transform.position);
            }
        }
    }
}
