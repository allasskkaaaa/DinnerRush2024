using UnityEngine;

public class Spill : MonoBehaviour
{
    [SerializeField] private int requiredSwipes = 3; // Number of directional changes needed
    [SerializeField] private int swipeCount = 0; // Count the number of swipes

    [SerializeField] Vector2 entryPoint;
    [SerializeField] Vector2 exitPoint;
    [SerializeField] float timeBetween;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Finger"))
            return;

        if (ToolManager.instance.currentTool == ToolManager.CurrentTool.bucket)
        {
            timeBetween = 0;
            entryPoint = other.transform.position;
        }
        else
        {
            Debug.Log("Bucket not equipped");
            return;
        }

        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        timeBetween += Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Finger"))
            return;

        if (ToolManager.instance.currentTool == ToolManager.CurrentTool.bucket)
        {
            exitPoint = other.transform.position;
            DetectSwipe();
        }
        else
        {
            Debug.Log("Bucket not equipped");
            return;
        }
        

        
    }
    private void DetectSwipe()
    {
        if (timeBetween < 1)
        {
            swipeCount++;
        }

        if (swipeCount >= requiredSwipes)
        {
            Destroy(gameObject);
        }
        
    }
}
