using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cat_LeavingState : Cat_BaseState
{
    public override void EnterState(Cat_StateManager cat)
    {
        Debug.Log("Entering Leaving state");

        cat.StopAllCoroutines();
        cat.StartCoroutine(leave(cat));

        
    }

    private IEnumerator leave(Cat_StateManager cat)
    {
        Debug.Log("Cleanliness: " + cat.calculateCleanliness());
        Debug.Log("Patience: " + cat.patience);
        Debug.Log("Order Satisfaction: " + cat.orderSatisfaction);
        cat.overallSatisfaction = ((5 * cat.calculateCleanliness()) + (cat.patience / 4) + (5 * (cat.orderSatisfaction) / 3)) / 3;


        float tip;

        switch (cat.currentMood)
        {
            case "Happy":
                tip = (cat.overallSatisfaction * 5) / 0.20f; // Very large tip
                break;
            case "Neutral":
                tip = (cat.overallSatisfaction * 5) / 0.10f; // Extremely large tip
                break;
            case "Sad":
                tip = (cat.overallSatisfaction * 5) / 0.5f;  // Moderate tip
                break;
            case "Angry":
                tip = 0f; // No tip if the cat is angry
                break;

            default:
                tip = 0f;
                break;
        }

        int paid = Mathf.RoundToInt(cat.overallSatisfaction + tip);

        GameManager.Instance.updateMoney(paid);
        Debug.Log("Cat paid "+ paid);
        GameManager.Instance.allRatings.Add(cat.overallSatisfaction);
        Debug.Log("Cat rating: " + cat.overallSatisfaction);
        GameManager.Instance.calculateRestaurantScore();

        yield return new WaitForSeconds(3);

        cat.catAnim.Play("Leave");

        yield return new WaitForSeconds(1);
        cat.catTemplate.spawnNode.isOccupied = false;
        cat.destroyObject();

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
