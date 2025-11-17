using UnityEngine;
using System.Collections.Generic;

public class timeSincePlayerDMG : BTNode
{
    private float dmgTimer;
    private float maxAllowedTime;
 
    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        dmgTimer = (float)data["timeSincePreviousPlayerDMG"];
        maxAllowedTime = (float)data["maxDMGtimer"];
        if (dmgTimer >= maxAllowedTime)
        {
            state = NodeState.Success;
            return state;
        }

        //Debug.Log("Player took recent enough damage");
        state = NodeState.Failure;
        return state;
    }

}


public class doFollowupAttack : BTNode
{
    
    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        float random = Random.Range(1, 5);//rand 1-4

        if (random == 4)
        {
            state = NodeState.Success;
            return state;
        }

        Debug.Log("Failed follup attack opportunity");
        state = NodeState.Failure;
        return state;
    }

}

public class isPlayerInRange : BTNode
{
    private float range;
    private Vector2 playerPosition;
    private Vector2 selfPosition;

    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        Vector2 playerPosition = (Vector3)data["playerPosition"];
        Vector2 selfPosition = (Vector3)data["selfPosition"];
        float range = (float)data["slamDetetionRange"];
        

        if (playerPosition == null)
        {
            state = NodeState.Failure;
            return state;
        }

        float distance = Vector2.Distance(playerPosition, selfPosition);

        if (distance < range)
        {
            state = NodeState.Success;
            return state;
        }

        //Debug.Log("Player is out of range" + state);
        state = NodeState.Failure;
        return state;
    }

}

