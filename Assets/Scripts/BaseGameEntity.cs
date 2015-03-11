using UnityEngine;
using System.Collections;

public class BaseGameEntity : MonoBehaviour
{
    public string name;
 

    public MessageDispatcher msgDispatcher;




    public virtual bool HandleMessage(Message msg)
    {
        return false;
    }


}
