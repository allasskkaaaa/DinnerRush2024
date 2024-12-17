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

    

    private int _lives;
    public int lives
    {
        get => _lives;
        set
        {
            if (value <= 0)
            {
                GameOver();
                return;
            }
            if (value < _lives) Respawn();
            if (value > maxLives) value = maxLives;
            _lives = value;

            OnLifeValueChange?.Invoke(_lives);

            Debug.Log($"Lives have been set to {_lives}");
            //broadcast can happen here
        }
    }

    [SerializeField] private int maxLives = 5;
    [SerializeField] private PlayerController playerPrefab;
    [HideInInspector] public PlayerController PlayerInstance => _playerInstance;
    PlayerController _playerInstance = null;
    Transform currentCheckpoint;
    [SerializeField] private Transform startingSpawn;
    public Action<PlayerController> OnPlayerSpawned;
    [SerializeField] public Timer timer;
    [SerializeField] public int playerScore;

    private bool isPaused;
   

    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);

        
    }

    private void Start()
    {
        if (maxLives <= 0)
        {
            maxLives = 5;
        }
        lives = maxLives;

      

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Game")
                SceneManager.LoadScene(0);
            else
                SceneManager.LoadScene(1);
        }
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

    public void Respawn()
    {
        _playerInstance.transform.position = currentCheckpoint.position;
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        _playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        currentCheckpoint = spawnLocation;
    }


    public void UpdateCheckpoint(Transform updatedCheckpoint)
    {
        currentCheckpoint = updatedCheckpoint;
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void unpauseGame()
    {
        Time.timeScale = 1;
    }
}
