using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class Draggable : MonoBehaviour
{
    public bool isDragging;

    private Collider2D _collider;

    private DragController dragController;
}
