using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {GameManager.Instance.score:000000}";
        }
        else
        {
            Debug.LogWarning("Score Text reference missing in ScoreUI!");
        }
    }
}
