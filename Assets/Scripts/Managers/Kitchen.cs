using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static NPC;

public class Kitchen : MonoBehaviour
{
    public static Kitchen Instance;
    [SerializeField] private Transform[] cookingSpawnNode; //Node where cooked objects will spawn
    [SerializeField] private float cookingTime = 5f; //How long it takes for object to cook aka cooldown between instantiation
    [SerializeField] private GameObject[] cookables; //Objects the kitchen can prepare
    [SerializeField] private List<NPC.Order> preparing = new List<NPC.Order>(); //Where NPC's orders are stored
    private bool canCook = true;
    private bool isCounterSpace = true;
    private Transform availableNode;

    private void Start()
    {
        Instance = this;
    }
    public void Update()
    {
        //Checks if the kitchen is off cooking cooldown. If it can, it first checks if there is any backlog in orders
        if (canCook && isCounterSpace)
        {
            if (preparing.Count > 0)
            {
                for (int i = 0; i < preparing.Count; i++)
                {
                    Serve(preparing[i]);
                }
            }
        }
        
        
    }
    public void Serve(NPC.Order order)
    {
        //Finds space on the counter to spawn to put item
        availableNode = AvailableNode();
        
        //If there is space, it will check if the order matches the name of any of the cookable items and spawns it
        if (availableNode != null)
        {
            string orderName = order.ToString();
            for (int i = 0; i < cookables.Length; i++)
            {
                if (cookables[i].name == orderName)
                {
                    Instantiate(cookables[i], availableNode.position, Quaternion.identity);
                    preparing.Remove(order);
                    StartCoroutine(cookingCooldown());
                }
            }
        }
    }

    public void sendToKitchen(NPC.Order order)
    {
        preparing.Add(order);
        string orderName = order.ToString();
        Debug.Log(orderName + " has been sent to the kitchen.");
    }
    public Transform AvailableNode()
    {
        for (int i = 0; i < cookingSpawnNode.Length; i++)
        {
            Transform cookingSpawnNodeTransform = cookingSpawnNode[i];

            // Grabs the OrderNode script from the node
            OrderNode node = cookingSpawnNodeTransform.GetComponent<OrderNode>();

            if (node != null)
            {
                if (node.isOccupied)
                {
                    Debug.Log(node.name + " is occupied.");
                }
                else
                {
                    Debug.Log(node.name + " is available.");

                    //Set the next available node.
                    node.isOccupied = true;
                    isCounterSpace = true;
                    return node.transform;
                }
            }
            else
            {
                Debug.Log(cookingSpawnNodeTransform.name + " does not have the Seat script.");
            }
        }
        isCounterSpace = false;
        return null;
    }

    IEnumerator cookingCooldown()
    {
        canCook = false;
        yield return new WaitForSeconds(cookingTime);
        canCook = true;
    }
}
