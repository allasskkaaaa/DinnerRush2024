using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Customer : MonoBehaviour
{
    // Appearance Settings
    [Header("Cat Settings")]
    [SerializeField] private Sprite[] appearanceVarieties; // Different cat appearances they can use
    [SerializeField] private SpriteRenderer sr; // Reference to customer sprite renderer
    [SerializeField] private float cleanlinessRating = 5;
    [SerializeField] private float patience = 20;
    [SerializeField] private int orderSatisfaction = 5;
    [SerializeField] private float overallSatisfaction;

    // Interaction Settings
    [Header("Interaction Settings")]
    [SerializeField] private float scaleMultiplier; // When the cat is hovered over, local scale is multiplied by this value
    private Vector3 originalScale; // Tracks original scale

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
    private ObstacleManager obstacleManager;
    public SpawnNode spawnNode;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip meow;
    [SerializeField] private AudioClip angry_meow;
    [SerializeField] private AudioClip hungry_meow;

    private bool isScaled;

    private Coroutine checkingCleanliness;
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

        obstacleManager = FindObjectOfType<ObstacleManager>();
    }

    private void Start()
    {
        sr.sortingOrder = spawnNode.zOrder;
        sr.flipX = spawnNode.xFlipped;

        AudioManager.instance.playOneShot(meow);

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

        if (checkingCleanliness == null)
        {
            checkingCleanliness = StartCoroutine(checkCleanliness());
        }

        if (patience > 0 && !wasServed)
        {
            patience -= Time.deltaTime;
        }
        
        if ((patience <= 0 || orderSatisfaction <= 0) && !wasServed)
        {
            orderSatisfaction = 0;
            cleanlinessRating = 0;
            patience = 0;
            StartCoroutine(leave());
        }
        
    }

    private IEnumerator thinkOrder(string tag)
    {
        checkingOrder = true;
       
        for (int i = 0; i < menuSelection.Count; i++) 
        {
            if (order.tag == menuSelection[i].tag)
            {
                speechAnim.Play("Form");
                currentThought = Instantiate(menuSelection[i], speechBubbleSlot.transform);
                hasOrdered = true;
                AudioManager.instance.playOneShot(hungry_meow);
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
        selected();
        Draggable draggable = collision.GetComponent<Draggable>();
        
        if (collision.CompareTag("Finger") && (collision.transform.childCount <= 0) && !wasServed)
        {
            StartCoroutine(thinkOrder(order.tag));
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Finger"))
            return;

        Draggable draggable = collision.GetComponent<Draggable>();
        Food food = collision.GetComponent<Food>();
      
        if (food != null)
        {
            if (draggable != null && !wasServed)
            {
                if (!draggable.isDragging && !collision.CompareTag("Finger"))
                {
                    if (checkOrder(collision.gameObject))
                    {
                        AudioManager.instance.playOneShot(meow);
                        StartCoroutine(playThought("Happy"));
                        wasServed = true;
                        StartCoroutine(leave());
                        draggable.gameObject.SetActive(false);
                        Destroy(draggable.gameObject, 0.1f);
                        return;
                    }
                    else
                    {
                        AudioManager.instance.playOneShot(angry_meow);
                        orderSatisfaction--;
                        StartCoroutine(playThought("Angry"));
                        draggable.gameObject.SetActive(false);
                        Destroy(draggable.gameObject, 0.1f);
                        return;
                    }
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
        if (!isScaled)
        {
            sr.gameObject.transform.localScale = sr.gameObject.transform.localScale * scaleMultiplier;
            isScaled = true;
            Debug.Log("Scaling object");
        }
    }

    private void unselected()
    {
        sr.gameObject.transform.localScale = originalScale;
        isScaled = false;
    }

    private IEnumerator playThought(string thought)
    {

        speechAnim.SetTrigger(thought);

        yield return new WaitForSeconds(3);

        speechAnim.Play("HeadEmpty");

    }


    private IEnumerator leave()
    {
        yield return new WaitForSeconds(3);

        anim.Play("Leave");

        yield return new WaitForSeconds(1);


        overallSatisfaction += ((5*cleanlinessRating) + (patience/4) + orderSatisfaction)/3;

        GameManager.Instance.allRatings.Add(overallSatisfaction);
        GameManager.Instance.calculateRestaurantScore();

        int randomGarbageAmount = 0;

        if (overallSatisfaction < 3.5)
        {
            randomGarbageAmount = Random.Range(2,5);
        }
        else if (overallSatisfaction >= 3.5)
        {
            randomGarbageAmount = Random.Range(0, 2);
        }

        for (int i = 0; i < randomGarbageAmount; i++)
            obstacleManager.spawnObstacle();

        spawnNode.isOccupied = false;
        Destroy(gameObject);

    }

    private bool checkOrder(GameObject checkOrder)
    {
        return (order.CompareTag(checkOrder.tag));
    }

    private IEnumerator checkCleanliness()
    {

        Obstacle[] possibleGarbage = FindObjectsOfType<Obstacle>();

        List<Obstacle> garbageOnFloor = new List<Obstacle>();

        for (int i = 0; i < possibleGarbage.Length; i++)
        {
            if (possibleGarbage[i].isOccupied)
            {
                garbageOnFloor.Add(possibleGarbage[i]);
            }
        }

        cleanlinessRating = 1 - ((float)garbageOnFloor.Count / possibleGarbage.Length);


        yield return new WaitForSeconds(1);

        checkingCleanliness = null;

    }

}
