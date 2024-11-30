using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_WaitingState : NPC_BaseState
{
    private float timer;
    private NPC.Order order;
    private NPCStateManager stateManager;

    public override void EnterState(NPCStateManager npc)
    {
        Debug.Log(npc.name + " has entered the waiting state.");
        timer = npc.npc.patience; // Initialize the timer here
        order = npc.npc.currentOrder;
        Kitchen.Instance.sendToKitchen(order);
        stateManager = npc;
    }

    public override void UpdateState(NPCStateManager npc)
    {
        timer -= Time.deltaTime;

        if (timer > npc.npc.patience / 2)
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
            npc.SwitchState(NPCStateManager.NPCState.Leaving);
        }
    }

    public override void OnCollisionEnter(NPCStateManager npc)
    {
        // Handle collision if needed
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name + " has collided with NPC.");
        // Check if the collided object has the Food component
        Food food = other.GetComponent<Food>();
        if (food == null) return; // Exit early if no food component

        // Check if the food matches the NPC's order
        if (other.gameObject.tag == order.ToString())
        {
            Debug.Log("NPC has been given the correct order.");
            stateManager.SwitchState(NPCStateManager.NPCState.Eating);

        }
        else
        {
            // NPC gets angry and switches to leaving state
            Debug.Log("NPC has been given the incorrect order.");
            stateManager.npc.currentMood = NPC.mood.Angry;
            stateManager.SwitchState(NPCStateManager.NPCState.Leaving);
        }
    }


}
