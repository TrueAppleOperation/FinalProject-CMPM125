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
    public string nextSceneName = "StartGame";
    public string defaultSaveScene = "StartGame";

    private string savedSceneName;

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueClicked);
        newGameButton.onClick.AddListener(OnNewGameClicked);
        optionsButton.onClick.AddListener(OnOptionsClicked);
        creditsButton.onClick.AddListener(OnCreditsClicked);

        InitializeContinueButton();
        ShowMainMenu();
    }

    private void InitializeContinueButton()
    {
        // Check if a save exists and enable/disable the continue button accordingly
        bool saveExists = SaveSystem.SaveExists();
        continueButton.interactable = saveExists;

        if (saveExists)
        {
            SaveData data = SaveSystem.LoadGame();
            if (data != null)
            {
                savedSceneName = data.savedSceneName;
            }
        }
        else
        {
            savedSceneName = defaultSaveScene;
        }
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
        if (SaveSystem.SaveExists())
        {
            SaveData data = SaveSystem.LoadGame();
            if (data != null && !string.IsNullOrEmpty(data.savedSceneName))
            {
                Debug.Log($"Loading saved game from scene: {data.savedSceneName}");
                SceneManager.LoadScene(data.savedSceneName);
            }
            else
            {
                Debug.LogWarning("Save data corrupted, starting new game");
                SceneManager.LoadScene(defaultSaveScene);
            }
        }
        else
        {
            Debug.Log("No save found, starting new game");
            SceneManager.LoadScene(defaultSaveScene);
        }
    }

    private void OnNewGameClicked()
    {
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
        SaveSystem.DeleteSave();
        SceneManager.LoadScene(nextSceneName);
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