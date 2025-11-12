using UnityEngine;

public class DiamondPickup : MonoBehaviour
{
    public int points = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(points);
            Destroy(gameObject);
        }
    }
}
