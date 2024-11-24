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
        start.onClick.AddListener(startGame);
        options.onClick.AddListener(optionsMenu);
        tutorial.onClick.AddListener(tutorialMenu);
        credits.onClick.AddListener(creditsMenu);
        back.onClick.AddListener(backToMenu);
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

    public void backToMenu()
    {
        switchPanels("MainMenu");
    }

    public void switchPanels(string clip)
    {
        Debug.Log(clip + " playing");
        anim.Play(clip);
    }

}
