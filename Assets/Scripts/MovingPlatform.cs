using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Transform target;

    void Start()
    {
        target = pointB;
        Debug.Log($"[MovingPlatform] Starting movement toward {target.name}");
    }

    void Update()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogWarning("[MovingPlatform] Missing pointA or pointB reference!");
            return;
        }

        // Move towards the current target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Check if we reached the target (use distance threshold)
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 0.01f)
        {
            Debug.Log($"[MovingPlatform] Reached {target.name}");

            // Switch target
            target = target == pointA ? pointB : pointA;
            Debug.Log($"[MovingPlatform] Now moving toward {target.name}");
        }
    }

    // Draw gizmos in the Scene view to visualize movement path
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
