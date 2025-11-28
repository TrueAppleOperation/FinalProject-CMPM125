using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;



public class BossFireSlam : BTNode
{
    private GameObject fireSlamObject;
    private Vector2 spawnPosition;
    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        fireSlamObject = (GameObject)data["fireStomp"];
        spawnPosition = (Vector3)data["selfPosition"];
        state = NodeState.Success;
        var fireSlam = Object.Instantiate(fireSlamObject, spawnPosition, Quaternion.identity);
        stompScript fireSlamSript = fireSlam.GetComponent<stompScript>();
        fireSlamSript.Init(spawnPosition);
        //Debug.Log("Doing fire slam!");
        return state;
    }

}

public class BossWaterRush : BTNode
{
    GameObject waterWave;
    Vector2 spawnPosition;
    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        waterWave = (GameObject)data["waterTorrentRush"];
        spawnPosition = (Vector3)data["selfPosition"];
        var waterRush = Object.Instantiate(waterWave, spawnPosition, Quaternion.identity);
        state = NodeState.Success;
        //Debug.Log("Charing Wave towards player!");
        return state;
    }

}

public class BossThunderStrike : BTNode
{
    private GameObject thunderStrike;
    private GameObject thunderStrikeWarning;
    private Vector2 spawnPosition;
    private BossScript mbReference;
    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        thunderStrike = (GameObject)data["lightningStrike"];
        thunderStrikeWarning = (GameObject)data["lightningStrikeWarning"];
        spawnPosition = (Vector3)data["playerPosition"];
        mbReference = (BossScript)data["bossMB"];
        state = NodeState.Success;
        //Debug.Log("Summoning lightning at player!");
        
        mbReference.StartCoroutine(DoThunderStrikeAfter(0.6f));
        return state;
    }

    public IEnumerator DoThunderStrikeAfter(float time)
    {
        GameObject player = GameObject.FindWithTag("Player");

        int randomTimes = UnityEngine.Random.Range(1, 3);
        for (int i = 0; i < randomTimes; i++)
        {
            spawnPosition = player.transform.position;
            var shockWarning = Object.Instantiate(thunderStrikeWarning, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(time);
            var shockAttack = Object.Instantiate(thunderStrike, spawnPosition, Quaternion.identity);
        }
    }

}


public class BossWindFollowup : BTNode
{
    private GameObject windBlast;
    private Vector2 playerPosition;
    private Vector2 spawnPosition;
    private BossScript mbReference;

    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        windBlast = (GameObject)data["windCone"];
        spawnPosition = (Vector3)data["selfPosition"];
        mbReference = (BossScript)data["bossMB"];

        mbReference.StartCoroutine(delayedFollowup(0.8f));

        state = NodeState.Success;
        //Debug.Log("Here comes a followup!");
        return state;
    }

    public IEnumerator delayedFollowup(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject bossSelf = GameObject.FindWithTag("Boss");
        GameObject player = GameObject.FindWithTag("Player");

        playerPosition = player.transform.position;
        spawnPosition = bossSelf.transform.position;
        Vector2 toTarget = playerPosition - spawnPosition;
        Vector2 direction = toTarget.normalized;

        yield return new WaitForSeconds(0.3f);
        GameObject windFollowupAttack = Object.Instantiate(windBlast, spawnPosition + direction, Quaternion.identity);
        windScript windAttackScript = windFollowupAttack.GetComponent<windScript>();
        windAttackScript.Init(spawnPosition + direction);

        windFollowupAttack.transform.rotation = Quaternion.LookRotation(direction);
        Quaternion preAdjustRotation = windFollowupAttack.transform.rotation;

        windFollowupAttack.transform.rotation = Quaternion.Euler(0, 0, windFollowupAttack.transform.eulerAngles[0] - 90);
    }

}

