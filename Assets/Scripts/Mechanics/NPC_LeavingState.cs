using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_LeavingState : NPC_BaseState
{
    private NPC_Movement movement;

    public override void EnterState(NPCStateManager npc)
    {
        movement = npc.GetComponent<NPC_Movement>();
        Debug.Log(npc.name + " has entered the leaving state.");

        // Find the door by tag and set the destination to it
        movement.destinationNode = GameObject.FindWithTag("Door").GetComponent<PathNode>();
        movement.CreatePath();
        Debug.Log("Setting new destination to " + movement.destinationNode);

        npc.npc.seat.isOccupied = false;
    }

    public override void UpdateState(NPCStateManager npc)
    {
        // Display NPC's mood when leaving
        switch (npc.npc.currentMood)
        {
            case NPC.mood.Happy:
                Debug.Log("NPC is leaving happy!");
                break;
            case NPC.mood.Neutral:
                Debug.Log("NPC is leaving neutral!");
                break;
            case NPC.mood.Angry:
                Debug.Log("NPC is leaving angry!");
                break;
        }

        if (movement.hasReachedDestination)
        {
            GameObject.Destroy(npc.gameObject);
        }
    }

    public override void OnCollision2DEnter(NPCStateManager npc)
    {
        // Handle collision if needed
    }

}
