using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : BattleState
{
    
    public override void Enable()
    {
        base.Enable();
        gridCursor.EnableSprite();
    }
    protected override void OnMove(object sender, object e)
    {
        Tile t = (Tile)e + pos;
        SelectTile(t);
        Unit selectedUnit = SelectUnitAt(t);
        if(selectedUnit)
        {
            this.PostNotification("UNIT_INFO", selectedUnit);
        }
        else if (gridController.CheckTile(t))
        {
            this.PostNotification("TILE_INFO", t);
        }
    }

    protected override void OnConfirm(object sender, object e)
    {
        Tile t = (Tile)e;
        //Debug.Log(e.info + pos);
        Unit selectedUnit = SelectUnitAt(gridCursor.GetTile());
        if(selectedUnit)
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
        //Debug.Log(selectedUnit);
    }

    protected override void OnCancel(object sender, object e)
    {
        Debug.Log("Cancel");
    }

    protected override void OnInfo(object sender, object e)
    {
        //Tile t = (Tile)e;
        Debug.Log("Info");
    }

    protected override void OnPause(object sender, object e)
    {
        Debug.Log("Paws");
    }

    protected Unit SelectUnitAt(Tile tile)
    {
        Unit unit = unitController.SelectUnitAt(tile);
        this.PostNotification("SELECTED_UNIT", unitController.GetSelectedUnit());
        return unit;
    }
}
