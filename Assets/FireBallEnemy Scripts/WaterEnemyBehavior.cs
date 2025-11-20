using UnityEngine;

[RequireComponent(typeof(EnemyScript))]
public class WaterEnemyBehavior : MonoBehaviour
{
    [Header("Fire Settings")]
    public GameObject waterwavePrefab;
    public float waterCooldown = 0.8f;
    public float waveSpeed = 0.15f;
    public float shootRange = 16f;

    float waveTimer;
    Transform player;

    void Awake()
    {
        EnemyScript enemyScript = GetComponent<EnemyScript>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }


        waveTimer = waterCooldown; //delay before shooting
    }

    void Update()
    {
        if (player == null || waterwavePrefab == null)
        {
            return;
        }

        waveTimer -= Time.deltaTime;

        if (waveTimer > 0f)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootRange)
        {
            shootWave();
            waveTimer = waterCooldown;
        }
    }

    void shootWave()
    {
        Vector2 spawnPosition = transform.position;
        Vector2 toTarget = (Vector2)player.position - spawnPosition;
        Vector2 direction = toTarget.normalized;

        GameObject waterwave = Instantiate(waterwavePrefab, spawnPosition, Quaternion.identity);
        waterwave.transform.rotation = Quaternion.LookRotation(direction);
        Quaternion preAdjustRotation = waterwave.transform.rotation;

        waterwave.transform.rotation = Quaternion.Euler(0, 0, waterwave.transform.eulerAngles[0]);

        Fireball waterwaveScript = waterwave.GetComponent<Fireball>();
        if (waterwaveScript != null)
        {
            waterwaveScript.speed = waveSpeed;
            waterwaveScript.Init(direction);
        }
    }
}
