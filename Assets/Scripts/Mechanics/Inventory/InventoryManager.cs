using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //Inventories
    private List<FoodItem> foodInventory = new List<FoodItem>();
    private List<Ingredient> ingInventory = new List<Ingredient>();

    private void Start()
    {
        Instance = this;
    }

    public void addToInventory(GameObject item)
    {
        //Check if object is an ingredient or food
        Ingredient ing = item.GetComponent<Ingredient>();
        if (ing != null)
        {
            ingInventory.Add(ing);
        }

        FoodItem food = item.GetComponent<FoodItem>();
        if (food != null)
        {
            foodInventory.Add(food);
        }
    }
}
