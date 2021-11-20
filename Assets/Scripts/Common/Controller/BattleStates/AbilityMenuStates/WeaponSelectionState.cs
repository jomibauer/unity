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
            menuOptions.Add("Iron_sword");
            menuOptions.Add("Iron_axe");
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
        unitController.EquipSelectedUnit(menuOptions[abilityMenuPanelController.selection]);
        Debug.Log($"WeaponSelectionState: {menuOptions[abilityMenuPanelController.selection]}");
        owner.ChangeState<TargetSelectionState>();
    }
}
