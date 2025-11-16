using UnityEngine;

[RequireComponent(typeof(EnemyScript))]
public class FireEnemyBehavior : MonoBehaviour
{
    [Header("Fire Settings")]
    public GameObject fireballPrefab;
    public float fireCooldown = 0.6f;
    public float fireballSpeed = 6f;
    public float shootRange = 8f;

    float fireTimer;
    Transform player;

    void Awake()
    {
        EnemyScript enemyScript = GetComponent<EnemyScript>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
      

        fireTimer = fireCooldown; //delay before shooting
    }

    void Update()
    {
        if (player == null || fireballPrefab == null)
        {
            return;
        }

        fireTimer -= Time.deltaTime;

        if (fireTimer > 0f)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootRange)
        {
            shootFireball();
            fireTimer = fireCooldown;
        }
    }

    void shootFireball()
    {
        Vector2 spawnPosition = transform.position;
        Vector2 toTarget = (Vector2)player.position - spawnPosition;
        Vector2 direction = toTarget.normalized;

        GameObject fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);

        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.speed = fireballSpeed;
            fireballScript.Init(direction);
        }
    }
}
