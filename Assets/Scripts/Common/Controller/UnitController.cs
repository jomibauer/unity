using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitController : MonoBehaviour
{
    public Dictionary<Tile, Unit> unitMap;
    [SerializeField]
    List<Unit> units;
    Unit selectedUnit;
    //just adding this field to make it easier for units to get the tilemap from one place... might be a simpler way.
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        units = new List<Unit>(FindObjectsOfType<Unit>());
        unitMap = new Dictionary<Tile, Unit>();
    }

    internal void InitUnitPositions()
    {
        foreach(var unit in units)
        {
            UpdateUnitMap(unit.GetCurrentTile(), unit);
            Debug.Log(unitMap[unit.GetCurrentTile()]);
        }
    }

    public List<Unit> GetUnits()
    {
        return units;
    }

    internal void CheckUnitsAndResetState()
    {
        foreach(var unit in units)
        {
            if (unit.IsActive())
            {
                return;
            }
        }
        foreach(var unit in units)
        {
            unit.ActivateUnit();
        }
    }

    public void UpdateUnitMap(Tile tile, Unit unit)
    {
        Unit toBeSelected;
        unitMap.TryGetValue(tile, out toBeSelected);
        if (toBeSelected == unit) { return; }
        unitMap.Remove(unit.GetCurrentTile());
        unitMap[tile] = unit;
    }

    public Unit SelectUnitAt(Tile tile)
    {
        Unit toBeSelected;
        
        if (unitMap.TryGetValue(tile, out toBeSelected))
        {
            selectedUnit = toBeSelected.IsActive()? toBeSelected: null;
            return selectedUnit;
        }
        else 
        {
            return null;
        }
    }

    //Gets without selecting!
    public Unit GetUnitAt(Tile tile)
    {
        Unit toBeReturned;
        
        if (unitMap.TryGetValue(tile, out toBeReturned))
        {
            return toBeReturned.IsActive()? toBeReturned: null;
        }
        else 
        {
            return null;
        }
    }

    public void MoveSelectedUnit(List<PathNode> pathNodes)
    {
        selectedUnit.InitPath(pathNodes);
    }

    public void TeleportUnit(Unit unit, Tile destinationTile)
    {
        unit.TeleportTo(destinationTile);
    }
    public void DeselectUnit()
    {
        selectedUnit = null;
    }

    public bool UnitIsSelected()
    {
        return selectedUnit != null;
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public Tile GetSelectedUnitTile()
    {
        if(!selectedUnit)
        {
            return new Tile();
        }
        return selectedUnit.GetCurrentTile();
    }

    public void SetSelectedUnitPath(List<PathNode> pathNodes)
    {
        List<Tile> tilePath = new List<Tile>();
        for (int i = 0; i < pathNodes.Count; i++)
        {
            tilePath.Add(new Tile(pathNodes[i].xPos, pathNodes[i].yPos));
        }
        selectedUnit.RefreshPath(tilePath);
        //could change this to some boolean flag in Unit that starts checking for isFinished.  Think the Unit is getting rammed with bool flags though, I probably need to look more closely at events.
        selectedUnit.DeactivateUnit();
    }

}
