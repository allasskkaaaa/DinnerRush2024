using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_StateManager : MonoBehaviour
{
    [SerializeField] public CatTemplate catTemplate;

    [Header("Cat Order")]
    [SerializeField] public List<FoodObject> canOrder;
    [SerializeField] public float patience = 20;
    [SerializeField] public Animator thoughtBubbleAnimator;
    [SerializeField] public SpriteRenderer orderThoughtSprite;
    [SerializeField] public int maxThinkTime = 3;
    [SerializeField] public int minThinkTime = 1;
    [SerializeField] public FoodObject currentOrder;

    [SerializeField] public string currentMood;

    bool isScaled;

    [Header("Interaction Settings")]
    [SerializeField] private float scaleMultiplier; // When the cat is hovered over, local scale is multiplied by this value
    private Vector3 originalScale; // Tracks original scale

    Cat_BaseState currentState;

    public Cat_OrderState orderState = new Cat_OrderState();
    public Cat_WaitingState waitingState = new Cat_WaitingState();
    public Cat_LeavingState leavingState = new Cat_LeavingState();
    void Start()
    {
        originalScale = gameObject.transform.localScale;
        canOrder = catTemplate.canOrder;
        currentState = orderState;
        currentState.EnterState(this);
    }

    public void selected()
    {

        if (!isScaled)
        {
            gameObject.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
            isScaled = true;
            Debug.Log("Scaling object");
        }
    }

    public void unselected()
    {
        gameObject.transform.localScale = originalScale;
        isScaled = false;
    }
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(Cat_BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        currentState.OnTriggerStay(this, collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnter(this, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentState.OnTriggerExit(this, collision);
    }
}
