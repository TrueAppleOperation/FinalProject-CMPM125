using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChasePlayer : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f; // how close it gets before stopping

    Rigidbody2D rb;
    Transform player;

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
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        if (distance > stopDistance)
        {
            Vector2 dir = toPlayer.normalized;
            rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        }

        ClampToCamera();
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
}
