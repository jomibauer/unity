using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Unit : MonoBehaviour
{
    public string dataPath;
    [SerializeField] public string unit_name;
    public SpriteRenderer spriteRenderer;
    public Weapon weapon;
    public Inventory inventory;
    public Behavior behavior;
    public Stats stats;
    public UnitStats unitStats;
    public LevelComponent levelComponent;
    public int HP;
    public bool hasUnitMoved = false;
    public bool hasUnitActed = false;
    public bool isActive;
    public Directions direction;
    public Tile currentTile;
    public Tile targetTile;
    public List<Tile> path;
    public int moveSpeed = 5;
    public Transform movePoint;
    public TileConverter tileConverter;

    void Start()
    {
        /* UnitFactory factory = FindObjectOfType<UnitFactory>();
        factory.InitUnit(this); */
       
    }
    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        
        if (ReadyToMoveToNextNode() && PathNotEmpty())
        {
            MoveToNextTileInPath();  
        }
        
    }


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
    public void TeleportTo(Vector3 destination)
    {   
        transform.position = new Vector3(destination.x + 0.5f, destination.y + 0.5f, 0f);
        movePoint.position = transform.position;
        this.PostNotification(NotificationBook.UNIT_TILE_UPDATE, this);
    }
    public void TeleportTo(Tile destinationTile)
    {   
        Vector3 destination = tileConverter.TileToWorld(destinationTile);
        transform.position = new Vector3(destination.x + 0.5f, destination.y + 0.5f, 0f);
        movePoint.position = transform.position;
        this.PostNotification(NotificationBook.UNIT_TILE_UPDATE, this);
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

    #region Utility
    public int GetMoveRange()
    {
        return stats[StatTypes.MOV];
    }

    public MovementTypes GetMovementType()
    {
        return this.unitStats.movementType;
    }

    public string GetClassName()
    {
        return this.unitStats.unit_class.className;
    }

    public string GetSpriteName()
    {
        return this.unitStats.color + this.unitStats.unit_class.dataName;
    }

    public Factions GetFaction()
    {
        return this.unitStats.faction;
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

    public void EquipFirstWeapon()
    {
        foreach(var item in this.inventory.items)
        {
            if(item.type == "weapon")
            {
                if(CanUseWeapon(item.name))
                {
                    EquipWeapon(item.name);
                    break;
                }
            }
        }
    }

    public void EquipWeapon(string weapon)
    {
        WeaponData _weapondata = Resources.Load<WeaponData>("Items/Weapons/" + weapon); 
        this.weapon.Equip(_weapondata);
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

    public int[] GetWeaponRange()
    {
        return weapon.attackRange;
    }

    public bool CanUseWeapon(WeaponData weapon)
    {
        return unitStats.unit_class.weaponTypes.Contains(weapon.type);
    }

    public bool CanUseWeapon(string weapon)
    {
        WeaponData _weapon = Resources.Load<WeaponData>("Items/Weapons/" + weapon);
        return CanUseWeapon(_weapon);
    }

    public List<InventoryItem> GetInventoryItems()
    {
        return inventory.GetItems();
    }
    #endregion
    
    public string DebugInfo()
    {
        string toLog = $"Name: {this.unit_name}\n";
        toLog += $"hasMoved?: {this.hasUnitMoved} hasActed?:{this.hasUnitActed}\n";
        toLog += $"location: {this.GetCurrentTile()}\n";
        return toLog;
    }
}
