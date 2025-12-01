using UnityEngine;

[RequireComponent(typeof(EnemyScript))]
public class WaterEnemyBehavior : MonoBehaviour
{
    [Header("Water Settings")]
    public GameObject waterwavePrefab;
    public float waterCooldown = 0.8f;
    public float waveSpeed = 0.10f;
    private bool attackModeOn = false;

    float waveTimer;
    Transform player;

    void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        waveTimer = waterCooldown; // delay before shooting
    }

    void Update()
    {
        if (player == null || waterwavePrefab == null)
        {
            return;
        }

        waveTimer -= Time.deltaTime;

        if (waveTimer <= 0f && attackModeOn)
        {
            shootWave();
            waveTimer = waterCooldown;
        }
    }

    public void activateAttackMode()
    {
        if (attackModeOn) return;
        attackModeOn = true;
    }

    public void disableAttackMode()
    {
        if (!attackModeOn) return;
        attackModeOn = false;
    }

    void shootWave()
    {
        Vector2 spawnPosition = transform.position;
        Vector2 toTarget = (Vector2)player.position - spawnPosition;
        Vector2 direction = toTarget.normalized;

        GameObject waterwave = Instantiate(waterwavePrefab, spawnPosition, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        waterwave.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Water waterwaveScript = waterwave.GetComponent<Water>();
        if (waterwaveScript != null)
        {
            waterwaveScript.speed = waveSpeed;
            waterwaveScript.Init(direction);
        }
    }
}