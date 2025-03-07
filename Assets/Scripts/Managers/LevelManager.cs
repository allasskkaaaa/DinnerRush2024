using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Inventory foodInventory;
    [SerializeField] private Inventory menuInventory;
    [SerializeField] private FoodObject[] menu = new FoodObject[3];
    [SerializeField] private GameObject chooseMenuPanel;
    [SerializeField] private InventoryGrid foodSelection;


    private void Start()
    {
        chooseMenu();
    }

    private void chooseMenu()
    {
        Time.timeScale = 0;
        chooseMenuPanel.SetActive(true);

        foodSelection.inventory = foodInventory;
        foodSelection.GenerateGrid();



    }
}
