using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public enum NodeState
{
    Running,
    Success,
    Failure
}

public abstract class BTNode
{
    protected NodeState state;
    protected object data;
    public NodeState State { 
        get { return state; } 
    }

    public abstract NodeState Evaluate(Dictionary<string, object> data);
    
}