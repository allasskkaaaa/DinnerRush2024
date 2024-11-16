using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_EatingState : NPC_BaseState
{
    public override void EnterState(NPCStateManager npc)
    {
        Debug.Log(npc.name + " has entered the eating state.");
    }

    public override void UpdateState(NPCStateManager npc)
    {

    }

    public override void OnCollisionEnter(NPCStateManager npc)
    {

    }

    
}
