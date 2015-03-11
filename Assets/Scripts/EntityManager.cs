using UnityEngine;
using System.Collections;

public class EntityManager : MonoBehaviour
{

    public BaseGameEntity[] m_entities;


    void Start()
    {
        m_entities[0].name = "Miner Bob";
        m_entities[1].name = "MinersWife Elsa";
    }

    public BaseGameEntity GetEntityFromName(string name)
    {
        int numEntities = m_entities.Length;

        for (int i = 0; i < numEntities; i++)
            if (m_entities[i].name == name)
                return m_entities[i];

        return null;
    }


    

}