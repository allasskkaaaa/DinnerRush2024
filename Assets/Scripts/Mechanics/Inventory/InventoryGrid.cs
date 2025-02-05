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

    private List<GameObject> createdSlots = new List<GameObject>();

    public enum InventoryType
    {
        Food,
        Ingredient
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < inventorySlots; i++)
        {
            // Calculate the row and column based on the current index
            int row = i / columns;
            int column = i % columns;

            // Check if we're exceeding the maximum rows
            if (row >= rows)
            {
                Debug.LogWarning("Not enough grid space for all inventory items!");
                break;
            }

            // Instantiate the slot prefab
            GameObject newSlot = Instantiate(slotPrefab, transform);
            createdSlots.Add(newSlot);

            newSlot.name = $"Slot ({row}, {column})";

            if (inventory != null && inventory.list.Count > 0)
            {
                // Get the Image component from the instantiated slot
                Transform child = newSlot.transform.GetChild(0);

                Transform quantity = newSlot.transform.GetChild(1);
 
                Image slotImage = child.GetComponent<Image>();

                TMP_Text quantityTXT = quantity.GetComponent<TMP_Text>();

                if (slotImage != null && i < inventory.list.Count)
                {
                    child.gameObject.SetActive(true);
                    slotImage.sprite = inventory.list[i].thumbnail;
                    Debug.Log("Setting slot image as " + inventory.list[i].name);

                }

                
            }
        }
    }

}
