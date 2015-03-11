using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public GameObject icon;

    private Terrain terrain;

    public bool hasLocation;

    public Material locMat;

    void Start()
    {
        hasLocation = false;
    }

    public void AssignTerrain(Terrain _terr)
    {
        if (terrain != _terr)
        {
            terrain = _terr;
            AssignTerrainIcon();
        }
    }

    private void AssignTerrainIcon()
    {
        AssignIcon(terrain.material);
    }

    private void AssignIcon(Material mat)
    {
        MeshRenderer iconRenderer = icon.GetComponent<MeshRenderer>();
        iconRenderer.material = mat;
    }

    public void AssignLocation(Location loc)
    {
        hasLocation = true;
        AssignBackgroundMat(locMat);
        AssignIcon(loc.material);
    }

    private void AssignBackgroundMat(Material mat)
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = mat;
    }

}
