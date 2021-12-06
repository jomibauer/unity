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
        owner.ChangeState<MoveTargetState>();
    }
}
