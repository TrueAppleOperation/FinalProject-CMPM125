using UnityEngine;
using System.Collections;

public class TornadoProjectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5f;
    public int damage = 10;
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
                // Handle collision logic here
                EnemyScript enemy = other.GetComponent<EnemyScript>();

                if (enemy != null)
                {
                    Debug.Log($"Tornado projectile hit enemy: {other.gameObject.name}");
                    //Enemy takes damage logic
                    enemy.takeDamage(damage);

                    Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                    if (enemyRb != null)
                    {
                        enemy.Stun(2f); // Stun for 2 seconds
                        enemy.KnockUp(10f, 2f);
                        enemy.movingForward = false;
                        Debug.Log($"Enemy stunned and knocked up");
                    }

                }
                Destroy(gameObject);
            } else
            {
                // Handle collision logic here
                BossScript boss = other.GetComponent<BossScript>();

                if (boss != null)
                {
                    Debug.Log($"Tornado projectile hit boss: {other.gameObject.name}");
                    //Enemy takes damage logic
                    boss.takeDamage(damage);

                    Rigidbody2D enemyRb = boss.GetComponent<Rigidbody2D>();
                    if (enemyRb != null)
                    {
                        boss.Stun(2f); // Stun for 2 seconds
                        boss.KnockUp(10f, 2f);
                        Debug.Log($"Boss stunned and knocked up");
                    }

                }
                Destroy(gameObject);



            }


        }
    } 
}
