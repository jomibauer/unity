using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    [SerializeField] Tilemap gridTilemap;
    [SerializeField] Tilemap rangeTilemap;
    [SerializeField] Tilemap pathTilemap;
    [SerializeField] GridMap gridMap;
    [SerializeField] TileBase highlitTile;
    [SerializeField] TileBase rangeTile;
    [SerializeField] Pathfinding pathfinding;
    Tile selectedTile;
    //Tile where unit was before moving.  Keep it so we can teleport back in case we want to cancel a move.
    Tile originTile;
    Tile cursorTile;


    void Start()
    {
        DrawGridMap();
    }

    internal bool CheckTileIsInRange(Tile target, Tile start, int range)
    {
        return Mathf.Abs(start.x - target.x) + Mathf.Abs(start.y - target.y) <= range;
    }
    #region one off/setups
    public void InitMapAndPathfinding(LevelData levelData)
    {
        gridMap.Load(levelData);
        DrawGridMap();
        pathfinding.Init(gridMap);
    }
    public void DrawGridMap()
    {
        for(int i = 0; i < gridMap.length; i++) 
        {
            for(int j =0; j < gridMap.height; j++)
            {
                gridMap.UpdateTile(i, j);
            }
        }
    }
    #endregion
    
    #region Utility
    public bool CheckTile(Tile tile)
    {
        return gridMap.CheckTile(tile);
    }
    public void SelectTile(Tile tile)
    {
        selectedTile = tile;
    }
    public Tile GetSelectedTile(){
        return selectedTile;
    }
    public void SetCursorTile(Tile tile)
    {
        cursorTile = tile;
    }
    public Tile GetCursorTile(){
        return cursorTile;
    }
    public Tile GetTileFromWorldPostion(Vector3 worldPosition)
    {
        Vector3Int cellPosition = gridTilemap.WorldToCell(worldPosition);
        return new Tile(cellPosition.x, cellPosition.y);
    }

    public Vector3 GetWorldPositionFromTile(Tile tile)
    {
        Vector3 worldPositon = gridTilemap.CellToWorld(new Vector3Int(tile.x, tile.y, 0));
        return worldPositon;
    }
    internal void SetOriginTile(Tile tile)
    {
        originTile = tile;
    }
    internal Tile GetOriginTile()
    {
        return originTile;
    }
    #endregion

    #region Pathfinding

    internal void DrawRange(Tile rangeStart, int moveRange)
    {
        int[] xBounds = GetAxisBounds(rangeStart.x, moveRange);
        int[] yBounds = GetAxisBounds(rangeStart.y, moveRange);

        Tile temp = new Tile();
        for (int x = xBounds[0]; x <= xBounds[1]; x++){
            for (int y = yBounds[0]; y <= yBounds[1]; y++){
                temp = new Tile(x, y);
                //when obstacles are implemented, I'll need to rethink this...  Maybe I'll need to just be pathfinding everywhere and seeing where I can get?
                if(CheckTileIsInRange(temp, rangeStart, moveRange)){
                    rangeTilemap.SetTile(new Vector3Int(temp.x, temp.y, 0), rangeTile);
                }
            }
        }

    }

    internal int[] GetAxisBounds(int rangeFrom, int moveRange)
    {
        int min = rangeFrom - moveRange >= 0 ? rangeFrom - moveRange : 0;
        int max = rangeFrom + moveRange <= (gridMap.length-1) ? rangeFrom + moveRange : (gridMap.length-1);

        return new int[]{min, max};
    }

    internal List<Tile> GetAttackRangeAndDraw(Unit unit)
    {
        //int weaponRng = unit.GetWeapon().GetRange();
        int weaponRng = 1;
        Tile rangeStart = unit.GetCurrentTile();

        int[] xBounds = GetAxisBounds(rangeStart.x, weaponRng);
        int[] yBounds = GetAxisBounds(rangeStart.y, weaponRng);
        List<Tile> range = new List<Tile>();
        Tile temp = new Tile();
        for (int x = xBounds[0]; x <= xBounds[1]; x++){
            for (int y = yBounds[0]; y <= yBounds[1]; y++){
                temp = new Tile(x, y);
                if(temp == rangeStart)
                {
                    continue;
                }
                //when obstacles are implemented, I'll need to rethink this...  Maybe I'll need to just be pathfinding everywhere and seeing where I can get?
                if(CheckTileIsInRange(temp, rangeStart, weaponRng)){
                    rangeTilemap.SetTile(new Vector3Int(temp.x, temp.y, 0), rangeTile);
                    range.Add(new Tile(temp.x, temp.y));
                }
            }
        }
        return range;
    }

    internal List<PathNode> SetNewPathAndDraw(Tile startTile, Tile endTile)
    {
        List<PathNode> path = new List<PathNode>();
        if(startTile != endTile)
        {
            path = pathfinding.FindPath(startTile.x, startTile.y, endTile.x, endTile.y);
            DrawPath(path);
        }

        return path;
    }
    public void DrawPath(List<PathNode> path)
    {
        pathTilemap.ClearAllTiles();

        if(path != null)
        {
            //Debug.Log(path.Count);
            for (int i = 0; i < path.Count; i++)
            {
                pathTilemap.SetTile(new Vector3Int(path[i].xPos, path[i].yPos, 0), highlitTile);
            }
        }
    }

    public void ClearPathTilemapAndPathfinding()
    {
        pathTilemap.ClearAllTiles();
        rangeTilemap.ClearAllTiles();
    }

    public List<PathNode> GetPath(Tile startTile, Tile endTile)
    {
        List<PathNode> path = new List<PathNode>();
        if (gridMap.CheckTile(endTile))
        {
            path = pathfinding.FindPath(startTile.x, startTile.y, endTile.x, endTile.y);
        }
        return path;
    }
    #endregion




}
