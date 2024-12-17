using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static NPC;

public class NotePad : MonoBehaviour
{
    [Header("Object references")]
    [SerializeField] private KitchenV2 kitchen;
    [SerializeField] private Button openNotepad_BTN;
    [SerializeField] private GameObject notepadPanel;
    [SerializeField] private bool isNotePadOpen;

    [Header("Orders")]
    [SerializeField] private Button cakeBTN;
    [SerializeField] private GameObject cakeObject;
    [SerializeField] private Button latteBTN;
    [SerializeField] private GameObject latteObject;
    [SerializeField] private Button sandwichBTN;
    [SerializeField] private GameObject sandwichObject;

    private void Start()
    {

        if (openNotepad_BTN != null) openNotepad_BTN.onClick.AddListener(() => openNotePad());
        if (cakeBTN != null) cakeBTN.onClick.AddListener(() => sendOrder(cakeObject));
        if (latteBTN != null) latteBTN.onClick.AddListener(() => sendOrder(latteObject));
        if (sandwichBTN != null) sandwichBTN.onClick.AddListener(() => sendOrder(sandwichObject));
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

    private void sendOrder(GameObject order)
    {
        KitchenV2.instance.makeOrder(order);
        Debug.Log("Sent " + order.name + " to the kitchen.");
    }
}
