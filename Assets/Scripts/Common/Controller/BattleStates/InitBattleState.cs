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
        yield return new WaitForEndOfFrame();
        //Tile t = new Tile(0, 0);
        //for now we're just selecting Blobby, but I guess we'll want to make this select our 'Lord' unit
        Unit u = unitController.SelectUnitByName("Blobby");
        Debug.Log(u.GetCurrentTile());
        Tile t = unitController.SelectUnitByName("Blobby").GetCurrentTile();
        
        owner.units = unitController.GetUnits();
        yield return new WaitForEndOfFrame();
        TeleportSelectTile(u.GetCurrentTile());
        owner.ChangeState<MoveTargetState>();
    }
}
