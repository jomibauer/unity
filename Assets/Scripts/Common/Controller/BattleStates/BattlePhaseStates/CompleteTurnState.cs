using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteTurnState : BattleState
{
    
    public override void Enable()
    {
        base.Enable();
        StartCoroutine(CompleteTurn());
    }

    IEnumerator CompleteTurn()
    {
        yield return null;
        Tile pathOrigin = gridController.GetOriginTile();
        Unit selectedUnit = unitController.GetSelectedUnit();

        unitController.UpdateUnitMap(pathOrigin, selectedUnit);
        gridController.UpdateUnitMapLocation(pathOrigin, selectedUnit);

        unitController.DeactivateUnit(selectedUnit);
        
        gridController.ClearSelectedTilesForNewTurn();
        unitController.DeselectUnit();

        List<Unit> playerUnits = unitController.factionsUnitList[Factions.player];
        bool playerPhaseIsFinished = true;
        for(int i = 0; i < playerUnits.Count; ++i)
        {
            if(playerUnits[i].IsActive())
            {
                playerPhaseIsFinished = false;
                break;
            }
        }
        if (playerPhaseIsFinished)
        {
            owner.ChangeState<ExitPlayerPhaseState>();
        }
        owner.ChangeState<MoveTargetState>();
    }
}
