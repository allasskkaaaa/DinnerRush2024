using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button start;

    private void Start()
    {
        start.onClick.AddListener(startGame);
    }

    private void startGame()
    {
        SceneManager.LoadScene(1);
    }

}
