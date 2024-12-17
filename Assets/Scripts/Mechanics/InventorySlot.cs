using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public bool isOccupied;
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Item is overlapping drop zone");
        Draggable draggable = other.GetComponent<Draggable>();

        if (draggable == null)
        {
            return;
        }
        else
        {
            Debug.Log("Draggable is detected");
            if (!draggable.isDragging)
            {
                InventoryManager.Instance.AddToInventory(other.gameObject);
                Destroy(other.gameObject);
            }
        }
    }


}
