using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public float totalTime = 300f; //5 minutes
    public float currentTime;
    public bool isDone;

    [SerializeField] private CanvasManager canvas; 
    private void Start()
    {
        currentTime = totalTime;
    }

    private void Update()
    {
        if (!isDone)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime; //Decrease the timer
                currentTime = Mathf.Max(currentTime, 0); //Avoids negative time
                UpdateTimerUI();
            }
            else
            {
                canvas.endScreen();
                isDone = true;
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60); //Calculate minutes
        int seconds = Mathf.FloorToInt(currentTime % 60); //Calculate Seconds

        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }
}
