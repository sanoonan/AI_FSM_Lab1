using UnityEngine;
using System.Collections;

public class Location : MonoBehaviour
{
    public string name;
    public Tile tile;
    public Material material;

    public Location adjacentTo;


    public void AssignTile(Tile _tile)
    {
        tile = _tile;
        tile.AssignLocation(this);
    }
}
