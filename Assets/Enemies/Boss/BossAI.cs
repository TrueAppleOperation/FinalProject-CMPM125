using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class BossAI : MonoBehaviour
{
    private GameObject PLAYER;
    private PlayerController playerScript;
    private BTNode rootNode;
    const float NORMSPEED = 0.15f;
    const float NOSPEED = 0;

    private float MOVESPEED = NORMSPEED;


    private float timeSinceLastAction;
    private float timeSincePreviousPlayerDMG;
    private BossScript bossMonoBehavior;


    const float slamDetectionRange = 2f;
    const float maxDMGTimer = 6f;

    private bool notMidAttack = true;

    private bool aiRunning = true;

    [SerializeReference] public GameObject fireStomp;
    [SerializeReference] public GameObject windCone;
    [SerializeReference] public GameObject waterTorrentRush;
    [SerializeReference] public GameObject lightningStrike;
    [SerializeReference] public GameObject lightningStrikeWarning;

    [SerializeField] private Animator spriteAnimator;

    void Start()
    {
        timeSincePreviousPlayerDMG = 0;
        timeSinceLastAction = 0;
        PLAYER = GameObject.FindWithTag("Player");
        playerScript = PLAYER.GetComponent<PlayerController>();
        bossMonoBehavior = GetComponent<BossScript>();
        setupBT();
    }

    void Update()
    {

        Dictionary<string, object> data = new Dictionary<string, object>();
        timeSinceLastAction += Time.deltaTime;
        timeSincePreviousPlayerDMG += Time.deltaTime;
        data.Add("timeSincePreviousPlayerDMG", timeSincePreviousPlayerDMG);
        data.Add("selfPosition", transform.position);
        data.Add("playerPosition", PLAYER.transform.position);
        data.Add("bossMB", bossMonoBehavior);
        data.Add("maxDMGtimer", maxDMGTimer);
        data.Add("slamDetetionRange", slamDetectionRange);

        data.Add("fireStomp", fireStomp);
        data.Add("windCone", windCone);
        data.Add("waterTorrentRush", waterTorrentRush);
        data.Add("lightningStrike", lightningStrike);
        data.Add("lightningStrikeWarning", lightningStrikeWarning);

        //Debug.Log(aiRunning);
        if (aiRunning)
        {

            moveTowardsPlayer(MOVESPEED);

            if (rootNode != null && timeSinceLastAction > 2.5 && notMidAttack)
            {
                MOVESPEED = NOSPEED;
                notMidAttack = false;
                rootNode.Evaluate(data);
                if (timeSincePreviousPlayerDMG >= 6) timeSincePreviousPlayerDMG = 0;
                spriteAnimator.SetBool("isAttackingPlayer", true);
            }
        }
        else
        {
            //Debug.Log("BOSS CANT RUN");
        }
    }

    private void moveTowardsPlayer(float SPEED)
    {
        if (PLAYER == null) return;

        Vector2 toPlayer = PLAYER.transform.position - transform.position;
        float distance = toPlayer.magnitude;

        if (distance > 1.25)
        {
            Vector2 dir = toPlayer.normalized;
            transform.position = Vector2.MoveTowards(transform.position, PLAYER.transform.position, SPEED * Time.fixedDeltaTime);
        }
    }

    private void setupBT()
    {

        var followupAttack = new BTSequence(new List<BTNode>
        {
            new doFollowupAttack(),
            new BossWindFollowup()
        });


        var fireAttackCombo = new BTIterateThroughAll(new List<BTNode>
        {
            new BossFireSlam(),
            followupAttack
        });

        var fireSlam = new BTSequence(new List<BTNode>
        {
        new isPlayerInRange(),
        fireAttackCombo
        });




        var waterAttackCombo = new BTIterateThroughAll(new List<BTNode>
        {
            new BossWaterRush(),
            followupAttack
        });

        var waterRush = new BTSequence(new List<BTNode>
        {
            new timeSincePlayerDMG(),
            waterAttackCombo
        });


        var thunderAttackCombo = new BTIterateThroughAll(new List<BTNode>
        {
            new BossThunderStrike(),
            followupAttack
        });

        var thunderStrike = new BTSequence(new List<BTNode>
        {
            new BTInverter(new isPlayerInRange()),
            thunderAttackCombo
        });

        rootNode = new BTSelector(new List<BTNode>
        {
            fireSlam,
            waterRush,
            thunderAttackCombo

        });

    }

    IEnumerator resetAttackCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
        timeSinceLastAction = 0;
        notMidAttack = true;
        MOVESPEED = NORMSPEED;
        spriteAnimator.SetBool("isAttackingPlayer", false);
    }
    public void callBossStateReset()
    {
        StartCoroutine(resetAttackCoroutine());
    }

    public void resetPlayerDMGTimer()
    {
        timeSincePreviousPlayerDMG = 0;
    }

    public void enableAI() { aiRunning = true; }
    public void disableAI() { aiRunning = false; }



}


/*
BOSS BEHAVIOR EXPLAINED
     fire: ground slam aoe if player is close to boss
     water: rides on water and charge attacks in playerDirection if player has not taken any recent damage
     thunder: calls lightning if player is too far from boss
     wind: 1/4 chance for wind cone attack at the end of a fire/water/thunder attack
 */