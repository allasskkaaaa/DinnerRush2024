using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_WaitingState : NPC_BaseState
{
    private float waitTime = 15f;
    private float timer;

    public override void EnterState(NPCStateManager npc)
    {
        Debug.Log(npc.name + " has entered the waiting state.");
        timer = waitTime; // Initialize the timer here
    }

    public override void UpdateState(NPCStateManager npc)
    {
        timer -= Time.deltaTime;

        if (timer > waitTime / 2)
        {
            // If the player gives the NPC food within this time frame, they are happy
            npc.npc.currentMood = NPC.mood.Happy;
        }
        else if (timer > 0)
        {
            // If the player gives the NPC food within this time frame, they are neutral
            npc.npc.currentMood = NPC.mood.Neutral;
        }
        else
        {
            // If the player fails to give the NPC food, they will leave angry.
            npc.npc.currentMood = NPC.mood.Angry;
            npc.SwitchState(npc.leavingState);
        }
    }

    public override void OnCollisionEnter(NPCStateManager npc)
    {
        // Handle collision if needed
    }

  
}
