using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] public float restockCooldown = 30f; // 10 minutes
    [SerializeField] private float remainingTime;
    [SerializeField] private bool timerRunning = false;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private ShopSlot[] shopSlots;

    private void Start()
    {
        updateMoney();
        LoadTimer();
    }

    public void updateMoney()
    {
        moneyText.text = GameManager.Instance.money.ToString();
        Debug.Log("Money updated");
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
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0)
            {
                remainingTime = 0;
                timerRunning = false;
                restockItems();
            }
            UpdateTimerDisplay();
        }
    }

    private void SaveTimer()
    {
        if (timerRunning)
        {
            PlayerPrefs.SetString("SavedTime", DateTime.Now.ToString());
            PlayerPrefs.SetFloat("RemainingTime", remainingTime);
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

            remainingTime = PlayerPrefs.GetFloat("RemainingTime", restockCooldown) - (float)elapsed.TotalSeconds;

            if (remainingTime > 0)
            {
                timerRunning = true;
            }
            else
            {
                remainingTime = restockCooldown;
                timerRunning = true;
                Debug.Log("Timer already finished.");
                restockItems();
            }
        }
        else
        {
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
            if (!shopSlot.button.interactable)
            {
                shopSlot.restock();
            }
        }

        remainingTime = restockCooldown;
        timerRunning = true;
    }
}
