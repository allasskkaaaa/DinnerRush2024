using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private AudioClip selectSFX;
    [SerializeField] private Timer timer;

    [Header("End Screen")]
    public StarTracker starTracker;
    public GameObject endScreenPanel;
    public TMP_Text newHighScore;
    [SerializeField] private Button menuBTN;
    [SerializeField] private Button endRestartBTN;
    [SerializeField] private Button endSettingsBTN;
    [SerializeField] private AudioClip normalEndSFX;
    [SerializeField] private AudioClip newScoreSFX;

    [Header("Pause")]
    [SerializeField] private Button pauseBTN;
    [SerializeField] private Button resumeBTN;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pauseMenuBTN;

    [Header("Settings")]
    [SerializeField] private Button settingsBTN;
    [SerializeField] private Button gearBTN;
    [SerializeField] private GameObject settingsPanel;
    private bool isSettingsOpen;

    [Header("Panels")]
    [SerializeField] private Button BackBTN;
    [SerializeField] private GameObject menusPanel;

    [Header("Other buttons")]
    [SerializeField] private Button restartBTN;
    
    private void Start()
    {
        Time.timeScale = 1;
        if (menuBTN != null) menuBTN.onClick.AddListener(() => returnToMenu());
        if (pauseMenuBTN != null) pauseMenuBTN.onClick.AddListener(() => returnToMenu());
        if (pauseBTN != null) pauseBTN.onClick.AddListener(() => Pause());
        if (resumeBTN != null) resumeBTN.onClick.AddListener(() => Resume());
        if (gearBTN != null) gearBTN.onClick.AddListener(() => settings());
        if (settingsBTN != null) settingsBTN.onClick.AddListener(() => openSettings());
        if (endSettingsBTN != null) endSettingsBTN.onClick.AddListener(() => openSettings()); //End screen
        if (BackBTN != null) BackBTN.onClick.AddListener(() => openMenusOptions());
        if (restartBTN != null) restartBTN.onClick.AddListener(() => restartGame());
        if (endRestartBTN != null) endRestartBTN.onClick.AddListener(() => restartGame()); //End screen
    }

    public void settings()
    {
        AudioManager.instance.playOneShot(selectSFX);
        if (!isSettingsOpen)
        {
            Pause();
            settingsPanel.SetActive(true);
            menusPanel.gameObject.SetActive(false);
            isSettingsOpen = true;
        }
        else
        {
            isSettingsOpen = false;
            Resume();
        }
        
    }

    public void openSettings()
    {
        AudioManager.instance.playOneShot(selectSFX);
        Pause();
        settingsPanel.SetActive(true);
        menusPanel.gameObject.SetActive(false);

    }
    public void Pause()
    {
        AudioManager.instance.playOneShot(selectSFX);
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseBTN.gameObject.SetActive(false);
        resumeBTN.gameObject.SetActive(true);
        menusPanel.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseBTN.gameObject.SetActive(true);
        resumeBTN.gameObject.SetActive(false);
        menusPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(false);
        endScreenPanel.SetActive(false);

        
    }

    public void openMenusOptions()
    {
        AudioManager.instance.playOneShot(selectSFX);
        settingsPanel.gameObject.SetActive(false);
        menusPanel.gameObject.SetActive(true);
    }

    public void back()
    {
        AudioManager.instance.playOneShot(selectSFX);
        settingsPanel.gameObject.SetActive(false);
        menusPanel.gameObject.SetActive(true);
    }

    public void restartGame()
    {
        AudioManager.instance.playOneShot(selectSFX);
        timer.isDone = false;
        timer.currentTime = timer.totalTime;
        Resume();
        GameManager.Instance.restaurantScore = 0;
        GameManager.Instance.newHighScore = false;
        GameManager.Instance.LoadScene(1);
    }

    public void endScreen()
    {
        Time.timeScale = 0;
        starTracker.UpdateStars();
        endScreenPanel.SetActive(true);
        AudioManager.instance.playOneShot(normalEndSFX);
        if (GameManager.Instance.newHighScore)
        {
            Debug.Log("New high score!");
            newHighScore.gameObject.SetActive(true);
            AudioManager.instance.playOneShot(newScoreSFX);
        }
        else
        {
            newHighScore.gameObject.SetActive(false);

        }
        
    }
    public void returnToMenu()
    {
        GameManager.Instance.LoadScene(0);
    }
}
