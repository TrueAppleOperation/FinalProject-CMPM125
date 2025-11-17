using System.Collections.Generic;
using UnityEngine;

public class BossFireSlam : BTNode
{
    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        state = NodeState.Success;
        Debug.Log("Doing fire slam!");
        return state;
    }

}

public class BossWaterRush : BTNode
{

    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        state = NodeState.Success;
        Debug.Log("Charing Wave towards player!");
        return state;
    }

}

public class BossThunderStrike : BTNode
{

    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        state = NodeState.Success;
        Debug.Log("Summoning lightning at player!");
        return state;
    }

}


public class BossWindFollowup : BTNode
{

    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        state = NodeState.Success;
        Debug.Log("Here comes a followup!");
        return state;
    }

}

