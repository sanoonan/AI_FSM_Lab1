using UnityEngine;
using System.Collections;

public class LocationManager : MonoBehaviour
{


    public Location[] locations;
    public static LocationManager Instance;

    void Awake()
    {
        Instance = this;
        AssignNames();   
    }

    private void AssignNames()
    {
        int numLocs = locations.Length;

        for (int i = 0; i < numLocs; i++)
            locations[i].name = locations[i].gameObject.name;
    }

    public Location FindLocationByName(string loc)
    {
        int numLocs = locations.Length;

        for (int i = 0; i < numLocs; i++)
            if (loc == locations[i].name)
                return locations[i];

        return null;
    }
}
