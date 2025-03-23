using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CatTemplate : MonoBehaviour
{
    [Header("Cat Settings")]
    public Cat cat; //The cat the template bases itself on
    [SerializeField] public List<FoodObject> canOrder; //All the menu items this cat can order
    [SerializeField] private InventoryObject menu; //Reference to the menu set for the shift
    [SerializeField] public SpawnNode spawnNode;

    [SerializeField] public SpriteRenderer sr;


    public void initCat()
    {
        if (cat == null)
        {
            Debug.Log("Cat not found");
            return;
        }

        // Set cat image
        sr.sortingOrder = spawnNode.zOrder;
        sr.flipX = spawnNode.xFlipped;
        sr.sprite = cat.thumbnail;

        // Initialize the canOrder list to ensure it's empty before adding items
        canOrder = new List<FoodObject>();

        // Check what the cat can order by iterating through each of the favourite food and going through the menu
        foreach (FoodObject dish in cat.favouriteFoods)
        {
            foreach (FoodObject menuItem in menu.inventory)
            {
                // Compare based on the food name or ID (adjust as needed)
                if (dish.name == menuItem.name)
                {
                    canOrder.Add(dish);
                    Debug.Log("Cat can order: " + dish.name);
                    break;  // Stop after finding a match to avoid duplicates
                }
            }
        }
    }


}
