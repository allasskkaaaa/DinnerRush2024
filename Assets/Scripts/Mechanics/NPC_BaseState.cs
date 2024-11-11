using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC_BaseState
{
    public abstract void EnterState(NPCStateManager npc);

    public abstract void UpdateState(NPCStateManager npc);

    public abstract void OnCollisionEnter(NPCStateManager npc);

}