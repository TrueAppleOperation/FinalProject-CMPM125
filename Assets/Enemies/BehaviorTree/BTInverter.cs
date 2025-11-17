using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class BTInverter : BTNode
{
    private BTNode child;

    public BTInverter(BTNode child)
    {
        this.child = child;
    }

    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        switch (child.Evaluate(data))
        {
            case NodeState.Running:
                state = NodeState.Running;
                break;
            case NodeState.Success:
                state = NodeState.Failure; 
                break;
            case NodeState.Failure:
                state = NodeState.Success; 
                break;
        }
        return state;
    }
}


