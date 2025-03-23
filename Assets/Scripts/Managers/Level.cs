using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class Level : MonoBehaviour
{
    [SerializeField] private InventoryObject foodInventory;
    [SerializeField] private InventoryObject menuInventory;
    [SerializeField] private FoodObject[] menu = new FoodObject[3];
    [SerializeField] private SlotManager[] menuSlots;
    [SerializeField] private GameObject chooseMenuPanel;
    [SerializeField] private InventoryGrid foodSelection;
    [SerializeField] private Button startGameButton;
    [SerializeField] private NotePad notePad;
    [SerializeField] private CustomerManager customerManager;

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
            //capturedButton.onClick.RemoveAllListeners();
            //Debug.Log("Removing listeners");
            slotButton.onClick.AddListener(() => addToMenu(slotButton.GetComponent<SlotManager>().itemInSlot, slotButton));
        }

    }

    private void addToMenu(FoodObject foodItem, Button button)
    {
        Debug.Log("Adding food to menu");

        foreach (SlotManager slot in menuSlots)
        {
            if (slot.itemInSlot == foodItem)
            {
                Debug.Log("Item already in menu");
                return;
            }

            if (foodItem.quantity > 1)
            {
                foodItem.quantity--;
            }
            else
            {
                foodItem.quantity--;
                button.interactable = false;


            }
        }



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


        if (picked <= 0)
        {
            Debug.Log("All items picked!");
            startGameButton.interactable = true;
        }
    }


    private void startGame()
    {

        foreach (SlotManager slot in menuSlots)
        {
            menuInventory.inventory.Add(slot.itemInSlot);
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
