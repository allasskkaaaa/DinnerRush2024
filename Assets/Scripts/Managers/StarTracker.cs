using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class StarTracker : MonoBehaviour
{
    [SerializeField] private Image[] stars; // Array of star images
    [SerializeField] private int maxScorePerStar = 100; // Max score for each star
    [SerializeField] private int maxTotalScore = 500; // Max score for each star
    private int totalScore; // Cached score to avoid redundant updates
    private bool isFilling = false;

    private void Update()
    {
        if (GameManager.Instance.restaurantScore != totalScore && !isFilling) // Update only if score changes
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
