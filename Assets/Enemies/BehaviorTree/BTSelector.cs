using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BTSelector : BTNode
{
    private List<BTNode> children = new List<BTNode>();

    public BTSelector(List<BTNode> children)
    {
        this.children = children;
    }

    public override NodeState Evaluate(Dictionary<string, object> data)
    {
        this.data = data;
        int childIndex = 0;
        //Debug.Log(children.Count);
        foreach (BTNode node in children)
        {
            //Debug.Log($"Selector: Evaluating Child #{childIndex}");
            NodeState evaluation = node.Evaluate(data);
            //Debug.Log($"Selector: Child #{childIndex} returned {evaluation}");
            switch (evaluation)
            {
                case NodeState.Running:
                    state = NodeState.Running;
                    return state;
                case NodeState.Success:
                    state = NodeState.Success; 
                    return state;
                case NodeState.Failure:
                    childIndex++;
                    continue; 
                default:
                    continue;
            }
        }
        //Debug.Log("Selector: All children failed. Returning Failure.");
        state = NodeState.Failure;
        return state;

    }

}
