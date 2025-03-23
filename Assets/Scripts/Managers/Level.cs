using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class Level : MonoBehaviour
{
    [SerializeField] private InventoryObject foodInventory;
    [SerializeField] private InventoryObject menuInventory;
    [SerializeField] private FoodObject[] menu = new FoodObject[3];
    [SerializeField] private SlotManager[] menuSlots;
    [SerializeField] private GameObject chooseMenuPanel;
    [SerializeField] private GameObject returnToMenuPanel;
    [SerializeField] private InventoryGrid foodSelection;
    [SerializeField] private Button startGameButton;
    [SerializeField] private NotePad notePad;
    [SerializeField] private CustomerManager customerManager;
    [SerializeField] private Button returnToMenuButton;


    private int picked = 3;
    private void Start()
    {
        chooseMenu();

        startGameButton.onClick.AddListener(() => startGame());
        returnToMenuButton.onClick.AddListener(() => returnToMenu());

    }

    

    private void chooseMenu()
    {
        Time.timeScale = 0;
        chooseMenuPanel.SetActive(true);
        startGameButton.interactable = false;

        foodSelection.inventory = foodInventory;
        foodSelection.GenerateGrid();

        if (foodSelection.createdSlotButtons.Count > 0)
        {
            returnToMenuPanel.SetActive(false);
            foreach (Button slotButton in foodSelection.createdSlotButtons)
            {
                Button capturedButton = slotButton;  // Capture the correct reference
                                                     //capturedButton.onClick.RemoveAllListeners();
                                                     //Debug.Log("Removing listeners");
                slotButton.onClick.AddListener(() => addToMenu(slotButton.GetComponent<SlotManager>(), slotButton));
            }
        } else
        {
            returnToMenuPanel.SetActive(true);
        }

    }

    private void returnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void addToMenu(SlotManager slotManager, Button button)
    {
        Debug.Log("Adding food to menu");

        FoodObject foodItem = slotManager.itemInSlot;

        // Prevent adding duplicate items to the menu
        foreach (SlotManager slot in menuSlots)
        {
            if (slot.itemInSlot == foodItem)
            {
                Debug.Log("Item already in menu");
                return;
            }
        }

        // Reduce quantity only once
        if (foodItem.quantity > 1)
        {
            foodItem.quantity--;
        }
        else
        {
            foodItem.quantity--;
            button.interactable = false;
        }

        // Find an empty slot and add the item
        foreach (SlotManager slot in menuSlots)
        {
            if (slot.itemInSlot == null)
            {
                slot.itemInSlot = foodItem;
                slot.updateSlot();
                picked--;
                break;  // Stop the loop after assigning the item
            }
        }

        // Enable the start button if all items have been picked
        if (picked <= 0)
        {
            Debug.Log("All items picked!");
            startGameButton.interactable = true;
        }

        slotManager.updateSlot();
    }



    private void startGame()
    {

        foreach (SlotManager slot in menuSlots)
        {
            menuInventory.inventory.Add(slot.itemInSlot);
            slot.itemInSlot.quantity -= 1;
        }

        for (int i = foodInventory.inventory.Count - 1; i >= 0; i--)
        {
            if (foodInventory.inventory[i].quantity <= 0)
            {
                foodInventory.inventory.RemoveAt(i);
            }
        }

        
        Time.timeScale = 1;

        chooseMenuPanel.SetActive(false);

        notePad.initializeMenuButtons();

        customerManager.checkPossibleCats();

    }

    private void OnDisable()
    {
        menuInventory.inventory.Clear();
    }

}
