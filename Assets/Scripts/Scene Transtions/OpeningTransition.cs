using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningTransition : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("EnemyScene");
        }
    }
}