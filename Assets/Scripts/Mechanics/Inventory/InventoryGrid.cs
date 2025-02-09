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
    public Inventory potInventory;

    [SerializeField] private InventoryGrid selectedItemGrid;

    private List<Button> createdSlotButtons = new List<Button>();

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
            Button slotButton = newSlot.GetComponent<Button>();
            createdSlotButtons.Add(slotButton);
            newSlot.name = $"Slot ({row}, {column})";

            if (inventory != null && inventory.list.Count > 0)
            {
                Transform child = newSlot.transform.GetChild(0);
                Transform quantityBG = newSlot.transform.GetChild(1);
                Transform quantityTXTHolder = quantityBG.transform.GetChild(0);

                Image slotImage = child.GetComponent<Image>();
                TMP_Text quantityTXT = quantityTXTHolder.GetComponent<TMP_Text>();

                

                if (slotImage != null && i < inventory.list.Count)
                {
                    child.gameObject.SetActive(true);
                    FoodObject slotItem = inventory.list[i]; // Get the item from inventory

                    SlotManager slotManager = newSlot.GetComponent<SlotManager>();
                    slotManager.itemInSlot = slotItem; // Assign it properly

                    slotImage.sprite = slotItem.thumbnail;
                    Debug.Log("Setting slot image as " + slotItem.name);

                    if (quantityTXT != null)
                    {
                        if (slotItem.quantity > 1) //Display the quantity of an object if it's greater than 1
                        {
                            quantityBG.gameObject.SetActive(true);
                            quantityTXT.text = slotItem.quantity.ToString();
                        }
                    }
                    

                    // Assign button listener
                    slotButton.onClick.AddListener(() => inputItem(slotItem));
                    Debug.Log("Adding button listener for " + slotItem.name);
                }
            }
        }
    }


    public void inputItem(FoodObject item)
    {
        if (potInventory != null)
        {
            potInventory.list.Add(item);
        }

        selectedItemGrid.GenerateGrid();
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
