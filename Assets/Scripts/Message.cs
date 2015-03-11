using UnityEngine;
using System.Collections;

public class Message
{
    public BaseGameEntity m_sender, m_receiver;
    public string m_msg;

    public double m_dispatchTime;


    public Message()
    {
    }

    public Message(BaseGameEntity sender, BaseGameEntity receiver, string msg, double time)
    {
        m_sender = sender;
        m_receiver = receiver;
        m_msg = msg;
        m_dispatchTime = time;
    }

    public void Send()
    {
        m_receiver.HandleMessage(this);
    }
} 