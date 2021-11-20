using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMap : MonoBehaviour
{
    public int height;
    public int length;
    public TileSet tileSet;
    int[,] grid;
    public Tilemap tilemap;
    /* public void Init(int length, int height)
    {
        grid = new int[length, height];
        this.height = height;
        this.length = length;
    } */

    public void Dump()
    {
        Debug.Log($"height: {height}");
        Debug.Log($"length: {length}");
        Debug.Log($"grid: {grid}");
        for (int i=0; i < length; i++)
        {
            for(int j=0; j < height; j++)
            {
                Debug.Log(grid[i,j]);
            }
        }

    }

    public void Load(LevelData levelData)
    {
        this.grid = new int[levelData.length, levelData.height];
        this.height = levelData.height;
        this.length = levelData.length;
        this.tileSet = levelData.tileSet;
        //this.tilemap = levelData.tilemap;
        
        Debug.Log("loaded");
       //Dump();
    }

    public void DrawSelf()
    {
        for(int i = 0; i < length; i++) 
        {
            for(int j =0; j < height; j++)
            {
                UpdateTile(i, j);
            }
        }
    }
    
    public void UpdateTile(int x, int y)
    {
        int tileId = Get(x, y);
        if (tileId != -1)
        {
            tilemap.SetTile(new Vector3Int(x, y, 0), tileSet.tiles[tileId]);
        }
        
    }
    
    public void Set(int x, int y, int to) 
    {
        if (CheckTile(new Tile(x, y)))
        {
           
            grid[x,y] = to;
        } else
        {
            //Debug.LogWarning($"GridMap.cs : Postion [{x}, {y}] is out of bounds.  Grid size is [{length}, {height}]");
        }

    }

    public int Get(int x, int y) 
    {
        if (CheckTile(new Tile(x, y)))
        {
            return grid[x, y];
        }
        //Debug.LogWarning($"GridMap.cs :Postion [{x}, {y}] is out of bounds.  Grid size is [{length}, {height}]");
        return -1;
    }
    
    public bool CheckTile(Tile tile) 
    {
        //Makes sure Tile is inside the grid [length-1, height-1]
        if(tile.x < 0 || tile.x >= length || tile.y < 0 || tile.y >= height)
        {
            return false;
        }

        return true;
        
    }

    public Vector3 GetWorldLocation(Tile tile)
    {
        return tilemap.CellToWorld(new Vector3Int(tile.x, tile.y, 0));
    }

    public Tile GetTileData(Tile tile)
    {
        //eventually this should return some data we get from checking the tiles.  I think that using ruleTIles here will let me store what I want on the tile, but maybe this will be stored up
        //one level on the controller.
        return tile;
    }
    internal bool CheckWalkable(int xPos, int yPos)
    {
        return grid[xPos, yPos] == 0;
    }
}
