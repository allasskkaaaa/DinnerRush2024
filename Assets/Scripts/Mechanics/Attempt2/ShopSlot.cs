using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] FoodObject itemInSlot;
    [SerializeField] public Button button;
    [SerializeField] TMP_Text itemCost;
    [SerializeField] TMP_Text quantityText;



    private Shop shopReference;

    private void Start()
    {
        shopReference = FindObjectOfType<Shop>();
        updateSlot();
    }

    public void updateSlot()
    {
        button.image.sprite = itemInSlot.thumbnail;
        itemCost.text = itemInSlot.cost.ToString();
        quantityText.text = itemInSlot.currentStock.ToString();

        if (GameManager.Instance.money >= itemInSlot.cost && itemInSlot.currentStock > 0)
        {
            button.interactable = true;
        }

    }

    public void addToInventory(InventoryObject inventory)
    {
        if (itemInSlot.currentStock > 0 && GameManager.Instance.money >= itemInSlot.cost)
        {
            inventory.inventory.Add(itemInSlot);
            button.interactable = false;
            GameManager.Instance.money -= itemInSlot.cost;
            shopReference.updateMoney();
            itemInSlot.currentStock--;
            itemInSlot.quantity++;
            updateSlot();
        }
    }

    public void restock()
    {
        button.interactable = true;
        itemInSlot.currentStock = itemInSlot.maxStock;
        updateSlot();

        Debug.Log("Restocked");
    }

    
}
