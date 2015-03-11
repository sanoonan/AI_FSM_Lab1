using UnityEngine;
using System.Collections;

public class Miner : Agent
{
    public StateMachine<Miner> m_stateMachine;
    public int m_goldCarried, m_moneyInBank, m_Thirst, m_Fatigue;
    public int m_thirstThreshold, m_goldCapacity, m_tirednessThreshold;




    void Start()
    {
        ChangeLocation("Shack");
        m_goldCarried = m_moneyInBank = m_Thirst = m_Fatigue = 0;
        m_thirstThreshold = 10;
        m_goldCapacity = 5;
        m_tirednessThreshold = 15;

        m_stateMachine = new StateMachine<Miner>(this);
        m_stateMachine.SetCurrentState(new GoHomeAndSleepTilRested());
    }




    public override void Update()
    {
        m_Thirst++;
        m_stateMachine.Update();
    }

    public StateMachine<Miner> GetFSM()
    {
        return m_stateMachine;
    }

    public bool Thirsty()
    {
        if (m_Thirst > m_thirstThreshold)
            return true;

        return false;
    }

    public bool Tired()
    {
        if (m_Fatigue > m_tirednessThreshold)
            return true;
        return false;
    }

    public bool PocketsFull()
    {
        if (m_goldCarried > m_goldCapacity)
            return true;

        return false;
    }

    public void DepositGold()
    {
        m_moneyInBank += m_goldCarried;
        m_goldCarried = 0;
    }

    public void Sleep()
    {
        m_Fatigue -= 5;

        if (m_Fatigue < 0)
            m_Fatigue = 0;
    }

    public void WithdrawGold()
    {
        int amount;
        int amount_needed;

        amount_needed = m_goldCapacity - m_goldCarried;

        if (m_moneyInBank < amount_needed)
            amount = m_moneyInBank;
        else
            amount = amount_needed;

        m_moneyInBank -= amount;
        m_goldCarried += amount;
    }

    public void Drink(int cost)
    {
        m_goldCarried -= cost;
        m_Thirst = 0;
    }

    public override bool HandleMessage(Message msg)
    {
        return m_stateMachine.HandleMessage(msg);
    }
}


