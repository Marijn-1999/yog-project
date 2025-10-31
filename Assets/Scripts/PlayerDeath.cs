using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something entered the trigger: " + collision.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit the DeathZone! Restarting scene...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log($"'{collision.name}' fell into the DeathZone — destroying object.");
            Destroy(collision.gameObject);
        }
    }
}
