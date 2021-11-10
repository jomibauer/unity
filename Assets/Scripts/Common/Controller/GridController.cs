using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    [SerializeField] Tilemap gridTilemap;
    [SerializeField] Tilemap pathTilemap;
    [SerializeField] GridMap gridMap;
    [SerializeField] TileBase highlitTile;
    [SerializeField] Pathfinding pathfinding;

    //this is a field that stores where a unit is moving from when pathfinding.  I store it here to be able to preserve it once the unit moves so that if the player wants to press cancel, the unit
    //can reset back to its origin tile.
    Tile selectedTile;
    Tile originTile;
    Tile cursorTile;
    //[SerializeField] AnimatedTile selectedTile;

    void Start()
    {
        DrawGridMap();
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
    public void DrawPath(Tile startTile, Tile endTile)
    {
        pathTilemap.ClearAllTiles();

        List<PathNode> path = pathfinding.FindPath(startTile.x, startTile.y, endTile.x, endTile.y);

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
