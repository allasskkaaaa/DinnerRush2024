using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateManager : MonoBehaviour
{
    private int walkSpeed = 5;
    NPC_BaseState currentState;
    public NPC_SeatingState seatingState = new NPC_SeatingState();
    public NPC_WaitingState waitingState = new NPC_WaitingState();
    public NPC_EatingState eatingState = new NPC_EatingState();
    public NPC_LeavingState leavingState = new NPC_LeavingState();

    private void Start()
    {
        currentState = seatingState;

        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(NPC_BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
