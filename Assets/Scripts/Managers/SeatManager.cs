using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatManager : MonoBehaviour
{
    public (bool isOccupied, Vector3 position) CheckAvailability()
    {
        // Loops through each child in the seating area
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);

            // Grabs the Seat script from the child
            Seat seat = child.GetComponent<Seat>();

            if (seat != null)
            {
                if (seat.isOccupied)
                {
                    Debug.Log(child.name + " is occupied.");
                }
                else
                {
                    Debug.Log(child.name + " is available.");
                    // Return false and the position of the available seat
                    return (false, child.position); // 'false' means it's available
                }
            }
            else
            {
                Debug.Log(child.name + " does not have the Seat script.");
            }
        }

        // If no seats are available, return true and a default position
        return (true, Vector3.zero); // 'true' means all seats are occupied
    }
}
