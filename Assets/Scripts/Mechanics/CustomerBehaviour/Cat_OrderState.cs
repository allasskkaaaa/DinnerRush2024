using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_OrderState : Cat_BaseState
{
    float timer;
    float thinkingTime;
    bool hasOrdered;

    public override void EnterState(Cat_StateManager cat)
    {
        Debug.Log("Entering Order state");
        thinkingTime = Random.Range(cat.minThinkTime, cat.maxThinkTime); //Calculate how long the cat will decide on an order
    }

    public override void UpdateState(Cat_StateManager cat)
    {
        if (thinkingTime > 0 && !hasOrdered)
        {
            thinkingTime -= Time.deltaTime;
        }
        else if (thinkingTime <= 0 && !hasOrdered)
        {
            orderFood(cat);
        } 

    }

    private void orderFood(Cat_StateManager cat)
    {
        //Grab a random food the cat can order
        if (cat.catTemplate.canOrder.Count > 0)
        { 
            cat.currentOrder = cat.catTemplate.canOrder[Random.Range(0, cat.catTemplate.canOrder.Count)];
            cat.orderThoughtSprite.sprite = cat.currentOrder.thumbnail;
            hasOrdered = true;

            cat.StartCoroutine(displayOrder(cat));
        }
        else
        {
            Debug.Log("No items to order");
        }

    }

    private IEnumerator displayOrder(Cat_StateManager cat)
    {
        cat.thoughtBubbleAnimator.Play("Form");
        yield return new WaitForSeconds(1);
        cat.orderThoughtSprite.sprite = cat.currentOrder.thumbnail;
        yield return new WaitForSeconds(3);
        cat.thoughtBubbleAnimator.Play("HeadEmpty");
        cat.orderThoughtSprite.sprite = null;
        hasOrdered = true;

        cat.SwitchState(cat.waitingState);
    }

    public override void OnTriggerEnter(Cat_StateManager cat, Collider2D collision)
    {
        cat.selected();
    }

    public override void OnTriggerStay(Cat_StateManager cat, Collider2D collision)
    {
    }

    public override void OnTriggerExit(Cat_StateManager cat, Collider2D collision)
    {
        cat.unselected();
    }
}
