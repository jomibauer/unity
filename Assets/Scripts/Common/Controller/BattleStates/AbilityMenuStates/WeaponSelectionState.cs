using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionState : BaseAbilityMenuState
{
    protected override void LoadMenu ()
    {
        Unit selectedUnit = unitController.GetSelectedUnit();
        List<InventoryItem> items = unitController.GetSelectedUnitInventory();
        if (menuOptions == null)
        {
            menuTitle = "Items";
            menuOptions = new List<string>();
            items.ForEach(i => menuOptions.Add(i.displayName));
            
        }
        
        abilityMenuPanelController.Show(menuTitle, menuOptions);
        for(int i = 0; i < menuOptions.Count; ++i)
            {
                if(items[i].type != "weapon" || !selectedUnit.CanUseWeapon(items[i].name))
                {
                    abilityMenuPanelController.SetLocked(i, true);
                }
            }
        
    }
    protected override void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    protected override void Confirm()
    {
        List<InventoryItem> inventory = unitController.GetSelectedUnitInventory();
        unitController.EquipSelectedUnit(inventory[abilityMenuPanelController.selection].name);
        Debug.Log($"[WeaponSelectionState.cs]: {menuOptions[abilityMenuPanelController.selection]}");
        owner.ChangeState<TargetSelectionState>();
    }
}
