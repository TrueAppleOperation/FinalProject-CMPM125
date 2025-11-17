using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;


public class BossAI : MonoBehaviour
{
    private GameObject PLAYER;
    private PlayerController playerScript;
    private BTNode rootNode;
    private float timeSinceLastAction;
    private float timeSincePreviousPlayerDMG;


    const float slamDetectionRange = 2f;
    const float maxDMGTimer = 6f;
    void Start()
    {
        timeSincePreviousPlayerDMG = 0;
        timeSinceLastAction = 0;
        PLAYER = GameObject.FindWithTag("Player");
        playerScript = PLAYER.GetComponent<PlayerController>();
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
        data.Add("maxDMGtimer", maxDMGTimer);
        data.Add("slamDetetionRange", slamDetectionRange);



        if (rootNode != null && timeSinceLastAction > 2)
        {
            rootNode.Evaluate(data);
            timeSinceLastAction = 0;
            if (timeSincePreviousPlayerDMG >= 6) timeSincePreviousPlayerDMG = 0;
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
}


/*
BOSS BEHAVIOR EXPLAINED
     fire: ground slam aoe if player is close to boss
     water: rides on water and charge attacks in playerDirection if player has not taken any recent damage
     thunder: calls lightning if player is too far from boss
     wind: 1/4 chance for wind cone attack at the end of a fire/water/thunder attack
 */