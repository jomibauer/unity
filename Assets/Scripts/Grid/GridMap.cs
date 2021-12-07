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

    GridCursor gridCursor;
    

    Tile[] dirs = new Tile[4]
    {
      new Tile(0, 1),
      new Tile(0, -1),
      new Tile(1, 0),
      new Tile(-1, 0)
    };

    void Start()
    {
        gridCursor = FindObjectOfType<GridCursor>();
    }

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

    internal float CalcPathScore(List<PathNode> path, MovementTypes movementType)
    {
        float pathScore = 0;
        foreach(PathNode p in path)
        {
            pathScore += MovementCostBook.Lookup[movementType][tileInfoMap[new Tile(p.xPos, p.yPos)].GetTerrain()];
        }

        return pathScore;
    }
    internal float CalcPathScore(PathNode pathNode, MovementTypes movementType)
    {
        return MovementCostBook.Lookup[movementType][tileInfoMap[new Tile(pathNode.xPos, pathNode.yPos)].GetTerrain()];;
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
            //we use real row index because we're reading the csv rows backwards into our grid.  So the bottom left corner of the csv -- which is something like row 10, col 0 --
            // becomes 0,0 on our actual grid
            int realRowIndex = Mathf.Abs(i - (height - 1));
            string[] gridRow = readGrid[i].Split(',');
            string[] contentRow = readContent[i].Split(',');

            for(int j = 0 ; j <= length-1; j++)
            {

                //if the contents.csv has a value in a given cell, load the prefab with that value as a name.
                GameObject contents = null;
                if(contentRow[j] != "")
                {
                    string contentPrefabPath = $"Assets/Resources/Units/{contentRow[j]}.prefab";
                    contents = AssetDatabase.LoadAssetAtPath<GameObject>(contentPrefabPath);
                }
                
                res[new Tile(j, realRowIndex)] = new TileInfo(j, realRowIndex, (TerrainTypes)Convert.ToInt32(gridRow[j]), contents);
            }
        }

        return res;
    }

    #region Second Pathfinding Implementation
    public List<TileInfo> Search(Unit unit, Func<TileInfo, TileInfo, Unit, bool> addTile)
    {
        TileInfo start = GetTileData(unit.GetCurrentTile());
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
                float movementCost = MovementCostBook.Lookup[unit.GetMovementType()][t.GetTerrain()];
                if(next == null || next.distance <= t.distance + movementCost)
                {
                    continue;
                }

                if(addTile(t, next, unit))
                {
                    next.distance = t.distance + movementCost;
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

    public void UpdateUnitLocation(Tile oldLocation, Unit unit)
    {
     
        tileInfoMap[oldLocation].SetUnit(null);
        SetUnitLocation(unit);
    
    }

    public void SetUnitLocation(Unit unit)
    {
        tileInfoMap[unit.GetCurrentTile()].SetUnit(unit);
        //Debug.Log($"{unit} at {unit.GetCurrentTile()}");
    }
}
