using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public enum TileType { Empty, Floor };

    TileType type = TileType.Empty;

    Action<Tile> cbTileTypeChanged;

    LooseObject looseObject;
    InstalledObject InstalledObject;

    World world;

    int x, y;

    public int X
    {
        get
        {
            return x;
        }
    }
    public int Y
    {
        get
        {
            return y;
        }
    }
    public TileType Type
    {
        get
        {
            return type;
        }
        set
        {
            TileType oldType = type;
            type = value;
            //Call OnTileTypeChanged only when the tile type is changed to something new
            if (cbTileTypeChanged != null && oldType != type)
            {
                cbTileTypeChanged(this);
            }
        }
    }


    public Tile(World world, int x, int y)
    {
        this.world = world;
        this.x = x;
        this.y = y;
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged += callback;
    }

    public void UnregisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged -= callback;
    }
}
