using UnityEngine;
using System.Collections;

public class SunProjectile : MonoBehaviour
{
    public float offsetDistance = 1.5f;
    public int damage = 1;

    private PlayerController player;
    private Vector2 lastDir = Vector2.up;
    private float damageCheckCooldown = 0.5f; // Prevent rapid repeated damage
    private float lastDamageTime = -1f; 



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

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check cooldown to prevent rapid damage ticks
        if (Time.time - lastDamageTime < damageCheckCooldown)
            return;

        if (other.CompareTag("Enemy"))
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                Debug.Log($"Sun projectile hit enemy: {other.gameObject.name}");
                enemy.takeDamage(damage - 12);
                lastDamageTime = Time.time;
            }
        }
        else if (other.CompareTag("Boss"))
        {
            BossScript boss = other.GetComponent<BossScript>();
            if (boss != null)
            {
                Debug.Log($"Sun projectile hit boss: {other.gameObject.name}");
                boss.takeDamage(damage - 12);
                lastDamageTime = Time.time;
            }
        }
    }
}

