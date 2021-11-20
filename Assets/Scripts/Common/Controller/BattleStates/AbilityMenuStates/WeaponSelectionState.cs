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
            menuOptions = new List<string>();
            List<InventoryItem> items = unitController.GetSelectedUnitInventory();
            items.ForEach(i => menuOptions.Add(i.displayName));
            for(int i = 0; i < menuOptions.Count; i++)
            {
                if(items[i].type != "weapon")
                {
                    abilityMenuPanelController.SetLocked(i, true);
                }
            }
        }
        
        abilityMenuPanelController.Show(menuTitle, menuOptions);
        
        
    }
    protected override void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    protected override void Confirm()
    {
        List<InventoryItem> inventory = unitController.GetSelectedUnitInventory();
        unitController.EquipSelectedUnit(inventory[abilityMenuPanelController.selection].name);
        Debug.Log($"WeaponSelectionState: {menuOptions[abilityMenuPanelController.selection]}");
        owner.ChangeState<TargetSelectionState>();
    }
}
