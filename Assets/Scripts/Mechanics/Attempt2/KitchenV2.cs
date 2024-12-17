using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenV2 : MonoBehaviour
{
    public static KitchenV2 instance;

    [SerializeField] public Transform converyorbeltStart; // Start of the conveyor belt
    [SerializeField] public Transform conveyorbeltEnd;   // End of the conveyor belt
    [SerializeField] public float speed = 5;            // How fast the conveyor belt moves
    [SerializeField] private float cooldownBetweenOrders = 0.3f; // Cooldown between orders spawning
    [SerializeField] private List<GameObject> backlog = new List<GameObject>();

    private float timer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
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

    public void makeOrder(GameObject order)
    {
        if (timer > 0)
        {
            backlog.Add(order);
        }
        else
        {
            // Instantiate the order at the conveyor belt start position
            GameObject madeOrder = Instantiate(order, converyorbeltStart.position, Quaternion.identity);
            timer = cooldownBetweenOrders;
        }
    }
}
