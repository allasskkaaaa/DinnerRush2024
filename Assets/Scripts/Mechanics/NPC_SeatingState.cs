using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_SeatingState : NPC_BaseState
{
    private Transform seat;
    private Seat seatScript; // Reference to the Seat component
    private SeatManager sm;

    public override void EnterState(NPCStateManager npc)
    {
        Debug.Log(npc.name + " has entered the seating state.");

        sm = GameObject.FindWithTag("Seat Manager").GetComponent<SeatManager>();
        var (areAllOccupied, freeSeatTransform) = sm.CheckAvailability();

        if (areAllOccupied)
        {
            Debug.Log("All seats are occupied.");
        }
        else
        {
            seat = freeSeatTransform;
            seatScript = seat.GetComponent<Seat>();

            if (seatScript != null)
            {
                seatScript.isOccupied = true; // Mark seat as occupied
                Debug.Log(seat.name + " marked as occupied.");
            }
        }
    }

    public override void UpdateState(NPCStateManager npc)
    {
        if (seat != null)
        {
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, seat.position, npc.npc.walkSpeed * Time.deltaTime);

            if (Vector3.Distance(npc.transform.position, seat.position) < 0.1f)
            {
                npc.SwitchState(npc.waitingState);
            }
        }
    }
    public override void OnCollisionEnter(NPCStateManager npc)
    {
        
    }
}
