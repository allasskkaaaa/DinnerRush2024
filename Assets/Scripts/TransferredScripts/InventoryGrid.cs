using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(-1)]
public class InventoryGrid : MonoBehaviour
{
    public GameObject slotPrefab; // Assign your slot prefab in the Inspector
    public int rows = 4;          // Number of rows
    public int columns = 5;       // Number of columns
    public int inventorySlots = 12; // Total number of slots you want to generate

    public InventoryObject inventory; //Inventory the grid displays

    public List<Button> createdSlotButtons = new List<Button>();
    [SerializeField] private bool isCooking;

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


            

            if (inventoryIndex < inventory.inventory.Count)
            {
                GameObject newSlot = Instantiate(slotPrefab, transform);
                SlotManager slotScript = newSlot.GetComponent<SlotManager>();
                Button slotButton = newSlot.GetComponent<Button>();
                createdSlotButtons.Add(slotButton);
                slotScript.itemInSlot = inventory.inventory[inventoryIndex]; //Put current item in index into the slot
                slotScript.updateSlot(); //Update the slot to display info

                //if (isCooking)
                //{
                //    slotButton.onClick.AddListener(() => inputItem(slotScript.itemInSlot));
                //}
                
                newSlot.name = $"Slot ({row}, {column})";

                inventoryIndex++;
            }

            

            

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
