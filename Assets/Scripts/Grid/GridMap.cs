using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMap : MonoBehaviour
{
    public int height;
    public int length;
    public TileSet tileSet;
    int[,] grid;
    public Dictionary<Tile, TileInfo> tileInfoMap;
    public Tilemap tilemap;
    

    Tile[] dirs = new Tile[4]
    {
      new Tile(0, 1),
      new Tile(0, -1),
      new Tile(1, 0),
      new Tile(-1, 0)
    };

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
        this.tileInfoMap = LoadTileInfoMap(levelData.name);
        this.tileSet = levelData.tileSet;
    }

    public void DrawSelf()
    {
        for(int i = 0; i < length; i++) 
        {
            for(int j = 0; j < height; j++)
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
        Tile t = new Tile(x, y);
        if (CheckTile(t))
        {
            return (int)tileInfoMap[t].GetTerrain();
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

    public TileInfo GetTileData(Tile tile)
    {
        return tileInfoMap.ContainsKey(tile) ? tileInfoMap[tile] : null;
    }
    internal bool CheckWalkable(int xPos, int yPos)
    {
        return grid[xPos, yPos] == 0;
    }

    internal Dictionary<Tile, TileInfo> LoadTileInfoMap(string name)
    {
        Dictionary<Tile, TileInfo> res = new Dictionary<Tile, TileInfo>();

        string gridPath = $"{Application.dataPath}/Resources/Data/levels/grids/{name}.csv";
        string[] readGrid = File.ReadAllLines(gridPath);
        string contentsPath = $"{Application.dataPath}/Resources/Data/levels/contents/{name}.csv";
        string[] readContent = File.ReadAllLines(contentsPath);

        for(int i = height-1; i >= 0; i--)
        {
            int realRowIndex = Mathf.Abs(i - (height - 1));
            string[] gridRow = readGrid[i].Split(',');
            string[] contentRow = readContent[i].Split(',');
            for(int j = 0 ; j <= length-1; j++)
            {

                //if the contents.csv has a value in a given cell, load the prefab with that name.
                GameObject contents = null;
                if(contentRow[j] != "")
                {
                    string contentPrefabPath = $"Assets/Resources/Units/{contentRow[j]}.prefab";
                    contents = AssetDatabase.LoadAssetAtPath<GameObject>(contentPrefabPath);
                }
                res[new Tile(realRowIndex, j)] = new TileInfo(realRowIndex, j, (TerrainTypes)Convert.ToInt32(gridRow[j]), contents);
            }
        }

        return res;
    }

    #region SecondPathfinding Implementation
    public List<TileInfo> Search(TileInfo start, Func<TileInfo, TileInfo, bool> addTile)
    {
        List<TileInfo> retValue = new List<TileInfo>();
        retValue.Add(start);

        ClearSearch();
        Queue<TileInfo> checkNext = new Queue<TileInfo>();
        Queue<TileInfo> checkNow = new Queue<TileInfo>();
        start.distance = 0;
        checkNow.Enqueue(start);

        while (checkNow.Count > 0)
        {
            TileInfo t = checkNow.Dequeue();
            for(int i = 0; i < 4; i++)
            {
                TileInfo next = GetTileData(t.position + dirs[i]);
                if(next == null || next.distance <= t.distance + 1)
                {
                    continue;
                }

                if(addTile(t, next))
                {
                    next.distance = t.distance + 1;
                    next.prev = t;
                    checkNext.Enqueue(next);
                    retValue.Add(next);
                }
            }

            if(checkNow.Count == 0)
            {
                SwapReference(ref checkNow, ref checkNext);
            }

        }


        return retValue;
    }

    void SwapReference(ref Queue<TileInfo> a, ref Queue<TileInfo> b)
    {
        Queue<TileInfo> temp = a;
        a = b;
        b = temp;
    }

    void ClearSearch()
    {
        foreach(TileInfo ti in tileInfoMap.Values)
        {
            ti.prev = null;
            ti.distance = int.MaxValue;
        }
    }
    #endregion
}
