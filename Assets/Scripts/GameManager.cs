using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Lives Settings")]
    public int startingLives = 3;
    public int lives;

    [Header("Score Settings")]
    public int score = 0;

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
        UpdateScoreUI(); // Make sure score updates too
    }

    // Called when the player dies
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
            score = 0; // Reset score on full restart
            SceneManager.LoadScene(0);
        }
    }

    // Called when a collectible (like a diamond) is picked up
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
        UpdateScoreUI();
    }

    // Updates the Lives UI in the scene
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

    // Updates the Score UI in the scene
    private void UpdateScoreUI()
    {
        var scoreUI = FindFirstObjectByType<ScoreUI>();
        if (scoreUI != null)
        {
            Debug.Log("Found ScoreUI — updating now");
            scoreUI.UpdateScore();
        }
        else
        {
            Debug.LogWarning("No ScoreUI found in the scene!");
        }
    }
}
