using UnityEngine;
using UnityEngine.UI;

public class GarbageMechanic : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSFX;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ToolManager.instance.currentTool == ToolManager.CurrentTool.garbage)
        {
            Debug.Log("Garbage piece detected");
            GarbagePiece garbagePiece = collision.GetComponent<GarbagePiece>();
            if (garbagePiece != null)
            {
                AudioManager.instance.playOneShot(pickupSFX);
                Debug.Log("Removing Garbage");
                garbagePiece.spawnNode.isOccupied = false;
                Destroy(collision.gameObject);
            }
        }
        else
        {
            Debug.Log("Finger is equipped. Unable to pick up garbage");
        }
        
    }

}
