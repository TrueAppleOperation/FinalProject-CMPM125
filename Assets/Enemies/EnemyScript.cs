using UnityEditor.PackageManager;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement; 

public class EnemyScript : MonoBehaviour
{
    //include behvaior
    float HP;
    float SPEED;
    float maxHP;
    private Rigidbody2D rb;
    private float originalY;
    private bool isFrozen = false;
    private bool isStunned = false;

    [SerializeReference] public Sprite windSprite;
    [SerializeReference] public Sprite fireSprite;
    [SerializeReference] public Sprite lightningSprite;
    [SerializeReference] public Sprite waterSprite;

    //Sprite selfSprite = GetComponent<SpriteRenderer>();  <---- UNCOMMENT ONCE SPRITES ARE IMPLIMENTED
    enemyTypes TYPE;

    // Behavior-specific variables
    private Transform player;
    private Vector2 startPosition;
    private Vector2[] patrolPoints;
    private int currentPatrolIndex = 0;
    public bool movingForward = true;
    private bool isChasing = false;
    private Vector2 returnPosition;

    // Behavior parameters
    private float detectionRange = 2f;
    private float chaseRange = 3f;
    private float stopDistance = 1.5f;

    // Lightning enemy specific variables
    public GameObject lightningProjectilePrefab;
    public float projectileForce = 10f;
    public float attackCooldown = 2f;
    public float attackRange = 5f;

    private SpriteRenderer spriteRenderer;

