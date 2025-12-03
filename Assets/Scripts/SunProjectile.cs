using UnityEngine;

public class SunProjectile : MonoBehaviour
{
    public LineRenderer ray;
    public float masDistance = 12f;        
    public float damagePerSecond = 3f;     
    public float lifeTime = 0.5f;          

    private Transform player;
    private Vector2 direction;             
    private float lifeTimer;

    public void Setup(Vector2 dir, float speed)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null || ray == null)
        {
            Destroy(gameObject);
            return;
        }

        player = playerObj.transform;
        direction = dir.normalized;

        ray.enabled = true;
        ray.positionCount = 2;

        lifeTimer = lifeTime;
    }

    void Update()
    {
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
        if (player == null || ray == null) return;

        Vector3 start = player.position;
        Vector3 end   = start + (Vector3)(direction * masDistance);

        ray.SetPosition(0, start);
        ray.SetPosition(1, end);

        Debug.DrawLine(start, end, Color.yellow, 0.05f);

        RaycastHit2D hit = Physics2D.Raycast(start, direction, masDistance);

        if (hit.collider != null)
        {
            EnemyScript enemy = hit.collider.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.takeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
