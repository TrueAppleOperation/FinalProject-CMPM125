using UnityEngine;

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
        if (other.CompareTag("Enemy"))
        {
            // Handle collision logic here
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                //Enemy takes damage logic
                //Need to add TakeDamage method in Enemy class
                enemy.TakeDamage(damage);
                
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    enemyRb.velocity = new Vector2(enemyRb.velocity.x, 5f);
                }

            }
            Destroy(gameObject);
        }
    }
}
