using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    internal void EquipSelectedUnit(string weapon)
    {
        selectedUnit.EquipWeapon(weapon);
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

    public void RefreshUnits()
    {
        foreach (var unit in units)
        {
            unit.hasUnitActed = false;
            unit.hasUnitMoved = false;
            //might be unnecessary, but it's good to refresh I think.
            UpdateUnitMap(unit.currentTile, unit);
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
            selectedUnit = toBeSelected.IsActive() ? toBeSelected: null;
            return selectedUnit;
        }
        else 
        {
            return null;
        }
    }

    public bool CheckForUnit(Tile tile)
    {
        Unit toBeSelected;
        unitMap.TryGetValue(tile, out toBeSelected);
        return toBeSelected != null? true: false;
    }

    //Gets without selecting!
    //Modified this to not check if the unit is active, which makes sense.  We should be able to be attacked even if we aren't active.  Although I guess it'll never happen? IDK this is fine
    public Unit GetUnitAt(Tile tile)
    {
        Unit toBeReturned;
        
        if (unitMap.TryGetValue(tile, out toBeReturned))
        {
            return toBeReturned;
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

   public void DeactivateUnit(Unit unit)
   {
       unit.DeactivateUnit();
   }

    public void KillUnit(Unit toBeKilled)
    {
        
        toBeKilled.TeleportTo(new Tile(-10,-10));
        unitMap[toBeKilled.GetCurrentTile()] = null;
        toBeKilled.Kill(); 
    }

    public List<InventoryItem> GetSelectedUnitInventory()
    {
        List<InventoryItem> ls = selectedUnit.GetInventoryItems();
        ls.Print();
        return selectedUnit.GetInventoryItems();
    }

    public List<InventoryItem> GetUnitInventoryAt(Tile unitLocation)
    {
        Unit unit = unitMap[unitLocation];
        if(unit == null) { return null; }

        return unit.GetInventoryItems();
    }

    public Unit SelectUnitByName(string name)
    {
        return units.FirstOrDefault(u => u.unit_name == name);
    }

}
