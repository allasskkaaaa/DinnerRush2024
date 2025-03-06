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
                quantitySlot.SetActive(true);
                quantityText.SetText(itemInSlot.quantity.ToString());
            }

            thumbnail.gameObject.SetActive(true);
            thumbnail.sprite = itemInSlot.thumbnail;

            Debug.Log("Slot values updated!");
        }
        else
        {
            quantitySlot.SetActive(false);
            thumbnail.gameObject.SetActive(false);
        }
    }

    public void removeFromSlot()
    {
        itemInSlot = null;

        updateSlot();

    }
}
