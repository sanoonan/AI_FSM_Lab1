using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    public int length;
    public int width;

    public GameObject tilePrefab;

    private GameObject tileGroup;
    private List<Tile> tiles;

    public Terrain mountainTerrain;
    public Terrain planeTerrain;
    private Terrain defaultTerrain;

    public float mountainChance;

    public LocationManager locationManager;

    public static Map Instance;

    void Awake()
    {
        Instance = this;
   
        defaultTerrain = planeTerrain;

        tileGroup = new GameObject();
        tileGroup.transform.parent = this.transform;
        tileGroup.name = "Tiles";

        tiles = new List<Tile>();


        BuildFloorTiles();

        AssignLocations();
    }





    //build floor in tiles
    void BuildFloorTiles()
    {
        Vector3 prefabPos = tilePrefab.transform.position;
        for (int i = 0; i < length; i++)
            for (int j = 0; j < width; j++)
            {
                Vector3 tilePos = new Vector3(prefabPos.x + i - length/2, prefabPos.y, prefabPos.z + j - width/2);
                GameObject floorObject = (GameObject)Instantiate(tilePrefab, tilePos, tilePrefab.transform.rotation);
                floorObject.transform.parent = tileGroup.transform;

                Tile floor = floorObject.GetComponent<Tile>();

                float randFloat = Random.value;

                if (randFloat <= mountainChance)
                    floor.AssignTerrain(mountainTerrain);
                else
                    floor.AssignTerrain(planeTerrain);

                tiles.Add(floor);

            }
    }


    private void AssignLocations()
    {
        int numLocs = locationManager.locations.Length;

        for (int i = 0; i < numLocs; i++)
        {
            if (locationManager.locations[i].adjacentTo == null)
                locationManager.locations[i].AssignTile(AssignLocation());
            else
                locationManager.locations[i].AssignTile(AssignAdjacentLocation(locationManager.locations[i].adjacentTo));
        }
    }



    private Tile AssignLocation()
    {
        while (true)
        {
            int randInt = Random.Range(0, width * length);

            Tile tile = tiles[randInt];

            if (!tile.hasLocation)
            {
                tile.AssignTerrain(defaultTerrain);
                return tile;
            }

        }
        
    }

    private Tile AssignAdjacentLocation(Location prev)
    {
        return AssignAdjacentLocation(prev.tile);
    }



    private Tile AssignAdjacentLocation(Tile prev)
    {
        Vector3 prevPos = prev.transform.position;

        Vector3[] nsew = new Vector3[4];
        for (int i = 0; i < 4; i++)
            nsew[i] = prevPos;

        nsew[0].x++;
        nsew[1].x--;
        nsew[2].z++;
        nsew[3].z--;

        while(true)
        {
            int randInt = Random.Range(0, 4);
            Tile tile = GetTileByLocation(nsew[randInt]);

            if (tile == null)
                continue;

            if (!tile.hasLocation)
            {
                tile.AssignTerrain(defaultTerrain);
                return tile;
            }
        }
    }

    private Tile GetTileByLocation(Vector3 loc)
    {
        int numTiles = tiles.Count;
        for (int i = 0; i < numTiles; i++)
            if (tiles[i].transform.position == loc)
                return tiles[i];

        return null;
    }



    
}
