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
        SelectTile(pathfindStart);
        cursorTile = gridCursor.GetTile();
        moveRange = unitController.GetSelectedUnit().GetMoveRange();
        gridController.DrawRange(pathfindStart, moveRange);
        path = gridController.SetNewPath(pathfindStart, cursorTile);
        gridController.DrawPath(path);
        
        if(turn.hasUnitMoved)
        {
            owner.ChangeState<CommandSelectionState>();
        }
        this.PostNotification(NotificationBook.INPUT_ON);
    }
    protected override void OnMove(object sender, object e)
    {
        Tile t = (Tile)e + pos;
        //check the tile is on the map and in range
        if (!gridController.CheckTile(t) || !gridController.CheckTileIsInRange(t, pathfindStart, moveRange)) { return; }
        //if it is, we can safely move the cursor to the tile
        cursorTile = t;
        SelectTile(cursorTile);

        //if we move into the tile we're starting from, reset the path.
        if(cursorTile == pathfindStart) 
        {
            path = new List<PathNode>();
        }
        //check the tile is not already on our path(e.g. we made a circle) or if we've travelled across too many squares while staying in our moveRange
        //if either is true, get a more efficient path to the tile
        else if (path.Contains((PathNode)cursorTile) || path.Count > moveRange)
        {
            path = gridController.SetNewPath(pathfindStart, cursorTile);
        } 
        //otherwise, add the tile to our path, 
        else
        {
            path.Add((PathNode)cursorTile);
            
        }
        //then draw the new path
        gridController.DrawPath(path);
        
    }

    protected override void OnConfirm(object sender, object e)
    {
        
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
