using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTemplate : MonoBehaviour
{
    [Header("Cat Settings")]
    public Cat cat; //The cat the template bases itself on
    [SerializeField] private List<FoodObject> canOrder; //All the menu items this cat can order
    [SerializeField] private Inventory menu; //Reference to the menu set for the shift
    [SerializeField] private float cleanlinessRating = 5;
    [SerializeField] private float patience = 20;
    [SerializeField] private int orderSatisfaction = 5;
    [SerializeField] private float overallSatisfaction;

    [Header("Interaction Settings")]
    [SerializeField] private float scaleMultiplier; // When the cat is hovered over, local scale is multiplied by this value
    private Vector3 originalScale; // Tracks original scale
    private bool isServed; // Customer was served

    [Header("Order Settings")]
    private bool checkingOrder;
    private bool wasServed;
    private bool hasOrdered; // Track if they've ordered already or not
    private GameObject currentThought; // Track current thought

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
            for (int i = 0; i < menu.list.Count; i++)
            {
                if (dish == menu.list[i])
                {
                    canOrder.Add(dish);
                }
            }
        }
    }
}
