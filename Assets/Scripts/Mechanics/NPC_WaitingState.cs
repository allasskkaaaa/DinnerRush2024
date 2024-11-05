using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_WaitingState : NPC_BaseState
{
    public override void EnterState(NPCStateManager npc)
    {
        Debug.Log(npc.name + " has entered the waiting state.");
    }

    public override void UpdateState(NPCStateManager npc)
    {

    }

    public override void OnCollisionEnter(NPCStateManager npc)
    {

    }
}
