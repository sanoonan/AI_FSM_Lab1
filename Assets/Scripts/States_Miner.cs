using UnityEngine;
using System.Collections;

public class EnterMinerAndDigForNugget : State<Miner>
{
    public override void Enter(Miner agent)
    {
        if (agent.m_location.name != "Mine")
        {
            Debug.Log(agent.name + ": walking to gold mine");
            agent.ChangeLocation("Mine");
        }
    }

    public override void Execute(Miner agent)
    {
        agent.m_goldCarried++;
        agent.m_Fatigue++;
        agent.m_Thirst++;

        Debug.Log(agent.name + ": mining gold");

        if (agent.PocketsFull())
            agent.m_stateMachine.ChangeState(new VisitBankAndDepositGold());


        if (agent.Tired())
            agent.m_stateMachine.ChangeState(new GoHomeAndSleepTilRested());

        if (agent.Thirsty())
            agent.m_stateMachine.ChangeState(new QuenchThirst());
    }

    public override void Exit(Miner agent)
    {
        Debug.Log(agent.name + ": leaving gold mine");
    }

    
}

public class VisitBankAndDepositGold : State<Miner>
{
    public override void Enter(Miner agent)
    {
        if (agent.m_location.name != "Bank")
        {
            Debug.Log(agent.name + ": walking to bank");
            agent.ChangeLocation("Bank");
        }
    }

    public override void Execute(Miner agent)
    {
        agent.m_Thirst++;
        agent.m_Fatigue++;
        agent.DepositGold();

        Debug.Log(agent.name + ": depositing gold");

        agent.m_stateMachine.ChangeState(new EnterMinerAndDigForNugget());
    }

    public override void Exit(Miner agent)
    {
        Debug.Log(agent.name + ": leaving bank");
    }
}

public class GoHomeAndSleepTilRested : State<Miner>
{
    public override void Enter(Miner agent)
    {
        if (!agent.CurrentLocation("Shack"))
        {
            Debug.Log(agent.name + ": walking to shack");
            agent.ChangeLocation("Shack");

            //tell wife im home
            agent.msgDispatcher.SendANewMessage(agent.name, "MinersWife Elsa", "ImHome"); 
        }
    }

    public override void Execute(Miner agent)
    {

        agent.Sleep();


        Debug.Log(agent.name + ": sleeping");

        if (agent.m_Fatigue <= 0)
            agent.m_stateMachine.ChangeState(new EnterMinerAndDigForNugget());


    }

    public override void Exit(Miner agent)
    {
        Debug.Log(agent.name + ": leaving shack");
    }

    public override bool OnMessage(Miner agent, Message msg)
    {
        switch (msg.m_msg)
        {
            case "StewReady":
                {
                    Debug.Log("Going to eat stew");

                    agent.m_stateMachine.ChangeState(new EatStew());

                    return true;
                }
        }

        return false;
    }

    
}


public class EatStew : State<Miner>
{
    public override void Enter(Miner agent)
    {
        if (!agent.CurrentLocation("Kitchen"))
        {
            Debug.Log(agent.name + ": going to kitchen");
            agent.ChangeLocation("Kitchen");
        }
    }

    public override void Execute(Miner agent)
    {
        Debug.Log(agent.name + ": eating stew");

        agent.m_stateMachine.ChangeState(new GoHomeAndSleepTilRested());
    }

    public override void Exit(Miner agent)
    {
        Debug.Log(agent.name + ": leaving kitchen");
    }
}


public class QuenchThirst : State<Miner>
{
    public override void Enter(Miner agent)
    {
        if (!agent.CurrentLocation("Pub"))
        {
            Debug.Log(agent.name + ": walking to pub");
            agent.ChangeLocation("Pub");
        }
    }

    public override void Execute(Miner agent)
    {
        int cost = 5;
        agent.m_Fatigue++;

        if (agent.m_goldCarried < cost)
            agent.m_stateMachine.ChangeState(new GetMoneyFromBank());
        else
            agent.Drink(cost);


        Debug.Log(agent.name + ": drinking");

        if (agent.m_Thirst <= 0)
            agent.m_stateMachine.ChangeState(new EnterMinerAndDigForNugget());


    }

    public override void Exit(Miner agent)
    {
        Debug.Log(agent.name + ": leaving pub");
    }
}


public class GetMoneyFromBank : State<Miner>
{
    public override void Enter(Miner agent)
    {
        if (!agent.CurrentLocation("Bank"))
        {
            Debug.Log(agent.name + ": walking to bank");
            agent.ChangeLocation("Bank");
        }
    }

    public override void Execute(Miner agent)
    {
        agent.m_Thirst++;
        agent.m_Fatigue++;
        agent.WithdrawGold();

        Debug.Log(agent.name + ": withdrawing gold");

        agent.m_stateMachine.ChangeState(new QuenchThirst());
    }

    public override void Exit(Miner agent)
    {
        Debug.Log(agent.name + ": leaving bank");
    }
}
