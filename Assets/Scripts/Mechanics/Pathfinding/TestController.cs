using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{
    public PathNode currentNode;
    public List<PathNode> path = new List<PathNode>();
    public PathNode destinationNode;  // Reference to the target destination node
    private bool hasReachedDestination = false;

    private void Update()
    {
        if (currentNode != destinationNode)
        {
            CreatePath();
        }
    }

    // Method to set the destination for the NPC
    public void SetDestination(PathNode newDestination)
    {
        destinationNode = newDestination;
        path = AStarManager.instance.GeneratePath(currentNode, destinationNode); // Generate the path to the new destination
        hasReachedDestination = false; // Reset the flag when setting a new destination
    }

    public void CreatePath()
    {
        if (path == null) path = new List<PathNode>();

        if (path.Count > 0)
        {
            int x = 0;
            // Move towards the current node in the path
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), 3 * Time.deltaTime);

            if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
            {
                // Update the current node and remove it from the path
                currentNode = path[x];
                path.RemoveAt(x);

                // If the destination is reached, stop moving
                if (currentNode == destinationNode)
                {
                    hasReachedDestination = true;
                    Debug.Log("Reached the destination.");
                }
            }
        }
        else
        {
            // If the path is empty, generate a new one (if the destination is set)
            if (destinationNode != null && currentNode != null)
            {
                path = AStarManager.instance.GeneratePath(currentNode, destinationNode);
            }
        }
    }
}
