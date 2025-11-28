using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 6f;

    Vector2 direction = Vector2.zero;

    GameObject PLAYER;

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
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player failed their DEX save!");
            PlayerController playerScript = PLAYER.GetComponent<PlayerController>();

            playerScript.takeDamage(4f);
        }
    }
}

