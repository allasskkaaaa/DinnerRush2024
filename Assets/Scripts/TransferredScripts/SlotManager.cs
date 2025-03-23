using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{

    [SerializeField] public FoodObject itemInSlot;

    [Header("Slot Properties")]
    [SerializeField] private Image thumbnail;
    [SerializeField] private GameObject quantitySlot;
    [SerializeField] private TMP_Text quantityText;


    public void updateSlot()
    {
        if (itemInSlot != null)
        {
            if (itemInSlot.quantity > 0)
            {
                if (quantitySlot != null) quantitySlot.SetActive(true);
                if (quantitySlot != null) quantityText.SetText(itemInSlot.quantity.ToString());
            }

            thumbnail.gameObject.SetActive(true);
            thumbnail.sprite = itemInSlot.thumbnail;

        }
        else
        {
            if (quantitySlot != null) quantitySlot.SetActive(false);
            thumbnail.gameObject.SetActive(false);
        }

    }

    public void removeFromSlot()
    {
        if (itemInSlot != null)
        {
            itemInSlot.quantity += 1;
            itemInSlot = null;

            updateSlot();
        }
        

    }


    public FoodObject returnItemInSlot()
    {
        return itemInSlot;
    }
}
