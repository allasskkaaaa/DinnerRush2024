using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance => _instance;

    public Action<int> OnLifeValueChange;

    [SerializeField] public Timer timer;
    [SerializeField] public float restaurantScore = 0;
    [SerializeField] public List<float> allRatings;
    [SerializeField] private Image[] stars; // Array of star images

    


    private void Start()
    {
        _instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void GameOver()
    {
        Debug.Log("GameOver goes here");
        SceneManager.LoadScene(2);
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
        UpdateStars();
    }

    private void UpdateStars()
    {
        float remainingScore = restaurantScore;


        for (int i = 0; i < stars.Length; i++)
        {
            if (remainingScore > 1) // Fully fill the star
            {
                stars[i].fillAmount = 1f;
                remainingScore -= 1;
            }
            else // Partially fill the star
            {
                stars[i].fillAmount = remainingScore / 1;
                remainingScore = 0; // No more score to distribute
            }
        }
    }
}
