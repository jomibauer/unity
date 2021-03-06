using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSelectionState : BaseAbilityMenuState
{
    protected override void LoadMenu ()
    {
        if (menuOptions == null)
        {
            menuTitle = "Commands";
            menuOptions = new List<string>(3);
            menuOptions.Add("Action");
            menuOptions.Add("Item");
            menuOptions.Add("Wait");
        }
        abilityMenuPanelController.Show(menuTitle, menuOptions);
        abilityMenuPanelController.SetLocked(0, turn.hasUnitActed);
        abilityMenuPanelController.SetLocked(1, turn.hasUnitActed);
    }

    protected override void Confirm ()
    {
        
        switch (abilityMenuPanelController.selection)
        {
            case 0: // Action
                Debug.Log("action");
                owner.ChangeState<CategorySelectionState>();
                break;
            case 1: // Item
                Debug.Log("item");
                owner.ChangeState<ItemSelectionState>();
                break;
            case 2: // Wait
                Debug.Log("wait!");
                turn.actor.hasUnitActed = true;
                turn.actor.hasUnitMoved = true;
                owner.ChangeState<CompleteTurnState>();
                break;
        }
    }

    protected override void Cancel ()
    {
      if (turn.hasUnitMoved && !turn.lockMove)
      {
        turn.UndoMove();
        //TeleportSelectTile(turn.actor.currentTile);
        owner.ChangeState<PathfindingState>();
      }
      else
      {
        owner.ChangeState<MoveTargetState>();
      }
    }
}
