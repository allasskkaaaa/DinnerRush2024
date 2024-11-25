using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateManager : MonoBehaviour
{
    public NPC npc;
    private int walkSpeed = 5;

    // Enum for NPC states
    public enum NPCState
    {
        Seating,
        Waiting,
        Eating,
        Leaving
    }

    // Current state as an enum (visible in Inspector)
    public NPCState currentEnumState;

    // States
    private NPC_BaseState currentState;
    public NPC_SeatingState seatingState = new NPC_SeatingState();
    public NPC_WaitingState waitingState = new NPC_WaitingState();
    public NPC_EatingState eatingState = new NPC_EatingState();
    public NPC_LeavingState leavingState = new NPC_LeavingState();

    private void Start()
    {
        npc = ScriptableObject.CreateInstance<NPC>();

        // Initialize state
        SwitchState(NPCState.Seating);
    }

    private void Update()
    {
        currentState.UpdateState(this);

        // Debug: Log current state
        Debug.Log($"NPC is currently in the {currentEnumState} state.");
    }

    public void SwitchState(NPCState newState)
    {
        // Update the enum state
        currentEnumState = newState;

        // Set the current state based on the enum
        switch (newState)
        {
            case NPCState.Seating:
                currentState = seatingState;
                break;
            case NPCState.Waiting:
                currentState = waitingState;
                break;
            case NPCState.Eating:
                currentState = eatingState;
                break;
            case NPCState.Leaving:
                currentState = leavingState;
                break;
            default:
                Debug.LogError("Invalid state!");
                return;
        }

        // Enter the new state
        currentState.EnterState(this);
    }
}