    // Health bar events
    public System.Action<float> OnHealthChanged;
    public System.Action<float> OnMaxHealthChanged;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalY = transform.position.y;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        startPosition = rb.position;
        returnPosition = startPosition;
    }

    void Start()
    {
        AutoDetectEnemyType();
        OnMaxHealthChanged?.Invoke(maxHP);
        OnHealthChanged?.Invoke(HP);
    }

    void AutoDetectEnemyType()
    {
        string enemyName = gameObject.name.ToLower();

        if (enemyName.Contains("fire"))
        {
            spawnAsFire();
        }
        else if (enemyName.Contains("water"))
        {
            spawnAsWater();
        }
        else if (enemyName.Contains("lightning"))
        {
            spawnAsLightning();
        }
        else if (enemyName.Contains("wind"))
        {
            spawnAsWind();
        }
        else
        {
            Debug.LogWarning($"No enemy type detected in name '{gameObject.name}'. Defaulting to FIRE.");
            spawnAsFire();
        }
    }

    // Update is called once per frame
    void Update()
    {
        preventHPOverflow();

        // Check for player detection
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (!isChasing && distanceToPlayer <= detectionRange)
            {
                // Start chasing
                isChasing = true;
                returnPosition = transform.position;
            }
            else if (isChasing && distanceToPlayer > chaseRange)
            {
                // Stop chasing and return to patrol
                isChasing = false;
                currentPatrolIndex = FindNearestPatrolPoint();
            }
        }

        if (TYPE == enemyTypes.LIGHTNING)
        {
            LightningAttackLogic();
        }
        else if (TYPE == enemyTypes.WATER)
        {
            WaterAttackLogic();
        }

        ExecuteBehavior();
    }

    void FixedUpdate()
    {
        ClampToCamera();
    }

    void ExecuteBehavior()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            switch (TYPE)
            {
                case enemyTypes.FIRE:
                    FirePatrol();
                    break;
                case enemyTypes.WATER:
                    WaterPatrol();
                    break;
                case enemyTypes.LIGHTNING:
                    LightningPatrol();
                    break;
                case enemyTypes.WIND:
                    WindPatrol();
                    break;
            }
        }
    }

    void ChasePlayer()
    {
        if (player == null) return;
        if (isFrozen) return;
        if (isStunned) return;

        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        if (distance > stopDistance)
        {
            Vector2 dir = toPlayer.normalized;
            rb.MovePosition(rb.position + dir * SPEED * Time.fixedDeltaTime);
        }
    }

    void FirePatrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            patrolPoints = new Vector2[2] {
                startPosition + Vector2.left * 2f,
                startPosition + Vector2.right * 2f
            };
        }
        PatrolBetweenPoints(1.0f);
    }

    void WaterPatrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            patrolPoints = new Vector2[3] {
                startPosition + new Vector2(-1.5f, 0.5f),
                startPosition + new Vector2(1.5f, 0.5f),
                startPosition + new Vector2(0f, -1f)
            };
        }
        PatrolBetweenPoints(0.7f);
    }

    void LightningPatrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            chaseRange = 7f;
            detectionRange = 6f;
            patrolPoints = new Vector2[2] {
                startPosition + Vector2.left * 2.5f,
                startPosition + Vector2.right * 2.5f
            };
        }
        PatrolBetweenPoints(1.3f);
    }

    void WindPatrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            patrolPoints = new Vector2[3] {
                startPosition + new Vector2(-2f, 0f),
                startPosition,
                startPosition + new Vector2(2f, 0f)
            };
        }
        PatrolBetweenPoints(1.5f);
    }

    void PatrolBetweenPoints(float speedMultiplier)
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;
        if (isFrozen) return;
        if (isStunned) return;

        Vector2 currentTarget = patrolPoints[currentPatrolIndex];
        Vector2 toTarget = currentTarget - rb.position;

        if (toTarget.magnitude > 0.1f)
        {
            Vector2 dir = toTarget.normalized;
            rb.MovePosition(rb.position + dir * SPEED * speedMultiplier * Time.fixedDeltaTime);
        }
        else
        {
            if (patrolPoints.Length == 2)
            {
                currentPatrolIndex = (currentPatrolIndex == 0) ? 1 : 0;
            }
            else
            {
                if (movingForward)
                {
                    currentPatrolIndex++;
                    if (currentPatrolIndex >= patrolPoints.Length)
                    {
                        currentPatrolIndex = patrolPoints.Length - 2;
                        movingForward = false;
                    }
                }
                else
                {
                    currentPatrolIndex--;
                    if (currentPatrolIndex < 0)
                    {
                        currentPatrolIndex = 1;
                        movingForward = true;
                    }
                }
            }
        }
    }

    void LightningAttackLogic()
    {
        if (!isChasing) return;
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        ThunderEnemyBehavior thunderScript = GetComponent<ThunderEnemyBehavior>();
        if (thunderScript != null)
        {
            if (distanceToPlayer <= attackRange)
            {
                thunderScript.activateAttackMode();
            }
            else
            {
                thunderScript.disableAttackMode();
            }
        }
    }

    void WaterAttackLogic()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        WaterEnemyBehavior waterScript = GetComponent<WaterEnemyBehavior>();

        if (waterScript != null)
        {
            if (distanceToPlayer <= attackRange) // maybe add isChasing && 
            {
                waterScript.activateAttackMode();
            }
            else
            {
                waterScript.disableAttackMode();
            }
        }
    }

    int FindNearestPatrolPoint()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return 0;

        int nearestIndex = 0;
        float nearestDistance = Vector2.Distance(transform.position, patrolPoints[0]);

        for (int i = 1; i < patrolPoints.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, patrolPoints[i]);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestIndex = i;
            }
        }
        return nearestIndex;
    }

    void ClampToCamera()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        Vector3 camPos = cam.transform.position;
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, camPos.x - halfWidth, camPos.x + halfWidth);
        pos.y = Mathf.Clamp(pos.y, camPos.y - halfHeight, camPos.y + halfHeight);

        transform.position = pos;
    }

    public void takeDamage(float dmg)
    {
        Debug.Log($"Taking {dmg} damage. Current HP: {HP}");

        if (dmg >= HP)
        {
            HP = 0;
            OnHealthChanged?.Invoke(HP);
            Debug.Log("Enemy died! Destroying...");
            Destroy(gameObject);
        }
        else
        {
            HP -= dmg;
            OnHealthChanged?.Invoke(HP);
            Debug.Log($"New HP: {HP}");
        }
    }

    // Not set in stone
    void preventHPOverflow()
    {
        if (HP > maxHP)
        {
            HP = maxHP / 2;
        }
    }

    public IEnumerator BringBackDown(Rigidbody2D rb, float delay)
    {
        Debug.Log("Bringing enemy back down");
        yield return new WaitForSeconds(delay);
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
            if (transform.position.y > (originalY))
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
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 1;
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            StartCoroutine(BringBackDown(rb, duration));

        }
    }

    public void Freeze(float duration)
    {
        if (!isFrozen)
        {
            StartCoroutine(FreezeCoroutine(duration));
        }
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        isFrozen = true;
        yield return new WaitForSeconds(duration);
        isFrozen = false;
    }

    public void Stun(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine(duration));
        }
    }

    private IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

    //custom init functions since unity doesnt have built in functions for this
    public void spawnAsFire()
    {
        HP = 30f;
        SPEED = 2f;
        maxHP = HP;
        TYPE = enemyTypes.FIRE;
        detectionRange = 2.5f;
        chaseRange = 4f;
        //selfSprite = fireSprite;  <---- UNCOMMENT ONCE SPRITES ARE IMPLIMENTED
        //behavior = behavior yada yada
    }
    public void spawnAsWater()
    {
        HP = 40f;
        SPEED = 1.5f;
        maxHP = HP;
        TYPE = enemyTypes.WATER;
        detectionRange = 2f;
        chaseRange = 3.5f;
        attackRange = 5f; // Add this line
        //selfSprite = waterSprite;  <---- UNCOMMENT ONCE SPRITES ARE IMPLIMENTED
        //behavior = behavior yada yada
    }
    public void spawnAsLightning()
    {
        HP = 35f;
        SPEED = 2.5f;
        maxHP = HP;
        TYPE = enemyTypes.LIGHTNING;
        detectionRange = 2f;
        chaseRange = 4f;
        attackRange = 5f;
        //selfSprite = lightningSprite;  <---- UNCOMMENT ONCE SPRITES ARE IMPLIMENTED
        //behavior = behavior yada yada
    }
    public void spawnAsWind()
    {
        HP = 0.5f;
        SPEED = 3f;
        maxHP = HP;
        TYPE = enemyTypes.WIND;
        detectionRange = 2.2f;
        chaseRange = 4.5f;
        //selfSprite = windSprite;  <---- UNCOMMENT ONCE SPRITES ARE IMPLIMENTED
        //behavior = behavior yada yada
    }

    public float GetCurrentHP() => HP;
    public float GetMaxHP() => maxHP;
}