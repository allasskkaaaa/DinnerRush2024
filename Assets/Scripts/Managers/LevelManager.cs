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
    [SerializeField] private SlotManager[] menuSlots;
    [SerializeField] private Button startGameButton;
    [SerializeField] private NotePad notePad;

    private int picked = 3;
    private void Start()
    {
        chooseMenu();

        startGameButton.onClick.AddListener(() => startGame());
    }

    private void chooseMenu()
    {
        Time.timeScale = 0;
        chooseMenuPanel.SetActive(true);
        startGameButton.interactable = false;

        foodSelection.inventory = foodInventory;
        foodSelection.GenerateGrid();

        foreach (Button slotButton in foodSelection.createdSlotButtons)
        {
            Button capturedButton = slotButton;  // Capture the correct reference
            capturedButton.onClick.AddListener(() => addToMenu(capturedButton.GetComponent<SlotManager>().itemInSlot));
        }



    }

    private void addToMenu(FoodObject foodItem)
    {
        Debug.Log("Adding food to menu");

        if (menuInventory.list.Contains(foodItem))
        {
            Debug.Log("Item already in menu!");
            return;
        }

        menuInventory.list.Add(foodItem);

        bool itemAdded = false;
        foreach (SlotManager slot in menuSlots)
        {
            if (slot.itemInSlot == null)
            {
                slot.itemInSlot = foodItem;
                slot.updateSlot();
                picked--;
                
                break;
            }
        }

        if (itemAdded)
        {
            Debug.Log("Items left to pick: " + picked);
        }

        if (picked <= 0)
        {
            Debug.Log("All items picked!");
            startGameButton.interactable = true;
        }
    }


    private void startGame()
    {
        Time.timeScale = 1;
        chooseMenuPanel.SetActive(false);

        notePad.initializeMenuButtons();
    }


}
