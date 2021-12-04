using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public virtual List<TileInfo> GetTilesInRange(GridMap gridMap, Unit unit)
    {
        List<TileInfo> retValue = gridMap.Search(unit, ExpandSearch);
        Filter(retValue);
        return retValue;
    }

    protected virtual bool ExpandSearch(TileInfo from, TileInfo to, Unit unit)
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

        if(forbidden.Contains(to.GetTerrain())) { return false; }

        return InRange(from, to, unit);
    }

    protected virtual bool InRange(TileInfo from, TileInfo to, Unit unit)
    {
        return (from.distance + MovementCostBook.Lookup[unit.GetMovementType()][from.GetTerrain()]) <= unit.GetMoveRange();
    }

    protected virtual void Filter (List<TileInfo> tiles)
    {
        for(int i = tiles.Count - 1; i >= 0; --i)
        {
            if(tiles[i].GetContent() != null)
            {
                tiles.RemoveAt(i);
            }
        }
    }
}