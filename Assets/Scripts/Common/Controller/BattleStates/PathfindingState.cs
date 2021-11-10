using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingState : BattleState
{
    Tile pathfindStart;
    Tile cursorTile;
    public override void Enable()
    {
        base.Enable ();
        gridCursor.DisableSprite();
        pathfindStart = unitController.GetSelectedUnitTile();
        cursorTile = gridCursor.GetTile();
        gridController.DrawPath(pathfindStart, cursorTile);

        if(turn.hasUnitMoved)
        {
            owner.ChangeState<CommandSelectionState>();
        }
    }
    protected override void OnMove(object sender, object e)
    {
        Tile t = (Tile)e;
        cursorTile = t + pos;
        if (!gridController.CheckTile(cursorTile)) { return; }
        SelectTile(cursorTile);
        gridController.DrawPath(pathfindStart, cursorTile);
    }

    protected override void OnConfirm(object sender, object e)
    {
        List<PathNode> pathNodes = gridController.GetPath(pathfindStart, cursorTile);
        //if(pathNodes.Count < 1) { return; }
        
        gridController.ClearPathTilemapAndPathfinding();
        gridController.SetOriginTile(pathfindStart);
        unitController.MoveSelectedUnit(pathNodes);
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
