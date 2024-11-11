using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_SeatingState : NPC_BaseState
{
    private Vector3 seat;

    SeatManager sm;
    public override void EnterState(NPCStateManager npc)
    {
        Debug.Log(npc.name + " has entered the seating state.");

        sm = GameObject.FindWithTag("Seat Manager").GetComponent<SeatManager>();

        var (areAllOccupied, freeSeatPosition) = sm.CheckAvailability();

        if (areAllOccupied)
        {
            Debug.Log("All seats are occupied.");
        }
        else
        {
            //Assigns the first available seat as the target place to sit.
            seat = freeSeatPosition;
        }
    }

    public override void UpdateState(NPCStateManager npc)
    {
        if (seat != null)
        {
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, seat, npc.npc.walkSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(npc.transform.position, seat) < 0.1f)
        {
            npc.SwitchState(npc.waitingState);
        }
    }

    public override void OnCollisionEnter(NPCStateManager npc)
    {

    }
}
