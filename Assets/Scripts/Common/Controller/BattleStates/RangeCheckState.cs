using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheckState : BattleState
{
    public override void Enable()
    {
        base.Enable ();
        Unit rangeCheckTarget = GetUnitAt(gridCursor.GetTile());
        gridController.DrawMovementRangeFor(rangeCheckTarget);
        this.PostNotification(NotificationBook.INPUT_ON);
    }

    public override void Disable()
    {
        base.Disable();
        gridController.ClearPathTilemapAndPathfinding();
    }

    protected override void OnMove(object sender, object e)
    {
        Tile t = (Tile)e + pos;
        SelectTile(t);
        Unit selectedUnit = GetUnitAt(t);
        if(selectedUnit)
        {
            this.PostNotification(NotificationBook.UNIT_INFO, selectedUnit);
        }
        else if (gridController.CheckTile(t))
        {
            this.PostNotification(NotificationBook.TILE_INFO, t);
        }
    }

    protected override void OnCancel(object sender, object arg)
    {
        owner.ChangeState<MoveTargetState>();
    }

    protected override void OnConfirm(object sender, object arg)
    {
        owner.ChangeState<MoveTargetState>();
    }

    protected Unit GetUnitAt(Tile tile)
    {
        return unitController.GetUnitAt(tile);
    }

}
