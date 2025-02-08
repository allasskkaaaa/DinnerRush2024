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
    [SerializeField] public List<FoodObject> menu;

    [HideInInspector] public bool newHighScore;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject); // Destroys duplicate instances
        }

        LoadPlayer();


    }

    private void Start()
    {
        Time.timeScale = 1;
        restaurantScore = 0;

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
        }
        
    }
   
    public void deleteData()
    {
        SaveSystem.ResetPlayerData();
    }
}
