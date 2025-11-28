using UnityEngine;

[RequireComponent(typeof(EnemyScript))]
public class ThunderEnemyBehavior : MonoBehaviour
{
    [Header("Fire Settings")]
    public GameObject lightningPrefab;
    public GameObject lightningWarning;
    EnemyChasePlayer movementScript;
    const float lightningCooldown = 1f;
    public float lightningDelay = 0.6f;
    public float shootRange = 20f;
    private bool alreadyStriking = false;
    private bool attackModeOn = false;

    float lightningTimer;
    Transform player;
    Vector3 lightningPosition;

    void Awake()
    {
        EnemyScript enemyScript = GetComponent<EnemyScript>();
        movementScript = GetComponent<EnemyChasePlayer>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }


        lightningTimer = lightningCooldown; //delay before shooting
    }

    void Update()
    {
        if (player == null || lightningPrefab == null)
        {
            return;
        }

        if (attackModeOn) lightningTimer -= Time.deltaTime;

        if (lightningTimer > 0f)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootRange && alreadyStriking == false && attackModeOn)
        {
            warnLightning();
            alreadyStriking = true;
        }
    }

    void warnLightning()
    {
        lightningPosition = player.transform.position;
        movementScript.freezeMovement();
        GameObject lightningWarningArea = Instantiate(lightningWarning, lightningPosition, Quaternion.identity);
        Invoke("strikeLightning", lightningDelay);
    }

    public void activateAttackMode()
    {
        if (attackModeOn) return;
        alreadyStriking = false;
        attackModeOn = true;
    }

    public void disableAttackMode()
    {
        if (!attackModeOn) return;
        attackModeOn = false;
        lightningTimer = lightningCooldown;
    }

    void strikeLightning()
    {
        GameObject lightning = Instantiate(lightningPrefab, lightningPosition, Quaternion.identity);

        Lightning lightningScript = lightning.GetComponent<Lightning>();
        if (lightningScript != null)
        {
            lightningScript.Init(lightningPosition);
        }
        movementScript.setToMove();
        alreadyStriking = false;
        lightningTimer = lightningCooldown;
    }
}
