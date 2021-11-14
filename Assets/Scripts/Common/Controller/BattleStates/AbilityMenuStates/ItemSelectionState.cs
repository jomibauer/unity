using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectionState : BaseAbilityMenuState
{
    protected override void LoadMenu ()
    {
        if (menuOptions == null)
        {
            menuTitle = "Items";
            menuOptions = new List<string>(3);
            menuOptions.Add("Potion");
            menuOptions.Add("Trap");
            menuOptions.Add("Sword");
        }
        
        abilityMenuPanelController.Show(menuTitle, menuOptions);
    }
    protected override void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    protected override void Confirm()
    {
        turn.hasUnitActed = true;
        turn.actor.hasUnitActed = true;
        if (turn.hasUnitMoved)
        {
            turn.lockMove = true;
        }
        owner.ChangeState<CommandSelectionState>();
    }

}
