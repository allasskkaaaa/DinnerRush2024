using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_WaitingState : Cat_BaseState
{
    FoodObject waitingFor;
    float timer;
    int serveAttempts = 2;
    public override void EnterState(Cat_StateManager cat)
    {
        Debug.Log("Entering Waiting state");
        timer = cat.patience;
    }

    public override void UpdateState(Cat_StateManager cat)
    {
        //Counts down patience
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            cat.SwitchState(cat.leavingState);
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

        cat.SwitchState(cat.waitingState);
    }

    private IEnumerator displayMood(Cat_StateManager cat, string mood)
    {
        cat.thoughtBubbleAnimator.Play(mood);
        yield return new WaitForSeconds(3);
        cat.thoughtBubbleAnimator.Play("HeadEmpty");
        cat.orderThoughtSprite.sprite = null;

        cat.SwitchState(cat.waitingState);
    }

    public override void OnTriggerEnter(Cat_StateManager cat, Collider2D collision)
    {
        cat.selected();
        cat.StartCoroutine(displayOrder(cat));
    }

    public override void OnTriggerStay(Cat_StateManager cat, Collider2D collision)
    {
        if (collision.CompareTag("Finger"))
            return;

        Draggable draggable = collision.GetComponent<Draggable>();

        if (draggable == null)
            {Debug.Log("No draggable object detected");
            return;
        }

        if (!draggable.isDragging)
        {
            Dish dish = collision.GetComponent<Dish>();

            if (dish.dish == waitingFor)
            {
                Debug.Log("Order recieved");
                cat.StartCoroutine(displayMood(cat, "Happy"));
                GameObject.Destroy(draggable.gameObject);
                cat.currentMood = "Happy";
            } 
            else if (dish.dish == waitingFor && serveAttempts == 2)
            {
                serveAttempts--;
                cat.StartCoroutine(displayMood(cat, "Neutral"));
                GameObject.Destroy(draggable.gameObject);
                cat.currentMood = "Neutral";
            }
            else if (dish.dish == waitingFor && serveAttempts == 1)
            {
                serveAttempts--;
                cat.StartCoroutine(displayMood(cat, "Sad"));
                GameObject.Destroy(draggable.gameObject);
                cat.currentMood = "Sad";
            }
            else if (dish.dish == waitingFor && serveAttempts == 1)
            {
                serveAttempts--;
                cat.StartCoroutine(displayMood(cat, "Angry"));
                GameObject.Destroy(draggable.gameObject);
                cat.currentMood = "Angry";
            }


        }
    }

    public override void OnTriggerExit(Cat_StateManager cat, Collider2D collision)
    {
        cat.unselected();
    }
}
