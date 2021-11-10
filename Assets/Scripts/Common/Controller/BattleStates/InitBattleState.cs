using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : BattleState
{

    public override void Enable()
    {
        base.Enable ();
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        gridController.InitMapAndPathfinding(levelData);
        unitController.InitUnitPositions();
        Tile t = new Tile(0, 0);
        SelectTile(t);
        owner.units = unitController.GetUnits();
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }
}
