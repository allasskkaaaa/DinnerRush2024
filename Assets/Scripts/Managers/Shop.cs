using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] public float restockCooldown = 30f; // Cooldown time (e.g., 30 seconds for testing)
    [SerializeField] private float remainingTime;
    [SerializeField] private bool timerRunning = false;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private ShopSlot[] shopSlots;

    private void Start()
    {
        LoadTimer();
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveTimer();
        }
    }

    void OnApplicationQuit()
    {
        SaveTimer();
    }

    void Update()
    {
        if (timerRunning)
        {
            remainingTime -= Time.deltaTime; // Reduce remaining time
            if (remainingTime <= 0) // Time is up, restock the items
            {
                remainingTime = 0;
                timerRunning = false;
                restockItems(); // Trigger restock when time runs out
            }
            UpdateTimerDisplay();
        }
    }

    private void SaveTimer()
    {
        if (timerRunning)
        {
            PlayerPrefs.SetString("SavedTime", DateTime.Now.ToString()); // Save current time
            PlayerPrefs.SetFloat("RemainingTime", remainingTime); // Save remaining time
            PlayerPrefs.Save();
        }
    }

    private void LoadTimer()
    {
        if (PlayerPrefs.HasKey("SavedTime"))
        {
            string savedTime = PlayerPrefs.GetString("SavedTime", DateTime.Now.ToString());
            DateTime lastSavedTime = DateTime.Parse(savedTime);
            TimeSpan elapsed = DateTime.Now - lastSavedTime;

            // Calculate the remaining time based on elapsed time
            remainingTime = PlayerPrefs.GetFloat("RemainingTime", restockCooldown) - (float)elapsed.TotalSeconds;

            // If the remaining time is still positive, resume the timer
            if (remainingTime > 0)
            {
                timerRunning = true;
            }
            else
            {
                // If time expired, start a fresh timer
                remainingTime = restockCooldown;
                timerRunning = true;
                Debug.Log("Timer has expired, restocking items.");
                restockItems(); // Restock items once when time expires
            }
        }
        else
        {
            // No saved data, start the timer fresh
            remainingTime = restockCooldown;
            timerRunning = true;
            restockItems();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void restockItems()
    {
        foreach (ShopSlot shopSlot in shopSlots)
        {
            if (shopSlot.itemInSlot.currentStock < shopSlot.itemInSlot.maxStock)
            {
                shopSlot.restock();
            }
        }

        // Reset the timer for the next restock
        remainingTime = restockCooldown;
        timerRunning = true;
    }
}
