using UnityEngine;
using System.Collections;

public abstract class State <T>  where T : Agent
{
    public abstract void Execute(T agent);
    public abstract void Exit(T agent);
    public abstract void Enter(T agent);

    public virtual bool OnMessage(T agent, Message msg)
    {
        return false;
    }
}
