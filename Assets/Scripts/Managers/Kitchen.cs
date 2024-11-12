using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    [SerializeField] private Transform[] cookingSpawnNode;
    [SerializeField] private float cookingTime = 5f;

    public (bool areAllOccupied, Transform node) Serve()
    {
        for (int i = 0; i < cookingSpawnNode.Length; i++)
        {
            Transform cookingSpawnNodeTransform = cookingSpawnNode[i];

            // Grabs the Seat script from the seat
            OrderNode node = cookingSpawnNodeTransform.GetComponent<OrderNode>();

            if (node != null)
            {
                if (node.isOccupied)
                {
                    Debug.Log(node.name + " is occupied.");
                }
                else
                {
                    Debug.Log(node.name + " is available.");
                    // Return false and the available seat Transform
                    return (false, cookingSpawnNodeTransform); // 'false' means it's available
                }
            }
            else
            {
                Debug.Log(cookingSpawnNodeTransform.name + " does not have the Seat script.");
            }
        }
        return (true, null);
    }

    IEnumerator cookingCooldown()
    {
        yield return new WaitForSeconds(cookingTime);
    }
}
