using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : BattleState
{
    
    public override void Enable()
    {
        base.Enable();
        gridCursor.EnableSprite();
        Tile t = gridCursor.GetTile();
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
        this.PostNotification(NotificationBook.INPUT_ON);
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

    protected override void OnConfirm(object sender, object e)
    {
        Unit selectedUnit = SelectUnitAt(gridCursor.GetTile());
        if(selectedUnit.GetFaction() == Factions.player){
        //***CONFUSING LOGIC ALERT***
        //I think these checks ensure that a unit turn ends once they've moved and either acted or waited.  However, right now the unit and turn are keeping track of movements separately.
        //I'm trying to set them together, but eventually it may make sense to do this in a single method in the unit controller. a little scary as is.
            if(selectedUnit && !selectedUnit.hasUnitMoved)
            {
                turn.Change(unitController.GetSelectedUnit());
                if(turn.hasUnitMoved)
                {
                    owner.ChangeState<CommandSelectionState>();
                }
                else
                {
                    owner.ChangeState<PathfindingState>();
                }
            }
        }
        else
        {
            owner.ChangeState<RangeCheckState>();
        }
        //Debug.Log(selectedUnit);
    }

    protected override void OnCancel(object sender, object e)
    {
        Debug.Log("Cancel");
    }

    protected override void OnInfo(object sender, object e)
    {
        Unit u = SelectUnitAt(gridCursor.GetTile());
        Debug.Log(u.currentTile);
    }

    protected override void OnPause(object sender, object e)
    {
        Debug.Log("Paws");
    }

    protected Unit SelectUnitAt(Tile tile)
    {
        Unit unit = unitController.SelectUnitAt(tile);
        this.PostNotification(NotificationBook.SELECTED_UNIT, unitController.GetSelectedUnit());
        return unit;
    }

    protected Unit GetUnitAt(Tile tile)
    {
        return unitController.GetUnitAt(tile);
    }
}
