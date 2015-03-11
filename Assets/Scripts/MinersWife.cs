using UnityEngine;
using System.Collections;


public class MinersWife : Agent
{
    public StateMachine<MinersWife> m_stateMachine;
    public bool isCooking = false;



    void Start()
    {
        ChangeLocation("Kitchen");

        m_stateMachine = new StateMachine<MinersWife>(this);
        m_stateMachine.SetCurrentState(new Clean());
        m_stateMachine.SetGlobalState(new MinersWifeGlobalState());

  
    }



    public override void Update()
    {
        m_stateMachine.Update();
    }

    public override bool HandleMessage(Message msg)
    {
        return m_stateMachine.HandleMessage(msg);
    }


}
