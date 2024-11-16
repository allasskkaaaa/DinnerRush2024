using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class Draggable : MonoBehaviour
{
    private bool isDragActive = false;
    private Vector2 screenPosition;
    private Vector3 worldPosition;
    private Draggable lastDragged;

    private void Awake()
    {
        //Makes sure there is only one drag controller in the game. Deletes if it detects more than one
        DragController[] controller = FindObjectsOfType<DragController>();
        if (controller.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Checks if the player lets go of mouse button or touch and drops item if detected
        if (isDragActive && (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            Drop();
            return;
        }

        //If the player uses a mouse or touch input on the screen, it collects the position of where they clicked
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if(Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position;
        } else
        {
            return;
        }

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        //Checks if the player is dragging an object. If yes, it constantly calls the drag method. If not, it checks for if the player touch is colliding with a draggable object
        if (isDragActive)
        {
            Drag();
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
                    InitDrag();
                }
            }
        }
    }

    void InitDrag()
    {
        isDragActive = true;
    }

    void Drag()
    {
        lastDragged.transform.position = new Vector2(worldPosition.x, worldPosition.y);
    }

    void Drop()
    {
        isDragActive = false;
    }
}
