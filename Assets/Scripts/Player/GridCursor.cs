using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridCursor : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform movePoint;
    public Transform thisTransform;
    SpriteRenderer spriteRenderer;
    public int moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCursor();
    }

    internal void DisableSprite()
    {
        spriteRenderer.enabled = false;
    }

    internal void EnableSprite()
    {
        spriteRenderer.enabled = true;
    }

    void MoveCursor()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }
    public void MoveMovePoint(Vector3 moveBy)
    {
        movePoint.position += moveBy;
    }

    public void MoveTo(Vector3 moveTo)
    {
        movePoint.position = new Vector3(moveTo.x + 0.5f, moveTo.y + 0.5f, 0);
    }

    public void TeleportCursor(Tile destinationTile)
    {
        Vector3 destination = tilemap.CellToWorld(new Vector3Int(destinationTile.x, destinationTile.y, 0));
        transform.position = new Vector3(destination.x + 0.5f, destination.y + 0.5f, 0f);
        movePoint.position = transform.position;
    }
    public bool CanMove()
    {
        return Vector3.Distance(transform.position, movePoint.position) <= .05f;
    }

    public Vector3 GetLocation()
    {
       Vector3 worldPosition = transform.position;
       //Debug.Log(worldPosition);
       return worldPosition;

    }

    public Tile GetTile()
    {
        Vector3 worldPosition = transform.position;
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return new Tile(cellPosition.x, cellPosition.y);
    }
    
}
