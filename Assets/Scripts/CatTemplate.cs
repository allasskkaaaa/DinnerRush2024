using System.Collections.Generic;
using UnityEngine;

public class CatTemplate : MonoBehaviour
{
    [Header("Cat Settings")]
    public Cat cat; // The cat the template bases itself on
    [SerializeField] public List<FoodObject> canOrder; // All the menu items this cat can order
    [SerializeField] private InventoryObject menu; // Reference to the menu set for the shift
    [SerializeField] public SpawnNode spawnNode;

    [SerializeField] public SpriteRenderer sr;

    private void Awake()
    {
        // Clear cat reference in case this object is reused or reset
        cat = null;
    }

    // Call this method to initialize the cat template
    public void initCat(Cat newCat)
    {
        if (newCat == null)
        {
            Debug.Log("Cat not found");
            return;
        }

        cat = newCat; // Set the new cat reference

        // Set the sprite and other properties for the cat
        sr.sortingOrder = spawnNode.zOrder;
        sr.flipX = spawnNode.xFlipped;
        sr.sprite = cat.thumbnail;

        // Clear the previous canOrder list and reinitialize it
        canOrder.Clear();

        // Populate the canOrder list with the food items the cat can order
        foreach (FoodObject dish in cat.favouriteFoods)
        {
            foreach (FoodObject menuItem in menu.inventory)
            {
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
