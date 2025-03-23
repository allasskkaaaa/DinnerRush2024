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
        waitingFor = cat.currentOrder;
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


    private IEnumerator displayMood(Cat_StateManager cat, string mood)
    {
        cat.orderThoughtSprite.sprite = null;
        cat.thoughtBubbleAnimator.Play(mood);
        yield return new WaitForSeconds(3);
        cat.thoughtBubbleAnimator.Play("HeadEmpty");
        cat.orderThoughtSprite.sprite = null;

        cat.SwitchState(cat.waitingState);
    }

    public override void OnTriggerEnter(Cat_StateManager cat, Collider2D collision)
    {

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
            Debug.Log("Dish detected");
            if (dish.dish == cat.currentOrder)
            {
                AudioManager.instance.playOneShot(cat.meow);
                Debug.Log("Order recieved");
                cat.StopAllCoroutines();
                cat.StartCoroutine(displayMood(cat, "Happy"));
                GameObject.Destroy(draggable.gameObject);
                cat.currentMood = "Happy";
                cat.SwitchState(cat.leavingState);
            } 
            else if (dish.dish == cat.currentOrder && serveAttempts == 2)
            {
                AudioManager.instance.playOneShot(cat.meow);
                serveAttempts--;
                cat.orderSatisfaction--;
                cat.StopAllCoroutines();
                cat.StartCoroutine(displayMood(cat, "Neutral"));
                GameObject.Destroy(draggable.gameObject);
                cat.currentMood = "Neutral";
                cat.SwitchState(cat.leavingState);
            }
            else if (dish.dish == cat.currentOrder && serveAttempts == 1)
            {
                AudioManager.instance.playOneShot(cat.meow);
                serveAttempts--;
                cat.orderSatisfaction--;
                cat.StopAllCoroutines();
                cat.StartCoroutine(displayMood(cat, "Sad"));
                GameObject.Destroy(draggable.gameObject);
                cat.currentMood = "Sad";
                cat.SwitchState(cat.leavingState);
            }
            else if (dish.dish == cat.currentOrder && serveAttempts == 0)
            {
                AudioManager.instance.playOneShot(cat.angry_meow);
                cat.orderSatisfaction--;
                cat.StopAllCoroutines();
                cat.StartCoroutine(displayMood(cat, "Angry"));
                GameObject.Destroy(draggable.gameObject);
                cat.currentMood = "Angry";
                cat.SwitchState(cat.leavingState);
            }


        }
    }

    public override void OnTriggerExit(Cat_StateManager cat, Collider2D collision)
    {

    }
}
