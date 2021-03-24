using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance { get; protected set; }

    public Sprite floorSprite;
    
    public World World { get; protected set; }


    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
            Debug.LogError("There should never be two world controllers");
        }
        Instance = this;
        //Create world with empty tiles
        World = new World();

        //Create a gameobject for each tile
        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                Tile tile_data = World.GetTileAt(x, y);

                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
                tile_go.transform.SetParent(this.transform, true);

                //Add sprite renderer, but don't set sprite because all tiles are empty
                tile_go.AddComponent<SpriteRenderer>();

                //The lambda function gets the tile variable from the Action cbTileTypeChanged in Tile class
                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChange(tile, tile_go); });
            }
        }

        World.RandomizeTiles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTileTypeChange(Tile tile_data, GameObject tile_go)
    {
        if(tile_data.Type == Tile.TileType.Floor)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
        else if(tile_data.Type == Tile.TileType.Empty)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
        }
    }
}
