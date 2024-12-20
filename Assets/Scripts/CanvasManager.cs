using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private Button pauseBTN;
    [SerializeField] private Button resumeBTN;
    [SerializeField] private GameObject pausePanel;

    [Header("Settings")]
    [SerializeField] private Button settingsBTN;
    [SerializeField] private Button gearBTN;
    [SerializeField] private GameObject settingsPanel;
    private bool isSettingsOpen;

    [Header("Panels")]
    [SerializeField] private Button BackBTN;
    [SerializeField] private GameObject menusPanel;

    
    private void Start()
    {
        if (pauseBTN != null) pauseBTN.onClick.AddListener(() => Pause());
        if (resumeBTN != null) resumeBTN.onClick.AddListener(() => Resume());
        if (gearBTN != null) gearBTN.onClick.AddListener(() => settings());
        if (settingsBTN != null) settingsBTN.onClick.AddListener(() => openSettings());
        if (BackBTN != null) BackBTN.onClick.AddListener(() => openMenusOptions());
    }

    public void settings()
    {
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
        Pause();
        settingsPanel.SetActive(true);
        menusPanel.gameObject.SetActive(false);

    }
    public void Pause()
    {
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
    }

    public void openMenusOptions()
    {
        settingsPanel.gameObject.SetActive(false);
        menusPanel.gameObject.SetActive(true);
    }

    public void back()
    {
        settingsPanel.gameObject.SetActive(false);
        menusPanel.gameObject.SetActive(true);
    }
}
