using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatManager : MonoBehaviour
{
    [SerializeField] public GameObject[] seats;

    public (bool areAllOccupied, GameObject seat) CheckAvailability()
    {
        // Loops through each seat
        for (int i = 0; i < seats.Length; i++)
        {
            GameObject seatObject = seats[i];

            // Grabs the Seat script from the seat
            Seat seat = seatObject.GetComponent<Seat>();

            if (seat != null)
            {
                if (seat.isOccupied)
                {
                    Debug.Log(seatObject.name + " is occupied.");
                }
                else
                {
                    Debug.Log(seatObject.name + " is available.");
                    // Return false and the available seat Transform
                    return (false, seatObject); // 'false' means it's available
                }
            }
            else
            {
                Debug.Log(seatObject.name + " does not have the Seat script.");
            }
        }

        // If no seats are available, return true and null
        return (true, null); // 'true' means all seats are occupied
    }
}
