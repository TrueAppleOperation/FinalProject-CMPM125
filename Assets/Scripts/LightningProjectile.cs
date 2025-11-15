using UnityEngine;

public class LightningProjectile : MonoBehaviour
{
    public int damage = 10;
    public float speed = 12f;
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
                //Need to add TakeDamage method in Enemy class
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
