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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            lives = startingLives;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        UpdateLivesUI();
    }

    public void PlayerDied()
    {
        lives--;
        Debug.Log("Player died! Lives left: " + lives);

        if (lives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("Game Over! Restarting game...");
            lives = startingLives;
            SceneManager.LoadScene(0);
        }
    }

    private void UpdateLivesUI()
    {
        var livesUI = FindFirstObjectByType<LivesUI>();
        if (livesUI != null)
        {
            Debug.Log("Found LivesUI — updating now");
            livesUI.UpdateLives();
        }
        else
        {
            Debug.LogWarning("No LivesUI found in the scene!");
        }
    }
}
