using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveButtonHUD : MonoBehaviour
{
    [Header("UI Elements")]
    public Button saveAndQuitButton;
    public GameObject saveConfirmationPanel;
    public TextMeshProUGUI saveTimeText;
    public float confirmationDisplayTime = 2f;

    private void Start()
    {
        if (saveAndQuitButton != null)
        {
            saveAndQuitButton.onClick.AddListener(OnSaveAndQuitClicked);
        }

        if (saveConfirmationPanel != null)
        {
            saveConfirmationPanel.SetActive(false);
        }
    }

    private void OnSaveAndQuitClicked()
    {
        // Save the current scene
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        SaveSystem.SaveGame(currentSceneName);

        // Show save confirmation
        ShowSaveConfirmation();
        Invoke("ReturnToMainMenu", confirmationDisplayTime);
    }

    private void ShowSaveConfirmation()
    {
        if (saveConfirmationPanel != null)
        {
            saveConfirmationPanel.SetActive(true);

            if (saveTimeText != null)
            {
                SaveData data = SaveSystem.LoadGame();
                if (data != null)
                {
                    saveTimeText.text = $"{data.saveTimestamp}";
                }
            }
        }
    }

    private void ReturnToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void QuickSave()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        SaveSystem.SaveGame(currentSceneName);
        Debug.Log("Game quick saved!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            QuickSave();
        }
    }
}