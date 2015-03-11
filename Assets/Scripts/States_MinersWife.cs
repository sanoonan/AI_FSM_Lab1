using UnityEngine;
using System.Collections;




public class MinersWifeGlobalState : State<MinersWife>
{
    public override void Enter(MinersWife agent)
    {
        
    }

    public override void Execute(MinersWife agent)
    {
        if (Random.value < 0.1f)
            agent.m_stateMachine.ChangeState(new GoToBathroom());
    }

    public override void Exit(MinersWife agent)
    {
        
    }

    public override bool OnMessage(MinersWife agent, Message msg)
    {
        switch (msg.m_msg)
        {
            case "ImHome":
                {
                    Debug.Log("Husband's home, cooking stew");

                    agent.m_stateMachine.ChangeState(new CookStew());

                    return true;
                }
            case "StewReady":
                {
                    Debug.Log("stew is ready");

                    agent.msgDispatcher.SendANewMessage(agent.name, "Miner Bob", "StewReady");

                    agent.isCooking = false;

                    agent.m_stateMachine.ChangeState(new Clean());
                    
                    return true;
                }
        }

        return false;
    }
}


public class Clean : State<MinersWife>
{
    public override void Enter(MinersWife agent)
    {
        if (!agent.CurrentLocation("Shack"))
        {
            Debug.Log(agent.name + ": going to shack");
            agent.ChangeLocation("Shack");
        }
    }

    public override void Execute(MinersWife agent)
    {
        Debug.Log(agent.name + ": cleaning");
    }

    public override void Exit(MinersWife agent)
    {

        Debug.Log(agent.name + ": leaving shack");

    }
}


public class GoToBathroom : State<MinersWife>
{
    public override void Enter(MinersWife agent)
    {
        if (!agent.CurrentLocation("Bathroom"))
        {
            Debug.Log(agent.name + ": going to bathroom");
            agent.ChangeLocation("Bathroom");
        }
    }

    public override void Execute(MinersWife agent)
    {
        Debug.Log(agent.name + ": in the bathroom");

        agent.m_stateMachine.RevertToPreviousState();
    }

    public override void Exit(MinersWife agent)
    {
        Debug.Log(agent.name + ": leaving the bathroom");
    }
}




public class CookStew : State<MinersWife>
{
    public override void Enter(MinersWife agent)
    {
        
        if (!agent.isCooking)
        {
            if (!agent.CurrentLocation("Kitchen"))
            {
                Debug.Log(agent.name + ": going to kitchen");
                agent.ChangeLocation("Kitchen");
            }

            Debug.Log(agent.name + ": cooking stew");

            agent.msgDispatcher.SendANewMessage(agent.name, agent.name, "StewReady", 10);

            agent.isCooking = true;
        }
    }

    public override void Execute(MinersWife agent)
    {
        Debug.Log(agent.name + ": waiting for stew to cook");
    }

    public override void Exit(MinersWife agent)
    {
        agent.isCooking = false;
        Debug.Log(agent.name + ": leaving kitchen");
    }
}

