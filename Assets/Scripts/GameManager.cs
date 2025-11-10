using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Lives Settings")]
    public int startingLives = 3;
    public int lives;

    private void Awake()
    {
        // Make this persist between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            lives = startingLives;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDied()
    {
        lives--;
        Debug.Log("Player died! Lives left: " + lives);

        if (lives > 0)
        {
            // Restart current level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("Game Over! Restarting game...");
            lives = startingLives;
            SceneManager.LoadScene(0); // Load the first scene (index 0)
        }
    }
}
