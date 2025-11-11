using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;

    private void Start()
    {
        UpdateLives();
    }

    public void UpdateLives()
    {
        if (livesText == null)
        {
            Debug.LogWarning("Lives Text reference missing in LivesUI!");
            return;
        }

        livesText.text = "Lives: " + GameManager.Instance.lives;
        Debug.Log("UI Updated — Lives: " + GameManager.Instance.lives);
    }
}
