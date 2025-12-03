using UnityEngine;

public class SunProjectile : MonoBehaviour
{
    public float offsetDistance = 1.5f;

    private PlayerController player;
    private Vector2 lastDir = Vector2.up; 
    public void Setup(Vector2 dir, float speed)
    {

        player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("SunProjectile: no PlayerController found!");
            Destroy(gameObject);
            return;
        }


        if (dir.sqrMagnitude > 0.01f)
            lastDir = dir.normalized;
    }

    void Update()
    {
        if (player == null) return;

        // Destroy when mouse is released
        if (!Input.GetMouseButton(0))
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = player.LastMoveDir;

        if (dir.sqrMagnitude < 0.01f)
            dir = lastDir;
        else
            lastDir = dir.normalized;


        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            dir = new Vector2(Mathf.Sign(dir.x), 0f);
        else
            dir = new Vector2(0f, Mathf.Sign(dir.y));

        Vector3 offset = (Vector3)dir * offsetDistance;
        transform.position = player.transform.position + offset;
    }
}

