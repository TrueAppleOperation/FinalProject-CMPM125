using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    [Header("Button")]
    public Button MainMenuButton;

    [Header("Scene Names")]
    public string mainMenuSceneName = "MainMenu";

    private void Start()
    {
        if (MainMenuButton != null)
        {
            MainMenuButton.onClick.AddListener(OnContinueClicked);
        }
        else
        {
            Debug.LogWarning("MainMenuButton is not assigned in the Inspector!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }
    }

    private void OnContinueClicked()
    {
        SceneManager.LoadScene(mainMenuSceneName);
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

    public void QuitGamePublic() => QuitGame();
}