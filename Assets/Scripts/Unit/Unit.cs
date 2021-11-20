using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Unit : MonoBehaviour
{
    [SerializeField] public string unit_name;
    [SerializeField] UnitController unitController;
    public SpriteRenderer spriteRenderer;
    [SerializeField] public Tilemap tilemap;
    [SerializeField] public string unitClass;
    public Weapon weapon;
    public Inventory inventory;
    public Stats stats;
    public UnitStats unitStats;
    public LevelComponent levelComponent;
    public int HP;
    public bool hasUnitMoved = false;
    public bool hasUnitActed = false;
    public bool isActive;
    public Tile currentTile;
    public Tile targetTile;
    public List<Tile> path;
    public int moveSpeed = 5;
    public Transform movePoint;

    void Start()
    {
        UnitFactory factory = FindObjectOfType<UnitFactory>();
        factory.InitUnit(this);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        
        if (ReadyToMoveToNextNode() && PathNotEmpty())
        {
            MoveToNextTileInPath();  
        }
        UpdateCurrentTile();
    }
    #region utility
    public void UpdateCurrentTile()
    {
        // Converts worldposition into a Tile value, then set Unit's current position to the new Tile.
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        Tile newTile = new Tile(cellPosition.x, cellPosition.y);
        //this is wasteful.  I should think on how I can do this only once I've finished moving, update the unit map more lazily.
        // potentially we could call this in move to next tile, maybe a solution there.
        unitController.UpdateUnitMap(newTile, this);
        currentTile = newTile;
    }
    #endregion

    #region moving the sprite
    private bool ReadyToMoveToNextNode()
    {
        //check that unit is in the same/nearly same position as its movePoint 
        return Vector3.Distance(transform.position, movePoint.position) <= .05;
    }
    private bool PathNotEmpty()
    {
        return path.Count > 0;
    }
    private void MoveToNextTileInPath()
    {
        
        Vector3 target = new Vector3(path[0].x - currentTile.x, path[0].y - currentTile.y, 0);
        movePoint.position += target;
        path.RemoveAt(0);
        
    }
    public void TeleportTo(Tile destinationTile)
    {
        Vector3 destination = tilemap.CellToWorld(new Vector3Int(destinationTile.x, destinationTile.y, 0));
        transform.position = new Vector3(destination.x + 0.5f, destination.y + 0.5f, 0f);
        movePoint.position = transform.position;
        UpdateCurrentTile();
    }
    #endregion 

    public void RefreshPath(List<Tile> newPath)
    {
        path = newPath;
        if (path.Count == 0 || hasUnitMoved)
        {
            return;
        }
        else {
            targetTile = new Tile(path[path.Count - 1].x, path[path.Count - 1].y);
        }
    }

    public void InitPath(List<PathNode> pathNodes)
    {
        List<Tile> tilePath = new List<Tile>();
        for (int i = 0; i < pathNodes.Count; i++)
        {
            tilePath.Add(new Tile(pathNodes[i].xPos, pathNodes[i].yPos));
        }
        
        RefreshPath(tilePath);
    }

    public int GetMoveRange()
    {
        return stats[StatTypes.MOV];
    }

    public Tile GetCurrentTile()
    {
        return currentTile;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void DeactivateUnit()
    {
        isActive = false;
        spriteRenderer.color = new Color(0.25f, 0.25f, 0.25f);
        hasUnitMoved = true;
        hasUnitActed = true;
    }

    public void ActivateUnit()
    {
        isActive = true;
        spriteRenderer.color = new Color(1f, 1f, 1f);
        hasUnitMoved = false;
        hasUnitActed = false;
    }

    public void Kill()
    {
        Destroy(this);
    }

    public void EquipWeapon(string weapon)
    {
        this.weapon.Equip(weapon);
    }

    public int GetWeaponDamage()
    {
        return weapon.stats[WeaponStatTypes.DAM];
    }

    public int GetWeaponHit()
    {
        return weapon.stats[WeaponStatTypes.HIT];
    }
    public int GetWeaponCrit()
    {
        return weapon.stats[WeaponStatTypes.CRT];
    }
    public int GetWeaponWeight()
    {
        return weapon.stats[WeaponStatTypes.WGT];
    }

    public List<InventoryItem> GetInventoryItems()
    {
        return inventory.GetItems();
    }

}
