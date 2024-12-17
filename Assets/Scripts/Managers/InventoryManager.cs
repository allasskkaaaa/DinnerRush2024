using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] private Transform[] inventorySlots;

    private void Start()
    {
        Instance = this;
    }
    private Transform findAvailableSlot()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot inventorySlot = inventorySlots[i].GetComponent<InventorySlot>();

            if (!inventorySlot.isOccupied)
            {
                inventorySlot.isOccupied = true;
                return inventorySlots[i].transform;
            }
        }
        Debug.Log("All inventory slots are full");
        return null; 
    }

    public void AddToInventory(GameObject invObject)
    {
        Transform availableSlot = findAvailableSlot();
        if (availableSlot != null)
        {
            Instantiate(invObject, availableSlot);
        }
        else
        {
            Debug.LogWarning("Cannot add to inventory: no available slots.");
        }
    }

}
