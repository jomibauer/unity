using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingState : BattleState
{
    Tile pathfindStart;
    Tile cursorTile;
    int moveRange;
    List<PathNode> path;
    public override void Enable()
    {
        base.Enable ();
        gridCursor.DisableSprite();
        pathfindStart = unitController.GetSelectedUnitTile();
        cursorTile = gridCursor.GetTile();
        moveRange = unitController.GetSelectedUnit().GetMoveRange();
        gridController.DrawRange(pathfindStart, moveRange);
        path = gridController.SetNewPathAndDraw(pathfindStart, cursorTile);
        
        if(turn.hasUnitMoved)
        {
            owner.ChangeState<CommandSelectionState>();
        }
    }
    protected override void OnMove(object sender, object e)
    {
        Tile t = (Tile)e + pos;
        //check the tile is on the map and in range
        if (!gridController.CheckTile(t) || !gridController.CheckTileIsInRange(t, pathfindStart, moveRange)) { return; }
        //if it is, we can safely move the cursor to the tile
        cursorTile = t;
        SelectTile(cursorTile);
        //check the tile is not already on our path(e.g. we made a circle) or if we've travelled across too many squares while staying in our moveRange
        //if either is true, redraw the path
        if (path.Contains((PathNode)cursorTile) || path.Count > moveRange)
        {
            path = gridController.SetNewPathAndDraw(pathfindStart, cursorTile);
        } 
        //otherwise, add the tile to our path, then draw the new path
        else
        {
            path.Add((PathNode)cursorTile);
            gridController.DrawPath(path);
        }
        
    }

    protected override void OnConfirm(object sender, object e)
    {
        //List<PathNode> pathNodes = gridController.GetPath(pathfindStart, cursorTile);
        
        gridController.ClearPathTilemapAndPathfinding();
        gridController.SetOriginTile(pathfindStart);

        unitController.MoveSelectedUnit(path);

        turn.hasUnitMoved = true;
        turn.actor.hasUnitMoved = true;

        SelectTile(unitController.GetSelectedUnitTile());
        
        owner.ChangeState<CommandSelectionState>();
    }

    protected override void OnCancel(object sender, object e)
    {
        gridCursor.EnableSprite();
        gridController.ClearPathTilemapAndPathfinding();
        owner.ChangeState<MoveTargetState>();
    }
}
