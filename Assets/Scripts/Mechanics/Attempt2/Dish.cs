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
        thumbnail.sprite = dish.thumbnail;
    }
}
