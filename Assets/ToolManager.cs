using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolManager : MonoBehaviour
{
    public static ToolManager instance;


    [Header("Buttons")]
    [SerializeField] private Button garbageButton;
    [SerializeField] private Button bucketButton;

    public GameObject garbageHighlightPanel;
    public GameObject spillsHighlightPanel;

    [SerializeField]
    public enum CurrentTool
    {
        garbage,
        bucket,
        finger
    }

    public CurrentTool currentTool;

    private void Start()
    {
        instance = this;

        equipFinger();

        if (garbageButton != null) garbageButton.onClick.AddListener(() => equipGarbage());
        if (bucketButton != null) bucketButton.onClick.AddListener(() => equipBucket());
    }


    private void equipGarbage()
    {
        if (currentTool != CurrentTool.garbage)
        {

            ColorBlock bucketColorBlock = bucketButton.colors;
            bucketColorBlock.normalColor = Color.white;
            bucketButton.colors = bucketColorBlock;
            bucketButton.OnDeselect(null);

            Debug.Log("Garbage was equipped");
            currentTool = CurrentTool.garbage;

            ColorBlock colorBlock = garbageButton.colors;
            colorBlock.normalColor = Color.green;
            garbageButton.colors = colorBlock; // Apply the updated ColorBlock
            garbageButton.OnDeselect(null); 
        }
        else if (currentTool == CurrentTool.garbage)
        {
            ColorBlock colorBlock = garbageButton.colors;
            colorBlock.normalColor = Color.white;
            garbageButton.colors = colorBlock; // Apply the updated ColorBlock
            Debug.Log("Finger was equipped");
            currentTool = CurrentTool.finger;
            garbageButton.OnDeselect(null);
        }
        
    }

    private void equipBucket()
    {
        if (currentTool != CurrentTool.bucket)
        {
            ColorBlock garbageColorBlock = garbageButton.colors;
            garbageColorBlock.normalColor = Color.white;
            garbageButton.colors = garbageColorBlock;
            garbageButton.OnDeselect(null);

            ColorBlock colorBlock = bucketButton.colors;
            colorBlock.normalColor = Color.green;
            bucketButton.colors = colorBlock; // Apply the updated ColorBlock

            Debug.Log("Bucket was equipped");
            currentTool = CurrentTool.bucket;
            bucketButton.OnDeselect(null);
        }
        else if (currentTool == CurrentTool.bucket)
        {

            ColorBlock colorBlock = bucketButton.colors;
            colorBlock.normalColor = Color.white;
            bucketButton.colors = colorBlock; // Apply the updated ColorBlock
            Debug.Log("Finger was equipped");
            currentTool = CurrentTool.finger;
            bucketButton.OnDeselect(null);
        }
        
    }


    private void equipFinger()
    {
        Debug.Log("Finger was equipped");
        currentTool = CurrentTool.finger;

        // Reset color for garbage button

        garbageButton.OnDeselect(null);

        // Reset color for bucket button

        bucketButton.OnDeselect(null);

        // Refresh button state


    }

}
