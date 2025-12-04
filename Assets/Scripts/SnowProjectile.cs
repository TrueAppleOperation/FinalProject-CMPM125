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
        if ( other.CompareTag("Enemy") || other.CompareTag("Boss") )
        {
            if (other.tag == "Enemy")
            {
                EnemyScript enemy = other.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    Debug.Log($"Snow projectile hit enemy: {other.gameObject.name}");
                    enemy.takeDamage(damage);
                    enemy.Freeze(freezeDuration);
                    Debug.Log($"Enemy frozen for {freezeDuration} seconds");
                }
                Destroy(gameObject);
            }
            else
            {
                BossScript boss = other.GetComponent<BossScript>();
                if (boss != null)
                {
                    Debug.Log($"Snow projectile hit boss: {other.gameObject.name}");
                    boss.takeDamage(damage);
                    boss.Freeze(freezeDuration);
                    Debug.Log($"Boss frozen for {freezeDuration} seconds");
                }
                Destroy(gameObject);
            }


        }
    }

}
