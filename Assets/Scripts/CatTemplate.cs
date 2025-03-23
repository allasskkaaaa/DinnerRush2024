using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTemplate : MonoBehaviour
{
    [Header("Cat Settings")]
    public Cat cat; //The cat the template bases itself on
    [SerializeField] private List<FoodObject> canOrder; //All the menu items this cat can order
    [SerializeField] private InventoryObject menu; //Reference to the menu set for the shift
  
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        initCat();
    }
    public void initCat()
    {
        if (cat = null)
        {
            Debug.Log("Cat not found");
            return;
        }

        //Set cat image
        sr.sprite = cat.thumbnail;

        //Check what the cat can order by iterating through each of the favourite food and going through the menu
        foreach (FoodObject dish in cat.favouriteFoods)
        {
            for (int i = 0; i < menu.inventory.Count; i++)
            {
                if (dish == menu.inventory[i])
                {
                    canOrder.Add(dish);
                }
            }
        }
    }
}
