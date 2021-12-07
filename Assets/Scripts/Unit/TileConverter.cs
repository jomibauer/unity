using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileConverter : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;
    void Start()
    {
        
    }

    public Vector3 TileToWorld(Tile tile)
    {
        Vector3 worldPos = tilemap.CellToWorld(new Vector3Int(tile.x, tile.y, 0));
        return worldPos;
    }

    public Tile WorldToTile(Vector3 worldPos)
    {
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        return new Tile(cell.x, cell.y);
    }
    
}
