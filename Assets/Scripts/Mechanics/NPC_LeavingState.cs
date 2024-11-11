using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_LeavingState : NPC_BaseState
{
    public Transform door;

    public override void EnterState(NPCStateManager npc)
    {
        Debug.Log(npc.name + " has entered the leaving state.");

        // Find the door by tag and set the transform if found
        GameObject doorObject = GameObject.FindWithTag("Door");
        if (doorObject != null)
        {
            door = doorObject.transform;
        }
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

        // Call leave method to move NPC towards the door
        leave(npc);
    }

    public override void OnCollisionEnter(NPCStateManager npc)
    {
        // Handle collision if needed
    }

    private void leave(NPCStateManager npc)
    {
        // Ensure door is set before moving
        if (door != null)
        {
            // Move the NPC towards the door
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, door.position, npc.npc.walkSpeed * Time.deltaTime);
        }

        // Check if NPC has reached the door
        if (Vector3.Distance(npc.transform.position, door.position) < 0.1f)
        {
            GameObject.Destroy(npc.gameObject);
        }
    }
}
