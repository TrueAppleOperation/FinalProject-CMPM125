using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChasePlayer : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;
    public float detectionRange = 5f; // How close player needs to be to start chasing

    Rigidbody2D rb;
    Transform player;
    bool isChasing = false;

    // Idle walking back and forth variables
    Vector2 startPosition;
    Vector2 leftTarget;
    Vector2 rightTarget;
    bool movingRight = true;
    public float walkDistance = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        // Set up patrol points
        startPosition = rb.position;
        leftTarget = startPosition + Vector2.left * walkDistance;
        rightTarget = startPosition + Vector2.right * walkDistance;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        // Detection logic
        if (!isChasing && distance <= detectionRange)
        {
            isChasing = true;
        }
        else if (isChasing && distance > detectionRange * 1.2f)
        {
            isChasing = false;
        }

        // Movement logic
        if (isChasing)
        {
            // Chase player
            if (distance > stopDistance)
            {
                Vector2 dir = toPlayer.normalized;
                rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            // Walk back and forth between two points
            PatrolBackAndForth();
        }

        ClampToCamera();
    }

    void PatrolBackAndForth()
    {
        Vector2 currentTarget = movingRight ? rightTarget : leftTarget;
        Vector2 toTarget = currentTarget - rb.position;

        // Move toward current target
        Vector2 dir = toTarget.normalized;
        rb.MovePosition(rb.position + dir * (moveSpeed * 0.5f) * Time.fixedDeltaTime);

        // Check if reached target
        if (toTarget.magnitude <= 0.1f)
        {
            // Switch direction
            movingRight = !movingRight;
        }
    }

    void ClampToCamera()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        Vector3 camPos = cam.transform.position;
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, camPos.x - halfWidth, camPos.x + halfWidth);
        pos.y = Mathf.Clamp(pos.y, camPos.y - halfHeight, camPos.y + halfHeight);

        transform.position = pos;
    }

    // Visualize detection range and patrol path in editor
    void OnDrawGizmosSelected()
    {
        // Detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Stop distance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);

        // Patrol path
        if (Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(leftTarget, rightTarget);
            Gizmos.DrawWireSphere(leftTarget, 0.1f);
            Gizmos.DrawWireSphere(rightTarget, 0.1f);
            Gizmos.DrawWireSphere(startPosition, 0.1f);
        }
        else
        {
            Vector2 currentPos = transform.position;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(currentPos + Vector2.left * walkDistance, currentPos + Vector2.right * walkDistance);
            Gizmos.DrawWireSphere(currentPos + Vector2.left * walkDistance, 0.2f);
            Gizmos.DrawWireSphere(currentPos + Vector2.right * walkDistance, 0.2f);
        }
    }
}
