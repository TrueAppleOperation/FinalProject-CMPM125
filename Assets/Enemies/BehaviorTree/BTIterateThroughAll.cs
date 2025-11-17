using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class BTIterateThroughAll : BTNode
{
    private List<BTNode> children = new List<BTNode>();
    private int currentChild;
    public BTIterateThroughAll(List<BTNode> children)
    {
        this.children = children;
        currentChild = 0;
    }

    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        for (int i = currentChild; i < children.Count; i++)
        {
            BTNode node = children[i];
            NodeState childState = node.Evaluate(data);

            if (childState == NodeState.Running)
            {
                currentChild = i;
                state = NodeState.Running;
                return state;
            }

        }

        currentChild = 0;
        state = NodeState.Success;
        return state;

    }

}
