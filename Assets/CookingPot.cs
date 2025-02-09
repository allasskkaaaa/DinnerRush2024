using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CookingPot : MonoBehaviour
{
    private FoodObject outputMeal;

    [Header("Inventory References")]
    [SerializeField] private Inventory potInventory;
    [SerializeField] private Inventory foodInventory;
    [SerializeField] private Inventory cookableFoods;

    [SerializeField] private Image cookingOutputIMG;
    [SerializeField] private Button cookingOutputButton;

    private void Start()
    {
        if (cookingOutputButton != null) { cookingOutputButton.onClick.AddListener(() => addToInventory(outputMeal)); }
    }
    public void cook()
    {

        foreach (FoodObject meal in cookableFoods.list)
        {
            outputMeal = checkRecipe(meal);

            if (outputMeal != null) // Stop checking once a valid recipe is found
            {
                break;
            }
        }

        if (outputMeal == null)
        {
            Debug.Log("No recipe was found.");
        }
        else
        {
            Debug.Log(outputMeal.name + " was made!");
            cookingOutputIMG.gameObject.SetActive(true);
            cookingOutputIMG.sprite = outputMeal.thumbnail;
        }

        //Clear both the selected item list 
        potInventory.list.Clear();
    }

    private FoodObject checkRecipe(FoodObject food)
    {

        // Ensure the recipe and pot inventory have the same number of items
        if (potInventory.list.Count != food.recipe.Count)
        {
            return null; // If ingredient count doesn't match, it's not the right recipe
        }

        // Create a copy of the recipe list to track used ingredients
        List<FoodObject> recipeItems = new List<FoodObject>(food.recipe);

        // Check if all items in potInventory match the recipe items
        foreach (FoodObject potItem in potInventory.list)
        {
            bool found = false;

            for (int i = 0; i < recipeItems.Count; i++)
            {
                if (potItem.itemName == recipeItems[i].itemName) // Compare item names
                {
                    recipeItems.RemoveAt(i); // Remove matched ingredient from the recipe list
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return null; // If an ingredient doesn't match, return null (failed dish)
            }
        }

        return food; // If all ingredients match, return the cooked food
    }

    public void addToInventory(FoodObject item)
    {
        if (outputMeal != null)
        {
            bool found = false;

            foreach (FoodObject foodObject in foodInventory.list) // Check for duplicate items
            {
                if (item.itemName == foodObject.itemName) // If duplicate is found, increase its quantity
                {
                    foodObject.quantity++;
                    found = true;
                    break; // Exit the loop early since we found a match
                }
            }

            if (!found)
            {
                foodInventory.list.Add(item); // If no duplicate was found, add it
                item.quantity++;
            }

            cookingOutputIMG.gameObject.SetActive(false);
            cookingOutputIMG.sprite = null;
            outputMeal = null; // Clear output meal after adding
        }
    }


}
