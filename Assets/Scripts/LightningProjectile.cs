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
        EnemyScript enemy = other.GetComponent<EnemyScript>();
        
        if (other.CompareTag("Enemy"))
        {
            if (enemy != null)
            {
                //Need to add TakeDamage method in Enemy class
                enemy.takeDamage(damage);
                enemy.Stun(2f); // Stun enemy for 2 seconds
            }
            Destroy(gameObject);
        }
    }
}
