using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public Sprite floorSprite;
    World world;
    // Start is called before the first frame update
    void Start()
    {
        //Create world with empty tiles
        world = new World();

        //Create a gameobject for each tile
        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                Tile tile_data = world.GetTileAt(x, y);

                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);

                //Add sprite renderer, but don't set sprite because all tiles are empty
                tile_go.AddComponent<SpriteRenderer>();

                //The lambda function gets the tile variable from the Action cbTileTypeChanged in Tile class
                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChange(tile, tile_go); });
            }
        }

        world.RandomizeTiles();
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
