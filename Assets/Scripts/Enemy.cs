using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    private Rigidbody2D rb;
    private float originalY;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalY = transform.position.y;
    }

    public void TakeDamage(int damage){
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }    
    }

    public IEnumerator BringBackDown(Rigidbody2D rb, float delay)
    {
        Debug.Log("Bringing enemy back down");
        yield return new WaitForSeconds(delay);
        if(rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
            if(transform.position.y > (originalY))
            {
                Vector3 pos = transform.position;
                pos.y = originalY;
                transform.position = pos;
            }
            Debug.Log("Gravity reset to 0");
        }
        
    }
    public void KnockUp(float force, float duration)
    {
        if(rb != null){
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 1;
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            StartCoroutine(BringBackDown(rb, duration));
        }
    }

    
}
