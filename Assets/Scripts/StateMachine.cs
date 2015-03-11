using UnityEngine;
using System.Collections;

public class StateMachine <T> where T : Agent
{
    T owner;

    State<T> current_state, previous_state, global_state;

    public StateMachine(T _owner)
    {
        owner = _owner;
        current_state = previous_state = global_state = null;
    }

#region GettersSetters
    public void SetCurrentState(State<T> s)
    {
        current_state = s;
    }
    public void SetGlobalState(State<T> s)
    {
        global_state = s;
    }
    public void SetPreviousState(State<T> s)
    {
        previous_state = s;
    }
    public State<T> GetCurrentState()
    {
        return current_state;
    }
    public State<T> GetGlobalState()
    {
        return global_state;
    }
    public State<T> GetPreviousState()
    {
        return previous_state;
    }
#endregion

    public void Update()
    {
        if (global_state != null)
            global_state.Execute(owner);

        if (current_state != null)
            current_state.Execute(owner);
    }

    public void ChangeState(State<T> new_state)
    {
        if (new_state == null)
        {
            Debug.Log("<StateMachine::ChangeState>: trying to change to a null state");
            return;
        }

        previous_state = current_state;
        current_state.Exit(owner);
        current_state = new_state;
        current_state.Enter(owner);
    }

    public void RevertToPreviousState()
    {
        ChangeState(previous_state);
    }

    bool IsInState(State<T> s)
    {
        if (s == current_state)
            return true;

        return false;
    }

    public bool HandleMessage(Message msg)
    {
        //see if current state is valid and can handle message
        if((current_state != null) && (current_state.OnMessage(owner, msg)))
            return true;

        //do same for global state
        if((global_state != null) && (global_state.OnMessage(owner, msg)))
            return true;

        return false;
    }
}




