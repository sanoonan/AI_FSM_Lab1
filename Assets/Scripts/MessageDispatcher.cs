using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MessageDispatcher : MonoBehaviour
{
    public List<Message> m_messageQ = new List<Message>();
    public EntityManager Manager;

    void Update()
    {
        SendMessages();
    }



    public void SendANewMessage(string sender, string receiver, string msg, double delay)
    {
        BaseGameEntity _sender = Manager.GetEntityFromName(sender);
        BaseGameEntity _receiver = Manager.GetEntityFromName(receiver);


        double currentTime = DateTime.Now.Ticks;

        double sendTime = delay+currentTime;

        Message message = new Message(_sender, _receiver, msg, sendTime);

        m_messageQ.Add(message);
    }

    public void SendANewMessage(string sender, string receiver, string msg)
    {
        SendANewMessage(sender, receiver, msg, 0);
    }

    private void SendMessages()
    {
        double currentTime = DateTime.Now.Ticks;

        for (int i = 0; i < m_messageQ.Count; i++)
        {
            if (m_messageQ[i].m_dispatchTime <= currentTime)
            {
                SendMessage(m_messageQ[i]);
                m_messageQ.RemoveAt(i);
            }
        }
    }

    private void SendMessage(Message msg)
    {
        msg.Send();
    }


}