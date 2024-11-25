using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class SlowFillStars : MonoBehaviour
{
    [SerializeField] private Image[] stars; // Array of star images
    [SerializeField] private int maxScorePerStar = 100; // Max score for each star
    [SerializeField] private int maxTotalScore = 500; // Max score for each star
    private int totalScore; // Cached score to avoid redundant updates
    private float secondsToShow = 1f;
    private bool isFilling = false;

    private void Start()
    {

        fillScore();
    }
    public void fillScore()
    {
        
        isFilling = true;
        StartCoroutine(FillScoreGradually(secondsToShow));
    }

    private IEnumerator FillScoreGradually(float duration)
    {
        yield return new WaitForSeconds(0.5f); // Add a short delay here to allow scene to load

        int scoreGot = GameManager.Instance.playerScore;
        totalScore = 0;

        float delay = scoreGot > 0 ? duration / scoreGot : 0;

        for (int i = 0; i < scoreGot; i++)
        {
            totalScore++;
            UpdateStars();
            yield return new WaitForSeconds(delay);
        }

        isFilling = false;
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
