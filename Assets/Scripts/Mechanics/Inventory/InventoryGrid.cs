using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
    public GameObject slotPrefab; // Assign your slot prefab in the Inspector
    public int rows = 4;          // Number of rows
    public int columns = 5;       // Number of columns
    public int inventoryItems = 12; // Total number of slots you want to generate



    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < inventoryItems; i++)
        {
            // Calculate the row and column based on the current index
            int row = i / columns; // Integer division determines the row
            int column = i % columns; // Modulus determines the column

            // Check if we're exceeding the maximum rows
            if (row >= rows)
            {
                Debug.LogWarning("Not enough grid space for all inventory items!");
                break; // Stop generating slots if we've run out of space
            }

            // Instantiate the slot prefab
            GameObject newSlot = Instantiate(slotPrefab, transform);

            // Optionally, customize the slot (e.g., naming it for clarity)
            newSlot.name = $"Slot ({row}, {column})";
        }
    }
}
