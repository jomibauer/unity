using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    //I can assign this grid map in init battlestate.
    GridMap gridMap;
    PathNode[,] pathNodes;   

    private void Dump()
    {
        for(int i=0; i<gridMap.length; i++)
        {
            for (int j=0; j<gridMap.height; j++)
            {
                Debug.Log(pathNodes[i,j]);
            }
        }
        Debug.Log(gridMap);
    }
    public void Init(GridMap map)
    {
        gridMap = map;

        Debug.Log($"Grid size is: {gridMap.length}, {gridMap.height}");
        pathNodes = new PathNode[gridMap.length, gridMap.height];
        for(int x = 0; x < gridMap.length; x++)
        {
            for (int y = 0; y < gridMap.height; y++)
            {
                pathNodes[x, y] = new PathNode(x, y);
            }
        }
    }

    public List<PathNode> FindPath(int startX,
                                   int startY,
                                   int endX,
                                   int endY,
                                   MovementTypes movementType)
    {
        //Dump();
        PathNode startNode = pathNodes[startX, startY];
        PathNode endNode = pathNodes[endX, endY];

        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        openList.Add(startNode);

        while(openList.Count > 0)
        {
            PathNode currentNode = openList[0];

            for (int i = 0; i < openList.Count; i++)
            {
                if (currentNode.fValue > openList[i].fValue)
                {
                    currentNode = openList[i];
                }
                if (currentNode.fValue == openList[i].fValue && currentNode.hValue > openList[i].hValue)
                {
                    currentNode = openList[i];
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == endNode)
            {
                break;
            }

            List<PathNode> neighborNodes = new List<PathNode>();
            //in this loop, where we find the neighbors, I could also limit diagonal movement by simply omitting diagonal neighbors from the check entirely.  that might be
            //unnecessarily complex though.
            for (int x = -1; x <2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    //we omit the starting square and diagonal neighbors with this line
                    if ((x==0 && y == 0) || Mathf.Abs(x) == Mathf.Abs(y)) { continue; }
                    if(gridMap.CheckTile(new Tile(currentNode.xPos + x, currentNode.yPos + y)) == false)
                    {
                        continue;
                    }

                    neighborNodes.Add(pathNodes[currentNode.xPos + x, currentNode.yPos + y]);
                }
            }
            for(int i = 0; i <neighborNodes.Count; i++)
            {
                if(closedList.Contains(neighborNodes[i])) { continue; }
                if(gridMap.CheckWalkable(neighborNodes[i].xPos, neighborNodes[i].yPos) == false) { continue; }

                int movementCost = currentNode.gValue + CalculateDistance(currentNode, neighborNodes[i], movementType);

                if (openList.Contains(neighborNodes[i]) == false || movementCost < neighborNodes[i].gValue)
                {
                    neighborNodes[i].gValue = movementCost;
                    neighborNodes[i].hValue = CalculateDistance(neighborNodes[i], endNode, movementType);
                    neighborNodes[i].parentNode = currentNode;

                    if(openList.Contains(neighborNodes[i]) == false)
                    {
                        openList.Add(neighborNodes[i]);
                    }
                }
            }
        }
        return RetracePath(startNode, endNode);
    }

    

    private List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();

        PathNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistance(PathNode currentNode, PathNode target, MovementTypes movementType)
    {
        int distX = Mathf.Abs(currentNode.xPos - target.xPos);
        int distY = Mathf.Abs(currentNode.yPos - target.yPos);
        //we account for terrain types when calculating distance so the script finds the most cost effective path
        TerrainTypes targetTerrain = gridMap.tileInfoMap[new Tile(target.xPos, target.yPos)].GetTerrain();
        float terrainCost = MovementCostBook.Lookup[movementType][targetTerrain] * 10;
        // casting back to int shouldn't cause any rounding issues because we multiplied our movement costs by ten
        if (distX > distY) { return (int)(25 * distY + terrainCost * (distX-distY)); }
        return (int)(25 * distX + terrainCost * (distY-distX));
    }
}

