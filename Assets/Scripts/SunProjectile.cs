using UnityEngine;

public class SunProjectile : MonoBehaviour
{
    public LineRenderer ray;
    public float masDistance = 1f;
    public float damagePerSecond = 3f;

    private Transform player;
    
    public void Setup(Vector2 direction, float speed)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 direction = (mousePos - player.position).normalized;
        Vector3 start = player.position;
        Vector3 end = start + (Vector3)(direction * masDistance);
        
        ray.SetPosition(0, start);
        ray.SetPosition(1, end);

        RaycastHit2D hit = Physics2D.Raycast(player.position, direction, masDistance);
        
        if(hit.collider != null)
        {
            EnemyScript enemy = hit.collider.GetComponent<EnemyScript>();
            if(enemy != null)
            {
                enemy.takeDamage((int)(damagePerSecond * Time.deltaTime));
            }
        }
    }
}
