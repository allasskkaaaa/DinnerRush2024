using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    // Appearance Settings
    [Header("Appearance Settings")]
    [SerializeField] private Sprite[] appearanceVarieties; // Different cat appearances they can use
    [SerializeField] private SpriteRenderer sr; // Reference to customer sprite renderer
    [HideInInspector] public int zOrder = 6; // Z order the sprite lays on

    // Interaction Settings
    [Header("Interaction Settings")]
    [SerializeField] private float scaleMultiplier; // When the cat is hovered over, local scale is multiplied by this value
    private Vector3 originalScale; // Tracks original scale
    private bool isServed; // Customer was served

    // Order Settings
    [Header("Order Settings")]
    [SerializeField] private GameObject order; // Cat's order
    [SerializeField] private List<GameObject> menuSelection; // All the possible orders the customer will select
    private bool checkingOrder;
    private bool wasServed;
    private bool hasOrdered; // Track if they've ordered already or not
    private GameObject currentThought; // Track current thought

    // Speech Bubble Settings
    [Header("Speech Bubble Settings")]
    [SerializeField] private GameObject speechBubble; // Reference to the speech bubble object
    [SerializeField] private GameObject speechBubbleSlot; // Reference to the slot in speech bubble
    private Animator speechAnim; // Reference to speech bubble animator

    // Timing Settings
    [Header("Timing Settings")]
    [SerializeField] private float thinkingTime; // Time until the cat first orders
    [SerializeField] private float minDecideTime = 2; // Min time to first order
    [SerializeField] private float maxDecideTime = 4; // Max time to first order

    // Animation Settings
    [Header("Animation Settings")]
    private Animator anim; // Reference to cat animator


    private bool isSelectable;
    private void Awake()
    {
        speechAnim = speechBubble.GetComponent<Animator>();

        anim = GetComponent<Animator>();

        speechAnim.Play("HeadEmpty");

        originalScale = sr.gameObject.transform.localScale;
        thinkingTime = Random.Range(minDecideTime, maxDecideTime);
        int randomInt = Random.Range(0, appearanceVarieties.Length);
        sr.sprite = appearanceVarieties[randomInt];
        
        order = menuSelection[Random.Range(0, menuSelection.Count)];
        Debug.Log("Z order = " + zOrder);
    }

    private void Start()
    {
        sr.sortingOrder = zOrder;

    }
    private void Update()
    {
        if (thinkingTime > 0)
        {
            thinkingTime -= Time.deltaTime;
        }
        else if (!hasOrdered)
        { 
            StartCoroutine(thinkOrder(order.tag));
        }
        
    }

    private IEnumerator thinkOrder(string tag)
    {
        checkingOrder = true;
        Debug.Log("Thinking: " + tag);
       
        for (int i = 0; i < menuSelection.Count; i++) 
        {
            Debug.Log(menuSelection[i].tag);
            if (order.tag == menuSelection[i].tag)
            {
                speechAnim.Play("Form");
                currentThought = Instantiate(menuSelection[i], speechBubbleSlot.transform);
                hasOrdered = true;
                Debug.Log("Current thought: " + currentThought.tag);
                yield return new WaitForSeconds(3);
                
            }
        }
        speechAnim.Play("HeadEmpty");
        Destroy(currentThought.gameObject);
        checkingOrder = false;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Draggable draggable = collision.GetComponent<Draggable>();

        if (draggable != null)
        {
            selected();
        }
        else if (collision.CompareTag("Finger") && draggable == null && !checkingOrder)
        {
            StartCoroutine(thinkOrder(order.tag));
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Finger"))
            return;

        Draggable draggable = collision.GetComponent<Draggable>();

       
        if (draggable != null && !wasServed)
        {
            if (!draggable.isDragging && !collision.CompareTag("Finger"))
            {
                if (checkOrder(collision.gameObject))
                {
                    Debug.Log("Order match: " + checkOrder(collision.gameObject));
                    playThought("Happy");
                    wasServed = true;
                    StartCoroutine(leave());
                    draggable.gameObject.SetActive(false);
                    Destroy(draggable.gameObject, 0.1f);
                    return;
                }
                else
                {
                    Debug.Log("Order match: " + checkOrder(collision.gameObject));
                    playThought("Angry");
                    draggable.gameObject.SetActive(false);
                    Destroy(draggable.gameObject, 0.1f);
                    return;
                }
            }
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        unselected();
    }

    private void selected()
    {
        sr.gameObject.transform.localScale = sr.gameObject.transform.localScale * scaleMultiplier;
    }

    private void unselected()
    {
        Debug.Log("Changing scale");
        sr.gameObject.transform.localScale = originalScale;
    }

    private IEnumerator playThought(string thought)
    {
        speechAnim.Play(thought);
        yield return new WaitForSeconds(3);

        speechAnim.Play("HeadEmpty");
    }

    private IEnumerator leave()
    {
        yield return new WaitForSeconds(3);

        anim.Play("Leave");

        yield return new WaitForSeconds(1);

        Destroy(gameObject);

    }

    private bool checkOrder(GameObject checkOrder)
    {
        return (order.CompareTag(checkOrder.tag));
    }

}
