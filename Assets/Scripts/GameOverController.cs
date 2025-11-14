using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("Restarting game...");

            // Reset static GameManager instance completely
            if (GameManager.Instance != null)
            {
                Destroy(GameManager.Instance.gameObject);
            }

            // Load the first scene (fresh start)
            SceneManager.LoadScene(0);
        }
    }
}
