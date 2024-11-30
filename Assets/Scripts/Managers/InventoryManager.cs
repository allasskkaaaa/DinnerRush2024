using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Transform[] inventorySlots;

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

    private void AddToInventory(GameObject invObject)
    {
        Instantiate(invObject, findAvailableSlot());
    }

    private void RemoveFromInventory(int slot)
    {
        
    }
}
