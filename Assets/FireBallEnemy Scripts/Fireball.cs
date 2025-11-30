using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 6f;
    Vector2 direction = Vector2.zero;

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Fireball triggered with: {other.gameObject.name} (Tag: {other.gameObject.tag})");

        if (other.gameObject.CompareTag("Player"))
        {
            ApplyDamage(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Fireball collided with: {collision.gameObject.name} (Tag: {collision.gameObject.tag})");

        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyDamage(collision.gameObject);
        }

        //Destroy(gameObject);
    }

    private void ApplyDamage(GameObject playerObject)
    {
        Debug.Log("Applying damage to player!");

        PlayerController playerScript = playerObject.GetComponent<PlayerController>();

        if (playerScript != null)
        {
            playerScript.takeDamage(4f);
        }
        else
        {
            Debug.LogError("PlayerController component not found!");
        }
    }
}