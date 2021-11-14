using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Unit : MonoBehaviour
{
    [SerializeField] UnitController unitController;
    [SerializeField] Sprite activeSprite;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite inactiveSprite;
    [SerializeField] Tilemap tilemap;
    [SerializeField] public string unitClass;
    public UnitData data;
    public int HP;
    public bool hasUnitMoved = false;
    public bool hasUnitActed = false;
    bool isActive;
    public Tile currentTile;
    public Tile targetTile;
    List<Tile> path;
    public int moveSpeed = 5;
    public Transform movePoint;

    void Start()
    {
        movePoint.parent = null;
        path = new List<Tile>();
        isActive = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        data = Resources.Load<UnitData>($"Units/{unitClass}");
        HP = data.maxHP;
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
        return data.move;
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
        spriteRenderer.sprite = inactiveSprite;
    }

    public void ActivateUnit()
    {
        isActive = true;
        spriteRenderer.sprite = activeSprite;
        hasUnitMoved = false;
        hasUnitActed = false;
    }

}
