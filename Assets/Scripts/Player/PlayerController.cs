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
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float sprintSpeed = 8;
    [SerializeField] private float sprintStamina = 10;
    [SerializeField] private float StaminaDrain = 1;
    [SerializeField] private float StaminaRegen = 1;
    [SerializeField] private float dashSpeed = 10;
    [SerializeField] private float dashDuration = 0.2f;
    private bool isDashing = false;
    private bool isSprinting = false;
    private Vector2 movement;

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
    void Update()
    {

        // Flip Sprite
        if (movement.x != 0) sr.flipX = (movement.x < 0);

        // Sprint Input
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSprinting)
        {
            isSprinting = true;
            Debug.Log("Sprinting");
        }

        // Stop Sprinting if Shift is released
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            Debug.Log("Stopped sprinting");
        }


        // Dash Input
        if (Input.GetButtonDown("Jump") && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = moveActionReference.action.ReadValue<Vector2>();
        
        if (!isDashing)
        {
            if (!isSprinting)
            {
                //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

                transform.Translate(moveDirection * dashSpeed* Time.deltaTime);
            }
            else
            {
                //rb.velocity = movement * sprintSpeed;

            }
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        rb.velocity = movement * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector2.zero; // Stop the dash
        isDashing = false;
    }


}
