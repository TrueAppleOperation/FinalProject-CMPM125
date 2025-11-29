using UnityEngine;

public class SunProjectile : MonoBehaviour
{
    public LineRenderer ray;
    public float masDistance = 12f;
    public float damagePerSecond = 3f;

    private Transform player;
    private Vector2 direction;
    public float lifeTime = 0.5f;
    float lifeTimer;
    
    public void Setup(Vector2 dir, float speed)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        direction = dir.normalized;

        ray.enabled = true;
        ray.positionCount = 2;
    }
   
    void Awake()
    {  

    lifeTimer = lifeTime;

    }


    // Update is called once per frame
    void Update()
    {
      // if(!Input.GetMouseButton(0))
       //{
       // Destroy(gameObject);
       //}

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
            return;
        }

       ShootBeam();
    }

    void ShootBeam()
    {
        if(player == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 direction = (mousePos - player.position).normalized;
        Vector3 start = player.position;
        Vector3 end = start + (Vector3)(direction * masDistance);
        
        ray.SetPosition(0, start);
        ray.SetPosition(1, end);

        Debug.DrawLine(start, end, Color.yellow, 0.05f);

        RaycastHit2D hit = Physics2D.Raycast(player.position, direction, masDistance);
      
        
        if(hit.collider != null)
        {
            EnemyScript enemy = hit.collider.GetComponent<EnemyScript>();
            if(enemy != null)
            {
                enemy.takeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
