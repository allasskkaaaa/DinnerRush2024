using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public FoodObject dish;
    [SerializeField] private SpriteRenderer thumbnail;

    public void setDish(FoodObject food)
    {
        
        dish = food;

        if (dish == null)
        {
            Debug.Log("No dish was found to set");
        }
        else
        {
            thumbnail.sprite = dish.thumbnail;
        }

            
    }
}
