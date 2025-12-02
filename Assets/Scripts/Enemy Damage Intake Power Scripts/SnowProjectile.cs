using UnityEngine;

public class SnowProjectile : MonoBehaviour
{
    public int freezeDuration = 5;
    public float speed = 10f;
    public int damage = 3;

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
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            EnemyFreeze enemyFreezeScript = other.GetComponent<EnemyFreeze>();
            if (enemy != null)
            {
                enemy.takeDamage(damage);
                enemyFreezeScript.Freeze(freezeDuration); 
            }
            Destroy(gameObject);
        }
    }

}
