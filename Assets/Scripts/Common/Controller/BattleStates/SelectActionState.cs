using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectActionState : BattleState
{
    //pathorigin preserves the location the unit travelled from so that player can undo a move in this state by pressing cancel.
    Tile pathOrigin;
    Tile cursorTile;
    Unit selectedUnit;
    public override void Enable()
    {
        base.Enable ();
        pathOrigin = gridController.GetOriginTile();
        selectedUnit = unitController.GetSelectedUnit();
        cursorTile = gridCursor.GetTile();
    }

    protected override void OnConfirm(object sender, object e)
    {
        unitController.UpdateUnitMap(selectedUnit.GetCurrentTile(), selectedUnit);
        selectedUnit = null;
        unitController.DeselectUnit();
        owner.ChangeState<MoveTargetState>();

    }

    protected override void OnCancel(object sender, object e)
    {
        unitController.TeleportUnit(selectedUnit, pathOrigin);
        owner.ChangeState<PathfindingState>();
    }
}
