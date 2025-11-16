using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Pages")]
    public GameObject mainMenuPage;
    public GameObject optionsPage;
    public GameObject creditsPage;

    [Header("Buttons")]
    public Button continueButton;
    public Button newGameButton;
    public Button optionsButton;
    public Button creditsButton;

    [Header("Scene Names")]
    public string openingSceneName = "OpeningScene";

    private void Start()
    {
        // Set up button listeners
        continueButton.onClick.AddListener(OnContinueClicked);
        newGameButton.onClick.AddListener(OnNewGameClicked);
        optionsButton.onClick.AddListener(OnOptionsClicked);
        creditsButton.onClick.AddListener(OnCreditsClicked);

        // Disable continue button since we have no save system yet
        continueButton.interactable = false;

        // Show main menu, hide other pages
        ShowMainMenu();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            // If we're in options or credits, go back to main menu
            if (optionsPage.activeSelf || creditsPage.activeSelf)
            {
                ShowMainMenu();
            }
        }
    }

    private void OnContinueClicked()
    {
        // TODO:
        // SceneManager.LoadScene(savedSceneName);
        // LoadGameData();
    }

    private void OnNewGameClicked()
    {
        // This will erase previous game data and start fresh
        StartNewGame();
    }

    private void OnOptionsClicked()
    {
        ShowOptions();
    }

    private void OnCreditsClicked()
    {
        ShowCredits();
    }

    private void ShowMainMenu()
    {
        mainMenuPage.SetActive(true);
        optionsPage.SetActive(false);
        creditsPage.SetActive(false);
    }

    private void ShowOptions()
    {
        mainMenuPage.SetActive(false);
        optionsPage.SetActive(true);
        creditsPage.SetActive(false);
    }

    private void ShowCredits()
    {
        mainMenuPage.SetActive(false);
        optionsPage.SetActive(false);
        creditsPage.SetActive(true);
    }

    private void StartNewGame()
    {
        // TODO:
        // 1. Delete any existing save data
        // 2. Reset game state
        // 3. Load the opening scene

        // For now, just load the opening scene

        Debug.Log("Starting new game -> opening scene");

        SceneManager.LoadScene(openingSceneName);
    }

    private void QuitGame()
    {
        Debug.Log("Quitting game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ShowMainMenuPublic() => ShowMainMenu();
    public void ShowOptionsPublic() => ShowOptions();
    public void ShowCreditsPublic() => ShowCredits();
    public void StartNewGamePublic() => StartNewGame();
    public void QuitGamePublic() => QuitGame();
}