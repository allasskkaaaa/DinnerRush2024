using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NPC_SeatingState : NPC_BaseState
{
    private GameObject seat;
    private Seat seatScript; // Reference to the Seat component
    private SeatManager sm;
    private NPC_Movement movement;

    public List<PathNode> path = new List<PathNode>();

    public override void EnterState(NPCStateManager npc)
    {
        movement = npc.GetComponent<NPC_Movement>();
        Debug.Log(npc.name + " has entered the seating state.");

        // Find the SeatManager
        sm = GameObject.FindWithTag("Seat Manager").GetComponent<SeatManager>();
        var (areAllOccupied, seatObject) = sm.CheckAvailability();

        if (areAllOccupied)
        {
            Debug.Log("All seats are occupied.");
        }
        else if (seatObject != null) // Check if a seat is available
        {
            var seat = seatObject.GetComponent<Seat>();
            npc.npc.seat = seat; // Assign the seat to the NPC
            seat.isOccupied = true; // Mark seat as occupied
            Debug.Log(seatObject.name + " marked as occupied by " + npc.name);

            // Set movement nodes
            movement.currentNode = GameObject.FindWithTag("Door").GetComponent<PathNode>();
            movement.destinationNode = seatObject.GetComponent<PathNode>();
        }
        else
        {
            Debug.LogError("Seat object is null despite availability.");
        }
    }


    public override void UpdateState(NPCStateManager npc)
    {

    }

    public override void OnCollisionEnter(NPCStateManager npc)
    {
        // Optional: Handle collision logic if needed
    }



}
