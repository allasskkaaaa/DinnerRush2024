using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Inventory foodInventory;
    [SerializeField] private Inventory menuInventory;
    [SerializeField] private FoodObject[] menu = new FoodObject[3];


    private void Start()
    {
        chooseMenu();
    }

    private void chooseMenu()
    {
        Time.timeScale = 0;


    }
}
