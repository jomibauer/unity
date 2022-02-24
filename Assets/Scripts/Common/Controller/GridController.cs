using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    [SerializeField] Tilemap gridTilemap;
    [SerializeField] Tilemap rangeTilemap;
    [SerializeField] Tilemap pathTilemap;
    [SerializeField] GridMap gridMap;
    [SerializeField] TileBase pathTile;
    [SerializeField] TileBase atkRangeTile;
    [SerializeField] TileBase rangeTile;
    [SerializeField] Pathfinding pathfinding;
    [SerializeField] Movement movement;
    List<TileInfo> range;
    Tile selectedTile;
    //Tile where unit was before moving.  Keep it so we can teleport back in case we want to cancel a move.
    Tile originTile;
    Tile cursorTile;

    void Start()
    {
        DrawGridMap();
    }

    internal bool CheckTileIsInRange(Tile target, Tile start, int[] range)
    {
        if(range.Length == 1)
        {
            return Mathf.Abs(start.x - target.x) + Mathf.Abs(start.y - target.y) == range[0];
        }
        else
        {
            return Mathf.Abs(start.x - target.x) + Mathf.Abs(start.y - target.y) <= range[range.Length-1];
        }
    }

    internal float GetPathScore(List<PathNode> path, MovementTypes movementType)
    {
        return gridMap.CalcPathScore(path, movementType);
    }

    internal float GetPathScore(PathNode path, MovementTypes movementType)
    {
        return gridMap.CalcPathScore(path, movementType);
    }

    public TileInfo GetTileInfo(Tile tile)
    {
        return gridMap.tileInfoMap[tile];
    }

    internal bool CheckTileIsInRange_search(Tile target)
    {
        foreach(TileInfo tile in range)
        {
            if(target == tile.position) { return true; }
        }
        return false;
    }
    #region one off/setups
    public void InitMapAndPathfinding(Level level)
    {
        gridMap.Load(level);
        DrawGridMap();
        pathfinding.Init(gridMap);
    }

    public void InitUnitMap(List<Unit> units)
    {
        foreach(Unit u in units)
        {
            gridMap.SetUnitLocation(u);
        }
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

    public void ClearSelectedTilesForNewTurn()
    {
        selectedTile = new Tile();
        originTile = new Tile();

    }

    #region Pathfinding

    public void DrawMovementRangeFor(Unit unit)
    {
        range = movement.GetTilesInRange(gridMap, unit);
        DrawRange(range);
    }
    internal void DrawRange(List<TileInfo> tiles)
    {   
        for(int i = tiles.Count - 1; i >= 0; --i)
        {
            rangeTilemap.SetTile(new Vector3Int(tiles[i].position.x, tiles[i].position.y, 0), rangeTile);
        }
    }

    //Need to add Movement types to units.  In the tutorial, it's done with a component, but I think for how simple movement types work here, it might be better to use a enum in a 
    //field on the Unit object.  Then we can just use different search types based on the unit's movementType field value.

    internal int[] GetAxisBounds(int rangeFrom, int moveRange)
    {
        int min = rangeFrom - moveRange >= 0 ? rangeFrom - moveRange : 0;
        int max = rangeFrom + moveRange <= (gridMap.length-1) ? rangeFrom + moveRange : (gridMap.length-1);

        return new int[]{min, max};
    }

    internal List<Tile> GetAttackRangeAndDraw(Unit unit)
    {
        int[] weaponRng = unit.GetWeaponRange();

        Tile rangeStart = unit.GetCurrentTile();

        int[] xBounds = GetAxisBounds(rangeStart.x, weaponRng[weaponRng.Length-1]);
        int[] yBounds = GetAxisBounds(rangeStart.y, weaponRng[weaponRng.Length-1]);
        List<Tile> atkRange = new List<Tile>();
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
                    rangeTilemap.SetTile(new Vector3Int(temp.x, temp.y, 0), atkRangeTile);
                    atkRange.Add(new Tile(temp.x, temp.y));
                }
            }
        }
        //I think putting these in the correct order would help here. Need to figure out the best way to order these. I think maybe ordering by y then by x should work?
        return atkRange;
    }
    

    internal List<PathNode> SetNewPath(Tile startTile, Tile endTile, MovementTypes movementType)
    {
        List<PathNode> path = new List<PathNode>();
        if(startTile != endTile)
        {
            path = pathfinding.FindPath(startTile.x, startTile.y, endTile.x, endTile.y, movementType);
        }

        return path;
    }
    public void DrawPath(List<PathNode> path)
    {
        pathTilemap.ClearAllTiles();

        if(path != null)
        {
            for (int i = 0; i < path.Count; i++)
            {
                pathTilemap.SetTile(new Vector3Int(path[i].xPos, path[i].yPos, 0), pathTile);
            }
        }
    }

    public void ClearPathTilemapAndPathfinding()
    {
        pathTilemap.ClearAllTiles();
        rangeTilemap.ClearAllTiles();
    }
    #endregion

    public void UpdateUnitMapLocation(Tile tile, Unit unit)
    {
        gridMap.UpdateUnitLocation(tile, unit);
    }


}
