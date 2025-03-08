using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenV2 : MonoBehaviour
{
    public static KitchenV2 instance;

    [SerializeField] public Transform converyorbeltStart; // Start of the conveyor belt
    [SerializeField] private float cooldownBetweenOrders = 1f; // Cooldown between orders spawning
    [SerializeField] private List<FoodObject> backlog = new List<FoodObject>();
    [SerializeField] private GameObject dishPrefab; //The base template for a dish gameobject

    private float timer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        timer = cooldownBetweenOrders;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (backlog.Count > 0)
            {
                // Process one order at a time
                makeOrder(backlog[0]);
                backlog.RemoveAt(0);
                timer = cooldownBetweenOrders;
            }
        }
    }

    public void makeOrder(FoodObject order)
    {
        if (timer > 0)
        {
            backlog.Add(order);
        }
        else
        {
            // Instantiate the order at the conveyor belt start position
            GameObject madeOrder = Instantiate(dishPrefab, converyorbeltStart.position, Quaternion.identity);

            // Get the Dish component from the instantiated object
            Dish dishComponent = madeOrder.GetComponent<Dish>();
            if (dishComponent != null)
            {
                dishComponent.dish = order;
                dishComponent.setDish(order);
            }
            else
            {
                Debug.LogError("Dish component not found on the dishPrefab.");
            }

            timer = cooldownBetweenOrders;
        }
    }

}
