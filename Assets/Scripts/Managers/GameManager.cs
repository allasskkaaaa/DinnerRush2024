using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

[DefaultExecutionOrder(-1)]

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance => _instance;

    public Action<int> OnLifeValueChange;

    [SerializeField] public float restaurantScore = 0;
    [SerializeField] public List<float> allRatings;
    [SerializeField] public float highScore = 0;
    [SerializeField] private StarTracker starTracker;
    [SerializeField] public int money = 1000;
    [SerializeField] public TMP_Text[] moneyTexts;

    [HideInInspector] public bool newHighScore;

    

    [SerializeField] private InventoryObject boughtInventory;
    private void Awake()
    {

        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject); // Destroys duplicate instances
        }

        LoadPlayer();


    }

    private void OnEnable()
    {
        LoadPlayer();
    }

    private void OnDisable()
    {
        SavePlayer();
    }

    private void Start()
    {
        Time.timeScale = 1;
        restaurantScore = 0;

        updateMoney(0);

        _instance = this;

        if (starTracker != null)
        starTracker.UpdateStars();

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to prevent memory leaks
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        starTracker = FindObjectOfType<StarTracker>();
        if (starTracker != null)
            starTracker.UpdateStars();
    }
    public void LoadScene(int scene)
    {
        Debug.Log("Loading scene " +  scene);
        SceneManager.LoadScene(scene);
    }



    public void calculateRestaurantScore()
    {
        float sum = 0;

        float average;

        for (int i = 0; i < allRatings.Count; i++)
        {
            sum += allRatings[i];
        }

        average = sum / allRatings.Count;

        restaurantScore = average;

        starTracker.UpdateStars();
    }
    public void updateMoney(int newAmount)
    {
        money += newAmount;
        if (moneyTexts.Length > 0)
            foreach (TMP_Text moneyText in moneyTexts)
            moneyText.text = money.ToString();
    }
    public void setHighScore()
    {
        
        if (restaurantScore > highScore)
        {
            Debug.Log("New high score!");
            highScore = restaurantScore;

            newHighScore = true;
            SavePlayer();
        }
        else
        {
            newHighScore = false;
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.loadPlayer();

        if (data != null)
        {
            highScore = data.highScore;
            money = data.money;
            updateMoney(0);
        }
        
    }

   

    public void deleteData()
    {
        SaveSystem.ResetPlayerData();
        money = 0;
        updateMoney(0);
        highScore = 0;
        restaurantScore = 0;

        foreach (FoodObject item in boughtInventory.inventory)
        {
            item.quantity = 0;
            item.currentStock = item.maxStock;
        }

        boughtInventory.inventory.Clear();

        SaveSystem.SavePlayer(this);

        Debug.Log("Data reset");
    }
}
