using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneTransition : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Level1");
        }
    }
}