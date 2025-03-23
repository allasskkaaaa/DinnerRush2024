using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{

    [SerializeField] private float minimumDistance = .2f;
    [SerializeField] private float maximumTime = 1f;

    [SerializeField] private GameObject trail;

    public InputManager inputManager;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;


    [SerializeField, Range(0,1)] private float directionThreshold = 0.9f;

    private Coroutine coroutine;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }


    private void OnEnable()
    {
        inputManager.onStartTouch += SwipeStart;
        inputManager.onEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.onStartTouch -= SwipeStart;
        inputManager.onEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
        trail.transform.position = position;
        trail.SetActive(true);
        coroutine = StartCoroutine(Trail());
    }

    private IEnumerator Trail()
    {
        while (true)
        {
            trail.transform.position = inputManager.PrimaryPosition();
            yield return null;
        }
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        trail.SetActive(false);
        StopCoroutine(coroutine);
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
             Debug.DrawLine(startPosition, endPosition, Color.red, 5);

            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            //SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection (Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up,direction) > directionThreshold)
        {
            Debug.Log("Swipe Up");
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Debug.Log("Swipe Down");
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Debug.Log("Swipe Left");
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Debug.Log("Swipe Right");
        }
    }
}
