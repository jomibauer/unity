using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public virtual List<TileInfo> GetTilesInRange(GridMap gridMap, Unit unit)
    {
        List<TileInfo> retValue = gridMap.Search(unit, ExpandSearch);
        ApplyFilters(retValue, unit);
        return retValue;
    }

    protected virtual bool ExpandSearch(TileInfo from, TileInfo to, Unit unit)
    {
        return InRange(from, to, unit);
    }

    protected virtual bool InRange(TileInfo from, TileInfo to, Unit unit)
    {
        return (from.distance + MovementCostBook.Lookup[unit.GetMovementType()][from.GetTerrain()]) <= unit.GetMoveRange();
    }

    protected virtual void ApplyFilters (List<TileInfo> tiles, Unit unit)
    {
        List<TerrainTypes> forbiddenTerrain = CreateTerrainFilter(tiles, unit);
        Factions faction = unit.GetFaction();
        
        //loop backwards because we're mutating as we're looping.
        for(int i = tiles.Count - 1; i >= 0; --i)
        {
            if(FilterByTerrain(forbiddenTerrain, tiles[i]))
            {
                tiles.RemoveAt(i);
            }
            else if(FilterByContents(tiles[i]))
            {
                tiles.RemoveAt(i);
            }
            //forbid passing through tiles that contain units of other factions.  Exception is that units on our faction or friendly other faction can pass through each others squares.
            else if(FilterByFaction(faction, tiles[i]))
            {
                tiles.RemoveAt(i);
            }
        }
    }
    private bool FilterByTerrain(List<TerrainTypes> forbiddenTerrain, TileInfo tile)
    {
        return forbiddenTerrain.Contains(tile.GetTerrain());
    }

    private bool FilterByFaction(Factions faction, TileInfo tile)
    {
        if(tile.GetUnit() != null)
        {
            switch (faction)
            {
                case Factions.player: case Factions.friendly_other:
                    if(tile.GetUnit().GetFaction() == Factions.enemy || tile.GetUnit().GetFaction() == Factions.unfriendly_other) 
                    {
                        return true;
                    }
                    break;
                default:
                    if(tile.GetUnit().GetFaction() != faction)
                    {
                        return true;
                    }
                    break;
            }
        }
        return false;
    }

    private bool FilterByContents(TileInfo tile)
    {
        //Once we get some more contents, there's gonna be a little more logic here.
        if(tile.GetContent() != null)
        {
            return true;
        }
        return false;
    }

    private List<TerrainTypes> CreateTerrainFilter(List<TileInfo> tiles, Unit unit)
    {
        List<TerrainTypes> forbidden = new List<TerrainTypes> { TerrainTypes.in_wall };
        //forbid certain tiles depending on movement type
        switch (unit.GetMovementType())
        {
            case MovementTypes.foot: case MovementTypes.foot_heavy: case MovementTypes.foot_light:
                forbidden.Add(TerrainTypes.gap);
                break;
            case MovementTypes.mount:
                forbidden.Add(TerrainTypes.gap);
                forbidden.Add(TerrainTypes.out_wall);
                break;
        }

        return forbidden;
    }
}