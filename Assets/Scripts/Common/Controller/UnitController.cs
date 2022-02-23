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
    public List<Unit> units;
    public Dictionary<Factions, List<Unit>> factionsUnitList;
    UnitMover unitMover;
    UnitFactory unitFactory;
    Unit selectedUnit;
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        unitFactory = FindObjectOfType<UnitFactory>();
        units = new List<Unit>();
        unitMap = new Dictionary<Tile, Unit>();
        unitMover = GetComponentInChildren<UnitMover>();
        factionsUnitList = new Dictionary<Factions, List<Unit>>();
        factionsUnitList.Add(Factions.player, new List<Unit>());
        factionsUnitList.Add(Factions.enemy, new List<Unit>());
        factionsUnitList.Add(Factions.friendly_other, new List<Unit>());
        factionsUnitList.Add(Factions.unfriendly_other, new List<Unit>());

        this.AddObserver(OnUnitTileUpdate, NotificationBook.UNIT_TILE_UPDATE);
    }

    //init units using the new levelData unitmaps instead of static unit placements.
    internal void InitUnits(Dictionary<Factions, Dictionary<Tile, Unit>> unitMaps)
    {
        
        foreach(Factions faction in EnumExtensions.GetValues<Factions>())
        {
            if(faction == Factions.None) { continue; }
            else if (unitMaps[faction] == null || unitMaps[faction].Count == 0) { continue; }

            foreach(Tile tile in unitMaps[faction].Keys)
            {
                Debug.LogWarning($"[UnitController.InitUnits]:{unitMaps[faction][tile]}");
                InitUnit(unitMaps[faction][tile], tile, faction);

            }
        }
        /* foreach(var unit in units)
        {
            UpdateUnitLocation(unit);
            unitMap[unit.GetCurrentTile()] = unit;
            factionsUnitList[unit.GetFaction()].Add(unit);
        } */
    }

    internal void InitUnit(Unit unit, Tile tile, Factions faction)
    {
        units.Add(unit);
        unitMap[tile] = unit;
        factionsUnitList[faction].Add(unit);
        PlaceUnitAt(tile, unit);
        unitFactory.InitUnit(unit);
    }

    internal void EquipSelectedUnit(string weapon)
    {
        selectedUnit.EquipWeapon(weapon);
    }

    public List<Unit> GetUnits()
    {
        return units;
    }

    public List<Unit> GetUnitsInFaction(Factions type)
    {
        return factionsUnitList[type];
    }

    internal void ResetUnitsOnFaction(Factions faction)
    {
        foreach(var unit in factionsUnitList[faction])
        {
            unit.ActivateUnit();
        }
    }


    public void UpdateUnitMap(Tile oldLocation, Unit unit)
    {
        Unit toBeSelected;
        unitMap.TryGetValue(oldLocation, out toBeSelected);
        if (toBeSelected != null) { unitMap.Remove(oldLocation); }
        
        unitMap[unit.GetCurrentTile()] = unit;
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

    public void InitUnitPath(List<PathNode> pathNodes)
    {
        selectedUnit.InitPath(pathNodes);
    }

    public bool UnitFinishedMoving()
    {
        return selectedUnit.currentTile == selectedUnit.targetTile && selectedUnit.transform.position == selectedUnit.movePoint.position;
    }
    public void MoveSelectedUnit()
    {
        selectedUnit.Move();
        UpdateUnitLocation(selectedUnit);
    }
    public void UpdateUnitLocation(Unit unit)
    {
        // Converts worldposition into a Tile value, then set Unit's current position to the new Tile.
        Vector3Int cellPosition = tilemap.WorldToCell(unit.transform.position);
        Tile newTile = new Tile(cellPosition.x, cellPosition.y);

        unit.currentTile = newTile;
    }

    public void PlaceUnitAt(Tile tile, Unit unit)
    {
        try{
            unit.currentTile = tile;
            Vector3 worldPosition = tilemap.CellToWorld(new Vector3Int(tile.x, tile.y, 0));
            unit.transform.position = new Vector3(worldPosition.x + 0.5f, worldPosition.y + 0.5f, 0);
        }
        catch(NullReferenceException e)
        {
            Debug.LogError($"Unit: {unit}, Tile: {tile}");
            throw e;
        }
    }

    private void OnUnitTileUpdate(object sender, object u)
    {
        Unit unit = u as Unit;
        UpdateUnitLocation(unit);
    }
    public void TeleportUnit(Unit unit, Tile destinationTile)
    {
        Vector3 destination = tilemap.CellToWorld(new Vector3Int(destinationTile.x, destinationTile.y, 0));
        unit.TeleportTo(destination);
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
        Vector3 offTheGrid = tilemap.CellToWorld(new Vector3Int(-10, -10, 0));
        toBeKilled.TeleportTo(offTheGrid);
        unitMap[toBeKilled.GetCurrentTile()] = null;
        toBeKilled.Kill(); 
    }

    public List<InventoryItem> GetSelectedUnitInventory()
    {
        List<InventoryItem> ls = selectedUnit.GetInventoryItems();
        ls.Print();
        return ls;
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

    public void ToggleUnitMover()
    {
        unitMover.enabled = !unitMover.enabled;
    }

}
