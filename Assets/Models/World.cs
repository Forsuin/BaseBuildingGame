﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    Tile[,] tiles;

    public int Width { get; protected set; }
    public int Height { get; protected set; }


    public World(int width = 100, int height = 100)
    {
        Width = width;
        Height = height;

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
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
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
        if(x > Width || x < 0 || y > Height || y < 0)
        {
            Debug.LogError("Tile (" + x + ", " + y + ") is out of range");
            return null;
        }
        return tiles[x,y];
    }
}
