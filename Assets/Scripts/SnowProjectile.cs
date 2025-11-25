using UnityEngine;

public class SnowProjectile : MonoBehaviour
{
    public int freezeDuration = 5f;
    public float speed = 10f;
    public float damage = 3f;

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
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                enemy.Freeze(freezeDuration); 
            }
            Destroy(gameObject);
        }
    }

}
