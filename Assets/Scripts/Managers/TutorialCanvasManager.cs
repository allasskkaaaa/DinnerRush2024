using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button openNotePad;
    [SerializeField] private Button openFood;
    [SerializeField] private Button openGarbage;
    [SerializeField] private Button openBucket;
    [SerializeField] private Button openCustomer;

    [Header("Descriptions")]
    [SerializeField] private GameObject foodDescription;
    [SerializeField] private GameObject garbageDescription;
    [SerializeField] private GameObject bucketDescription;
    [SerializeField] private GameObject customerDescription;

    [Header("Panels")]
    [SerializeField] private GameObject notepadPanel;

    [Header("Other")]
    [SerializeField] private Animator tutorialCatAnim;
    [SerializeField] private AudioClip selectSFX;

    private void Start()
    {
        if (openFood != null) openFood.onClick.AddListener(() => openNotepadTutorial());
        if (openGarbage != null) openGarbage.onClick.AddListener(() => garbageTutorial());
        if (openBucket != null) openBucket.onClick.AddListener(() => bucketTutorial());
        if (openCustomer != null) openCustomer.onClick.AddListener(() => customerTutorial());

    }
    private void openNotepadTutorial()
    {
        if (foodDescription.activeSelf)
        {
            foodDescription.SetActive(false);
            notepadPanel.SetActive(false);
        }
        else
        {
            foodDescription.SetActive(true);
            notepadPanel.SetActive(true);
        }
    }

    private void garbageTutorial()
    {
        if (garbageDescription.activeSelf)
        {
            garbageDescription.SetActive(false);
        }
        else
        {
            garbageDescription.SetActive(true);
        }
    }

    private void bucketTutorial()
    {
        if (bucketDescription.activeSelf)
        {
            bucketDescription.SetActive(false);
        }
        else
        {
            bucketDescription.SetActive(true);
        }
    }

    private void customerTutorial()
    {
        if (customerDescription.activeSelf) 
        {
            customerDescription.SetActive(false); 
        }
        else
        {
            customerDescription.SetActive(true); 
        }
    }


    private void closeAllTutorials()
    {
        foodDescription.SetActive(false);
        garbageDescription.SetActive(false);
        bucketDescription.SetActive(false);
        customerDescription.SetActive(false);
    }
}
