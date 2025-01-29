using UnityEngine;
using UnityEngine.UI;

public class CookingCanvasManager : MonoBehaviour
{
    [SerializeField] private Button ingButton;
    [SerializeField] private Button foodButton;

    [SerializeField] private GameObject ingTab;
    [SerializeField] private GameObject foodTab;

    void Start()
    {
        if (ingButton != null) ingButton.onClick.AddListener(() => SwapToTab(true));
        if (foodButton != null) foodButton.onClick.AddListener(() => SwapToTab(false));
    }

    private void SwapToTab(bool openIngTab)
    {
        if (ingTab != null && foodTab != null)
        {
            ingTab.SetActive(openIngTab);
            foodTab.SetActive(!openIngTab);
        }
    }
}
