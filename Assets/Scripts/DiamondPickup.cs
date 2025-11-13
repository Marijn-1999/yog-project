using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int value = 100;

    // Unique ID for this diamond (can be set manually if needed)
    private string uniqueID;

    private void Start()
    {
        // You can use position as a simple unique ID
        uniqueID = gameObject.scene.name + "_" + transform.position.ToString();

        // If the player already collected this diamond before dying, hide it
        if (GameManager.Instance.HasCollected(uniqueID))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(value);
            GameManager.Instance.MarkCollected(uniqueID);
            gameObject.SetActive(false);
        }
    }
}
