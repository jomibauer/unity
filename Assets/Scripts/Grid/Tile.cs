using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tile
{
    public int x;
    public int y;

    public Tile (int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"[{x}, {y}]";
    }

    public static Tile operator +(Tile a, Tile b)
    {
      return new Tile(a.x + b.x, a.y + b.y);
    }
    public static Tile operator -(Tile p1, Tile p2) 
    {
      return new Tile(p1.x - p2.x, p1.y - p2.y);
    }
    public static bool operator ==(Tile a, Tile b)
    {
      return a.x == b.x && a.y == b.y;
    }
    public bool Equals(Tile tile)
    {
        if (System.Object.ReferenceEquals(this, tile))
        {
            return true;
        }
        if (tile == null || GetType() != tile.GetType())
        {
            return false;
        }
        
        return (x == tile.x) && (y == tile.y);
    }

    public override int GetHashCode() => (x, y).GetHashCode();

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public static bool operator !=(Tile a, Tile b)
    {
      return !(a == b);
    }

    public static explicit operator PathNode(Tile t) => new PathNode(t.x, t.y);
}
