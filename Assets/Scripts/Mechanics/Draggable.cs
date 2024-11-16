using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class Draggable : MonoBehaviour
{
    public bool isDragging;

    private Collider2D _collider;

    private DragController dragController;

    private float movementTime = 15f;
    private System.Nullable<Vector3> movementDestination;
    private Vector3 lastPosition;
    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        dragController = FindObjectOfType<DragController>();
    }

    //If the player drops an object on another draggable object, it moves the object so it doesn't overlap.
    void OnTriggerEnter2D(Collider2D other)
    {
        Draggable collidedDraggable = other.GetComponent<Draggable>();

        if (collidedDraggable != null && dragController.LastDragged.gameObject == gameObject)
        {
            //Finds the nearest distance where the objects wouldn't collide and moves the dropped object to that position.
            ColliderDistance2D colliderDistance2D = other.Distance(_collider);
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;
            transform.position -= diff;
        }
    }
}
