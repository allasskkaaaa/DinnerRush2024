using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public Draggable LastDragged => lastDragged;
    private bool isDragActive = false;
    private Vector2 screenPosition;
    private Vector3 worldPosition;
    private Draggable lastDragged;
    private Food item;
    private Rigidbody2D itemRB;
    private List<Collider2D> safeZones = new List<Collider2D>();
    private Vector2 lastPosition;
    [SerializeField] private Transform parent;

    private void Awake()
    {
        // Ensures only one DragController exists
        DragController[] controllers = FindObjectsOfType<DragController>();
        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Ensure lastDragged is valid
        if (lastDragged != null && lastDragged.gameObject == null)
        {
            ClearReferences();
            return;
        }

        // Check for drop action
        if (isDragActive && (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            Drop();
            return;
        }

        // Get screen position
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        // Convert screen position to world position
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // Handle dragging or initiate drag
        if (isDragActive)
        {
            if (lastDragged != null && lastDragged.gameObject != null)
            {
                Drag();
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if (draggable != null)
                {
                    lastDragged = draggable;
                    item = draggable.GetComponent<Food>();
                    itemRB = draggable.GetComponent<Rigidbody2D>();
                    InitDrag();
                }
            }
        }
    }

    void InitDrag()
    {
        isDragActive = true;
        if (item != null)
        {
            item.pickedUp = true;
            if (itemRB != null)
            {
                itemRB.velocity = Vector2.zero;
                itemRB.isKinematic = true;
            }
        }
    }

    void Drag()
    {
        if (lastDragged == null || lastDragged.gameObject == null) return;

        lastDragged.transform.position = new Vector2(worldPosition.x, worldPosition.y);
        lastDragged.transform.parent = parent;
    }

    void Drop()
    {
        isDragActive = false;

        if (item != null)
        {
            item.pickedUp = false;
            if (itemRB != null)
            {
                itemRB.isKinematic = false;
            }
        }

        // Clear references to prevent accessing destroyed objects
        if (lastDragged != null)
        {
            lastDragged.transform.parent = null;
        }

        ClearReferences();
    }

    void ClearReferences()
    {
        lastDragged = null;
        item = null;
        itemRB = null;
    }
}
