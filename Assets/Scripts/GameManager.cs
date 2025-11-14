using System.Collections.Generic;
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

    // NEW — Track collected items
    private HashSet<string> collectedItems = new HashSet<string>();

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
        UpdateLivesUI();
        UpdateScoreUI();
    }

  public void PlayerDied()
{
    lives--;

    if (lives > 0)
    {
        // Restart current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    else
    {
        // Out of lives → go to Game Over screen
        score = 0;
        collectedItems.Clear(); // reset collected diamonds if you track them

        Debug.Log("Game Over! Loading Game Over screen...");

        SceneManager.LoadScene("GameOver"); 

    }
}


    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    // 💎 Add these 3 new helper methods:
    public bool HasCollected(string id) => collectedItems.Contains(id);
    public void MarkCollected(string id) => collectedItems.Add(id);

    private void UpdateLivesUI()
    {
        var livesUI = FindFirstObjectByType<LivesUI>();
        if (livesUI != null) livesUI.UpdateLives();
    }

    private void UpdateScoreUI()
    {
        var scoreUI = FindFirstObjectByType<ScoreUI>();
        if (scoreUI != null) scoreUI.UpdateScore();
    }
}
