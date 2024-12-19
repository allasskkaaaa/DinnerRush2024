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
        

        // Find the SeatManager
        sm = GameObject.FindWithTag("Seat Manager").GetComponent<SeatManager>();
        var (areAllOccupied, seatObject) = sm.CheckAvailability();

        if (areAllOccupied)
        {
            Debug.Log("All seats are occupied.");
        }
        else if (seatObject != null) // Check if a seat has been assigned
        {
            var seat = seatObject.GetComponent<Seat>();
            npc.npc.seat = seat; // Assign the seat to the NPC
            seatObject.GetComponent<Seat>().isOccupied = true; // Mark seat as occupied
            

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
        if (movement.currentNode == movement.destinationNode)
        {
            npc.SwitchState(NPCStateManager.NPCState.Waiting);
        }
    }

    public override void OnCollision2DEnter(NPCStateManager npc)
    {
        // Handle collision if needed
    }



}
