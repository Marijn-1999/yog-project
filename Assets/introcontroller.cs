using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneController : MonoBehaviour
{
    [Header("Scene Flow")]
    [SerializeField] private string nextSceneName = "intro2";   // what comes after THIS intro
    [SerializeField] private string skipToSceneName = "Level 1"; // where to go if player skips
    [SerializeField] private float autoLoadDelay = 6f;          // seconds until auto-advance

    [Header("Skip by Holding Key")]
    [SerializeField] private KeyCode skipKey = KeyCode.Space;   // key to hold to skip
    [SerializeField] private float skipHoldTime = 2f;           // how long to hold (seconds)

    private float heldTime = 0f;
    private bool isLoading = false;

    private void Start()
    {
        if (autoLoadDelay > 0f)
            StartCoroutine(AutoLoadNext());
    }

    private System.Collections.IEnumerator AutoLoadNext()
    {
        yield return new WaitForSeconds(autoLoadDelay);
        if (!isLoading)
            LoadNextScene();
    }

    private void Update()
    {
        // Hold key to skip both intro scenes
        if (Input.GetKey(skipKey))
        {
            heldTime += Time.deltaTime;
            if (heldTime >= skipHoldTime && !isLoading)
            {
                SkipToGame();
            }
        }
        else
        {
            // reset if key released or not pressed
            heldTime = 0f;
        }
    }

    // Called by timer OR "Next" button
    public void LoadNextScene()
    {
        if (isLoading) return;
        isLoading = true;
        SceneManager.LoadScene(nextSceneName);
    }

    // Called when skip is triggered (held key or skip button)
    public void SkipToGame()
    {
        if (isLoading) return;
        isLoading = true;
        SceneManager.LoadScene(skipToSceneName);
    }
}
