using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_LeavingState : Cat_BaseState
{
    public override void EnterState(Cat_StateManager cat)
    {
        Debug.Log("Entering Leaving state");
    }

    public override void UpdateState(Cat_StateManager cat)
    {

    }

    public override void OnTriggerEnter(Cat_StateManager cat, Collider2D collision)
    {

    }

    public override void OnTriggerStay(Cat_StateManager cat, Collider2D collision)
    {

    }

    public override void OnTriggerExit(Cat_StateManager cat, Collider2D collision)
    {

    }
}
