using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    Tile[,] tiles;
    int width;
    int height;

    public int Width
    {
        get
        {
            return width;
        }
    }

    public int Height
    {
        get
        {
            return height;
        }
    }

    public World(int width = 100, int height = 100)
    {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                    tiles[x, y] = new Tile(this, x, y);
            }
        }

        Debug.Log("World created with " + width + ", " + height + " tiles");
    }

    public void RandomizeTiles()
    {
        Debug.Log("RandomizeTiles");
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(Random.Range(0, 2) == 0)
                {
                    tiles[x, y].Type = Tile.TileType.Empty;
                }
                else
                {
                    tiles[x, y].Type = Tile.TileType.Floor;
                }
            }
        }
    }

    public Tile GetTileAt(int x, int y)
    {
        if(x > width || x < 0 || y > height || y < 0)
        {
            Debug.LogError("Tile (" + x + ", " + y + ") is out of range");
            return null;
        }
        return tiles[x,y];
    }
}
