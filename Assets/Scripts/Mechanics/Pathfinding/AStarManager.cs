using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AStarManager : MonoBehaviour
{
    public static AStarManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    public List<PathNode> GeneratePath(PathNode start, PathNode end)
    {
        Debug.Log("Creating path");
        List<PathNode> openSet = new List<PathNode>();

        foreach (PathNode n in FindObjectsOfType<PathNode>())
        {
            n.gScore = float.MaxValue;
        }

        start.gScore = 0;
        start.hScore = Vector2.Distance(start.transform.position, end.transform.position);
        openSet.Add(start);

        while (openSet.Count > 0)
        {
            int lowestF = default;

            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].FScore() < openSet[lowestF].FScore())
                {
                    lowestF = i;
                }
            }

            PathNode currentNode = openSet[lowestF];
            openSet.Remove(currentNode);

            if (currentNode == end)
            {
                List<PathNode> path = new List<PathNode>();

                path.Insert(0, end);

                while (currentNode != start)
                {
                    currentNode = currentNode.cameFrom;
                    path.Add(currentNode);
                }

                path.Reverse();
                return path;
            }

            foreach (PathNode connectedNode in currentNode.connections)
            {
                // Check if the connected node is not blocked
                if (connectedNode.isBlocked)
                    continue; // Skip this node if it's blocked

                float heldGScore = currentNode.gScore + Vector2.Distance(currentNode.transform.position, connectedNode.transform.position);

                if (heldGScore < connectedNode.gScore)
                {
                    connectedNode.cameFrom = currentNode;
                    connectedNode.gScore = heldGScore;
                    connectedNode.hScore = Vector2.Distance(connectedNode.transform.position, end.transform.position);

                    if (!openSet.Contains(connectedNode))
                        openSet.Add(connectedNode);
                }
            }
        }

        return null; // If no path is found
    }

}
