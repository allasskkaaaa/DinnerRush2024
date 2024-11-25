using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PlayerController : MonoBehaviour
{
    // Movement Variables
    [Header("Movement Parameters")]
    [SerializeField] private InputActionReference moveActionReference;
    [SerializeField] public float moveSpeed = 5;

    private Vector2 movement;
    private Vector2 movementInput;

    //Components
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        Vector2 moveDirection = moveActionReference.action.ReadValue<Vector2>();
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }


}
