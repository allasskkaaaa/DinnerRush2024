using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool pickedUp = false;

    private void Update()
    {
        // Check if KitchenV2 instance exists
        if (KitchenV2.instance == null) return;

        // If the food is picked up, stop moving
        if (pickedUp) return;

        // Move towards the conveyor belt end position
        if (Vector3.Distance(transform.position, KitchenV2.instance.conveyorbeltEnd.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                KitchenV2.instance.conveyorbeltEnd.position,
                KitchenV2.instance.speed * Time.deltaTime
            );
        }
        else
        {
            // Destroy the food object when it reaches the end
            Destroy(gameObject);
        }
    }
}
