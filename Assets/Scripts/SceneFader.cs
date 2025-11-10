using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public float fadeSpeed = 1.5f;
    private bool isFading = false;

    private void Awake()
    {
        fadeGroup.alpha = 0f; // Start transparent
    }

    public void FadeOut()
    {
        if (!isFading) StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        isFading = true;
        Debug.Log("Fade starting...");

        while (fadeGroup.alpha < 1f)
        {
            fadeGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        fadeGroup.alpha = 1f;
        Debug.Log("Fade complete.");
        isFading = false;
    }
}
