using UnityEngine;

public class SunProjectile : MonoBehaviour
{
    public Linerenderer ray;
    public float masDistance = 12f;
    public int damagePerSecond = 3f;

    private Transform player;
    private Vectorw direction;

    public void Setup(Vector2 dir, float speed)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = dir.normalized;
        ray.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
       if(!Input.GetMouseButton(0))
       {
        Destroy(gameObject);
       }
       ShootBeam();
    }

    void ShootBeam()
    {
        Vector3 start = player.position;
        Vector3 end = start + (Vector3)(direction * masDistance);
        
        ray.SetPosition(0, start);
        ray.SetPosition(1, end);

        RaycastHit2D hit = Physics2D.Raycast(player.position, direction, masDistance);
        
        if(hit.collider != null)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
