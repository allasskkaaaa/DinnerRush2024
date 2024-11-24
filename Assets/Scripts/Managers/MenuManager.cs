using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Button start;
    [SerializeField] private Button options;
    [SerializeField] private Button tutorial;
    [SerializeField] private Button credits;
    [SerializeField] private Button back;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (start != null)
        {
            start.onClick.AddListener(startGame);
        }

        if (options != null)
        {
            options.onClick.AddListener(optionsMenu);
        }

        if (tutorial != null)
        {
            tutorial.onClick.AddListener(tutorialMenu);
        }

        if (credits != null)
        {
            credits.onClick.AddListener(creditsMenu);
        }

        if (back != null)
        {
            back.onClick.AddListener(backToMenu);
        }
        
        
        
        
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void optionsMenu()
    {
        switchPanels("OptionsMenu");
    }

    public void tutorialMenu()
    {
        switchPanels("TutorialMenu");
    }

    public void creditsMenu()
    {
        switchPanels("CreditsMenu");
    }

    public void mainMenu()
    {
        switchPanels("MainMenu");
    }
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void switchPanels(string clip)
    {
        Debug.Log(clip + " playing");
        anim.Play(clip);
    }

}
