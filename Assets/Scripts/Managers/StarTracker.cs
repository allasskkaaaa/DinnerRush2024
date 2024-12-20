using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class StarTracker : MonoBehaviour
{
    [SerializeField] private Image[] stars; // Array of star images
    [SerializeField] private float maxScorePerStar = 1; // Max score for each star
    [SerializeField] private float maxTotalScore = 5; // Max score for each star
    private float totalScore = 0; // Cached score to avoid redundant updates


    private void Update()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null. Ensure GameManager is initialized.");
            return;
        }
        if (GameManager.Instance.restaurantScore != totalScore) // Update only if score changes
        {
            totalScore = GameManager.Instance.restaurantScore;
            UpdateStars();
        }
    }

    private void UpdateStars()
    {
        float remainingScore = totalScore;


        for (int i = 0; i < stars.Length; i++)
        {
            if (remainingScore > maxScorePerStar) // Fully fill the star
            {
                stars[i].fillAmount = 1f;
                remainingScore -= maxScorePerStar;
            }
            else // Partially fill the star
            {
                stars[i].fillAmount = remainingScore / maxScorePerStar;
                remainingScore = 0; // No more score to distribute
            }
        }
    }


}
