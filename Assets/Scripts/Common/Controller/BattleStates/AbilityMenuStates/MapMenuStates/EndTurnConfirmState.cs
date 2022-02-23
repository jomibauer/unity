using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EndTurnConfirmState : BaseMenuState
{
    public int lastSelection = 3;
    public override void Enable()
    {
        base.Enable();
        mapMenuController.SwitchToConfirm("End Turn?");
        mapMenuController.Next();
    }
    public override void Disable()
    {
        base.Disable();
    }

    protected override void OnMove (object sender, object t)
    {
        Tile tile = (Tile)t;
        if (tile.x < 0)
        mapMenuController.Next();
        else if (tile.x > 0)
        mapMenuController.Previous();
    }
    protected override void Cancel()
    {
        mapMenuController.SetLastSelection(lastSelection);
        owner.ChangeState<MapMenuState>();
    }

    protected override void Confirm()
    {
        switch(mapMenuController.GetSelection())
        {
            case 0:
                mapMenuController.HideMenu();
                owner.ChangeState<ExitPlayerPhaseState>();
                break;
            case 1:
                Cancel();
                break;
        }
    }

    protected override void LoadMenu()
    {
        throw new System.NotImplementedException();
    }
}