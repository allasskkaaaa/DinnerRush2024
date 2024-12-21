using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MenuStarTracker : MonoBehaviour
{
    [SerializeField] private Image[] stars; // Array of star images

    private void Start()
    {
        UpdateStars();
    }
    public void UpdateStars()
    {
        float remainingScore = GameManager.Instance.highScore;


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
