using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileInfo
{
    public Tile position;
    TerrainTypes terrain;
    GameObject content;
    [HideInInspector]
    public TileInfo prev;
    [HideInInspector]
    public float distance;
    public Unit unitContent;

    public TileInfo(int x, int y, TerrainTypes terrain, GameObject content =null)
    {
        this.position = new Tile(x, y);
        this.terrain = terrain;
        this.content = content;
        this.prev = null;
        this.unitContent = null;
        this.distance = int.MaxValue;   
    }
    public TerrainTypes GetTerrain()
    {
        return this.terrain;
    }

    public void SetTerrain(TerrainTypes terrain)
    {
        this.terrain = terrain;
    }

    public GameObject GetContent()
    {
        return this.content;
    }

    public void SetContent(GameObject gameObject)
    {
        this.content = gameObject;
    }

    public Unit GetUnit()
    {
        return unitContent;
    }

    public void SetUnit(Unit unit)
    {
        this.unitContent = unit;
    }
}
