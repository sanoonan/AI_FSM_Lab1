using UnityEngine;
using System.Collections;

public abstract class Agent : BaseGameEntity
{

    void Start()
    {
    }


    public Location m_location;

    public LocationManager locationManager;
  

    public abstract void Update();

    public void ChangeLocation(Location new_location)
    {
        m_location = new_location;
        Move();
    }

    public void ChangeLocation(string new_location)
    {
        if(locationManager == null)
            locationManager = LocationManager.Instance;
        Location loc = LocationManager.Instance.FindLocationByName(new_location);
        ChangeLocation(loc);
    }

    public bool CurrentLocation(string _loc)
    {
        if (locationManager == null)
            locationManager = LocationManager.Instance;

        Location loc = LocationManager.Instance.FindLocationByName(_loc);

        if (m_location == loc)
            return true;

        return false;
    }

    private void Move()
    {
        if (m_location == null)
            return;

        Vector3 locPos = m_location.tile.transform.position;
        locPos.y = 1.0f;
        gameObject.transform.position = locPos;
        
    }


    


}



