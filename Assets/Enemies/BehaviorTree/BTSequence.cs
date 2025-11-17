using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class BTSequence : BTNode
{
    private List<BTNode> children = new List<BTNode>();

    public BTSequence(List<BTNode> children)
    {
        this.children = children;
    }

    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        foreach (BTNode node in children)
        {
            switch (node.Evaluate(data))
            {
                case NodeState.Running:
                    state = NodeState.Running;
                    return state;
                case NodeState.Success:
                    continue;
                case NodeState.Failure:
                    state = NodeState.Failure;
                    return state;
                default:
                    state = NodeState.Success;
                    return state;
            }
        }

        state = NodeState.Success;
        return state;

    }

}
