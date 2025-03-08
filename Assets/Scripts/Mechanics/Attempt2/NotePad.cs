using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotePad : MonoBehaviour
{
    [Header("Object references")]
    [SerializeField] private KitchenV2 kitchen;
    [SerializeField] private Button openNotepad_BTN;
    [SerializeField] private GameObject notepadPanel;
    [SerializeField] private bool isNotePadOpen;

    [Header("Orders")]
    [SerializeField] private Button slot1;
    [SerializeField] private Button slot2;
    [SerializeField] private Button slot3;
    [SerializeField] private Inventory menu;
    


    private void Start()
    {

        if (openNotepad_BTN != null) openNotepad_BTN.onClick.AddListener(() => openNotePad());
        
    }

    private void openNotePad()
    {
        if (isNotePadOpen)
        {
            Debug.Log("Closing notepad");
            notepadPanel.SetActive(false);
            isNotePadOpen = false;
        }
        else if (!isNotePadOpen)
        {
            Debug.Log("Opening notepad");
            notepadPanel.SetActive(true);
            isNotePadOpen = true;
        }
    }

    private void sendOrder(FoodObject order)
    {
        
        KitchenV2.instance.makeOrder(order);
        Debug.Log("Sent " + order.name + " to the kitchen.");
    }

    public void initializeMenuButtons()
    {
        slot1.GetComponent<SlotManager>().itemInSlot = menu.list[0];
        slot1.GetComponent<SlotManager>().updateSlot();

        slot2.GetComponent<SlotManager>().itemInSlot = menu.list[1];
        slot2.GetComponent<SlotManager>().updateSlot();

        slot3.GetComponent<SlotManager>().itemInSlot = menu.list[2];
        slot3.GetComponent<SlotManager>().updateSlot();


        if (slot1 != null) slot1.onClick.AddListener(() => sendOrder(slot1.GetComponent<SlotManager>().itemInSlot));
        if (slot2 != null) slot2.onClick.AddListener(() => sendOrder(slot2.GetComponent<SlotManager>().itemInSlot));
        if (slot3 != null) slot3.onClick.AddListener(() => sendOrder(slot3.GetComponent<SlotManager>().itemInSlot));
    }

}
