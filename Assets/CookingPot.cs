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

    [SerializeField] private Image cookingOutputIMG;
    
    public void cook()
    {

        foreach (FoodObject meal in foodInventory.list)
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

    

}
