using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionState : BaseAbilityMenuState
{
    protected override void LoadMenu ()
    {
        if (menuOptions == null)
        {
            menuTitle = "Items";
            menuOptions = new List<string>(3);
            menuOptions.Add("Sword");
            menuOptions.Add("Bow");
            menuOptions.Add("Magic");
        }
        
        abilityMenuPanelController.Show(menuTitle, menuOptions);
    }
    protected override void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    protected override void Confirm()
    {
        /* turn.hasUnitActed = true;
        turn.actor.hasUnitActed = true;
        if (turn.hasUnitMoved)
        {
            turn.lockMove = true;
        } */
        //set unit weapon here.
        owner.ChangeState<TargetSelectionState>();
    }
}
