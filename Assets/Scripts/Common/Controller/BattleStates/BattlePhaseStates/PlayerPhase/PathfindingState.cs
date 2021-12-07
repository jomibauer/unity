using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingState : BattleState
{
    Tile pathfindStart;
    Tile cursorTile;
    int moveRange;
    float pathScore;
    MovementTypes movementType;
    List<PathNode> path;
    public override void Enable()
    {
        base.Enable ();
        gridCursor.DisableSprite(); 
        if(turn.hasUnitMoved)
        {
            owner.ChangeState<CommandSelectionState>();
        }

        pathfindStart = unitController.GetSelectedUnitTile();
        
        cursorTile = gridCursor.GetTile();
        SelectTile(cursorTile);

        moveRange = unitController.GetSelectedUnit().GetMoveRange();
        gridController.DrawMovementRangeFor(unitController.GetSelectedUnit());
        movementType = unitController.GetSelectedUnit().GetMovementType();

        path = gridController.SetNewPath(pathfindStart, cursorTile, movementType);
        pathScore = gridController.GetPathScore(path, movementType);

        gridController.DrawPath(path);
        
        this.PostNotification(NotificationBook.INPUT_ON);
    }
    protected override void OnMove(object sender, object e)
    {
        
        Tile t = (Tile)e + pos;
        //check the tile is on the map and in range
        if (!gridController.CheckTile(t) || !gridController.CheckTileIsInRange_search(t)) { return; }
        //if it is, we can safely move the cursor to the tile
        cursorTile = t;
        SelectTile(cursorTile);

        //if we move into the tile we're starting from, reset the path.
        if(cursorTile == pathfindStart) 
        {
            path = new List<PathNode>();
        }
        //check the tile is not already on our path(e.g. we made a circle) or if we've travelled across too many squares while staying in our moveRange
        //if either is true, get a more efficient/direct path to the tile
        else if (path.Contains((PathNode)cursorTile) || pathScore > moveRange)
        {
            path = gridController.SetNewPath(pathfindStart, cursorTile, movementType);
            pathScore = gridController.GetPathScore(path, movementType);
        } 
        //otherwise, add the tile to our path, 
        else
        {
            path.Add((PathNode)cursorTile);
            pathScore += gridController.GetPathScore((PathNode)cursorTile, movementType);
            
        }
        //then draw the new path
        gridController.DrawPath(path);
        
    }

    protected override void OnConfirm(object sender, object e)
    {
        
        gridController.ClearPathTilemapAndPathfinding();
        gridController.SetOriginTile(pathfindStart);

        unitController.InitUnitPath(path);

        turn.hasUnitMoved = true;
        turn.actor.hasUnitMoved = true;

        owner.ChangeState<TraversalState>();
    }

    protected override void OnCancel(object sender, object e)
    {
        gridCursor.EnableSprite();
        gridController.ClearPathTilemapAndPathfinding();
        owner.ChangeState<MoveTargetState>();
    }
}
