using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMenuState : BaseMenuState
{
    public override void Enable()
    {
        gridCursor.DisableSprite();
        base.Enable();
        controller = mapMenuController;
        mapMenuController.SwitchToMain();
        mapMenuController.ShowMenu();
        mapMenuController.Next();
    }

    public override void Disable()
    {
        base.Disable();
        //mapMenuController.HideMenu();
    }

    protected override void Confirm()
    {
        switch( mapMenuController.GetSelection() )
        {
            case 0: // Unit
                Debug.Log("Unit");
                var unitsToLog = unitController.GetUnitsInFaction(Factions.player);
                foreach(var unit in unitsToLog) {
                    Debug.Log(unit.DebugInfo());
                }
                break;
            case 1: // Options
                Debug.Log("Options");
                break;
            case 2: // Objectives
                Debug.Log("Objectives");
                break;
            case 3: // Suspend
                Debug.Log("Suspend");
                break;
            case 4: // End
                owner.ChangeState<EndTurnConfirmState>();
                break;
        }
    }

    protected override void Cancel()
    {
        owner.ChangeState<ExitMapMenuState>();
    }

    protected override void LoadMenu()
    {
        throw new System.NotImplementedException();
    }

}