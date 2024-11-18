using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarTracker : MonoBehaviour
{
    [SerializeField] private Image[] stars; // Array of star images
    [SerializeField] private int maxScorePerStar = 100; // Max score for each star
    private float totalScore; // Cached score to avoid redundant updates

    private void Update()
    {
        if (GameManager.Instance.playerScore != totalScore) // Update only if score changes
        {
            totalScore = GameManager.Instance.playerScore;
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
