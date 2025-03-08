using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGrid : MonoBehaviour
{
    public GameObject slotPrefab; // Assign your slot prefab in the Inspector
    public int rows = 4;          // Number of rows
    public int columns = 5;       // Number of columns
    public int inventorySlots = 12; // Total number of slots you want to generate

    public Inventory inventory; //Inventory the grid displays

    public List<Button> createdSlotButtons = new List<Button>();

    public enum InventoryType
    {
        Food,
        Ingredient
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void OnEnable()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        clearSlots(); //Clear any already created slots

        int inventoryIndex = 0;
        for (int i = 0; i < inventorySlots; i++)
        {
            int row = i / columns;
            int column = i % columns;

            if (row >= rows)
            {
                Debug.LogWarning("Not enough grid space for all inventory items!");
                break;
            }


            GameObject newSlot = Instantiate(slotPrefab, transform);
            SlotManager slotScript = newSlot.GetComponent<SlotManager>();
            Button slotButton = newSlot.GetComponent<Button>();
            createdSlotButtons.Add(slotButton);

            if (inventoryIndex < inventory.list.Count)
            {
                slotScript.itemInSlot = inventory.list[inventoryIndex]; //Put current item in index into the slot
                slotScript.updateSlot(); //Update the slot to display info

                inventoryIndex++;
            }

            slotButton.onClick.AddListener(() => inputItem(slotScript.itemInSlot));

            newSlot.name = $"Slot ({row}, {column})";

        }
    }

    public void inputItem(FoodObject item)
    {
        GameObject[] selectionSlots = GameObject.FindGameObjectsWithTag("CookingSlot");

        if (selectionSlots.Length > 0)
        {
            foreach (GameObject slot in selectionSlots)
            {
                SlotManager slotScript = slot.GetComponent<SlotManager>();

                if (slotScript.itemInSlot != null)
                {
                    continue;
                }
                else
                {
                    slotScript.itemInSlot = item;
                    slotScript.updateSlot();
                    break;
                }
            }
        }
        
    }
public void clearSlots()
    {
        foreach (Button slot in createdSlotButtons)
        {
            Destroy(slot.gameObject);
        }

        createdSlotButtons.Clear();
    }

}
