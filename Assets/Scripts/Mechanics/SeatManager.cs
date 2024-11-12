using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatManager : MonoBehaviour
{
    [SerializeField] public Transform[] seats;

    public (bool areAllOccupied, Transform seat) CheckAvailability()
    {
        // Loops through each seat
        for (int i = 0; i < seats.Length; i++)
        {
            Transform seatTransform = seats[i];

            // Grabs the Seat script from the seat
            Seat seat = seatTransform.GetComponent<Seat>();

            if (seat != null)
            {
                if (seat.isOccupied)
                {
                    Debug.Log(seatTransform.name + " is occupied.");
                }
                else
                {
                    Debug.Log(seatTransform.name + " is available.");
                    // Return false and the available seat Transform
                    return (false, seatTransform); // 'false' means it's available
                }
            }
            else
            {
                Debug.Log(seatTransform.name + " does not have the Seat script.");
            }
        }

        // If no seats are available, return true and null
        return (true, null); // 'true' means all seats are occupied
    }
}
