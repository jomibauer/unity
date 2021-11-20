using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public int xPos;
    public int yPos;
    public int gValue;
    public int hValue;
    public PathNode parentNode;
    public int fValue
    {
        get {
            return gValue + hValue;
        }
    }
    
    public PathNode(int xPos, int yPos)
    {
        this.xPos = xPos;
        this.yPos =yPos;
    }

    public override bool Equals(object obj) => this.Equals(obj as PathNode);
    public bool Equals(PathNode pathNode)
    {
        if (System.Object.ReferenceEquals(this, pathNode))
        {
            return true;
        }
        if (pathNode == null || GetType() != pathNode.GetType())
        {
            return false;
        }
        
        return (xPos == pathNode.xPos) && (yPos == pathNode.yPos);
    }

    public override int GetHashCode() => (xPos, yPos).GetHashCode();

    public static bool operator == (PathNode lhs, PathNode rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(PathNode lhs, PathNode rhs) => !(lhs == rhs);
    public override string ToString()
    {
        return $"[{this.xPos.ToString()}, {this.yPos.ToString()}]" ;
    }
}
