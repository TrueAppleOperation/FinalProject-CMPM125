using UnityEngine;

public class LightningProjectile : MonoBehaviour
{
    public int damage = 10;
    public float speed = 12f;
    public float duration = 2f;
    private Rigidbody2D rb;

    public void Setup(Vector2 direction, float force)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            if (other.tag == "Enemy")
            {
                EnemyScript enemy = other.GetComponent<EnemyScript>();

                if (enemy != null)
                {
                    Debug.Log($"Lightning projectile hit enemy: {other.gameObject.name}");
                    enemy.takeDamage(damage);
                    enemy.Stun(2f); // Stun enemy for 2 seconds
                    Debug.Log($"Enemy stunned for 2 seconds");
                }
                Destroy(gameObject);
            
            
            } else
            {
                BossScript boss = other.GetComponent<BossScript>();

                if (boss != null)
                {
                    Debug.Log($"Lightning projectile hit boss: {other.gameObject.name}");
                    boss.takeDamage(damage);
                    boss.Stun(2f); // Stun enemy for 2 seconds
                    Debug.Log($"Boss stunned for 2 seconds");
                }
                Destroy(gameObject);

            } 

        }

    }
}
