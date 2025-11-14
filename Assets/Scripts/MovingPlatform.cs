using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Points")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Transform target;
    private Vector3 lastPosition;
    private Vector3 platformVelocity;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogWarning("[MovingPlatform] Missing pointA or pointB reference!");
            enabled = false;
            return;
        }

        transform.position = pointA.position; // Start at point A
        target = pointB;
        lastPosition = transform.position;

        Debug.Log($"[MovingPlatform] Starting at {pointA.name}, moving toward {target.name}");
    }

    void Update()
    {
        if (pointA == null || pointB == null)
            return;

        // Move towards the current target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Check if we reached the target
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < 0.01f)
        {
            Debug.Log($"[MovingPlatform] Reached {target.name}");
            target = target == pointA ? pointB : pointA;
            Debug.Log($"[MovingPlatform] Now moving toward {target.name}");
        }
    }

    void LateUpdate()
    {
        // Calculate how fast and in what direction the platform moved this frame
        platformVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply platform velocity so the player "rides" smoothly
                rb.position += (Vector2)(platformVelocity * Time.deltaTime);
            }
        }
    }

    // Visualize movement path in Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (pointA != null)
            Gizmos.DrawSphere(pointA.position, 0.1f);
        if (pointB != null)
            Gizmos.DrawSphere(pointB.position, 0.1f);

        if (pointA != null && pointB != null)
            Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
