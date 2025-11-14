using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 60f; // seconds per level
    private float currentTime;
    private bool timerActive = true;

    [Header("UI Reference")]
    public TextMeshProUGUI timerText;

    private void Start()
    {
        currentTime = startTime;
        UpdateTimerUI();
    }

    private void Update()
    {
        if (!timerActive) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            timerActive = false;
            Debug.Log("Time's up!");
            GameManager.Instance.PlayerDied(); // triggers the same death logic
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = $"{Mathf.CeilToInt(currentTime):000}";

        }
        else
        {
            Debug.LogWarning("Timer Text reference missing!");
        }
    }
}
