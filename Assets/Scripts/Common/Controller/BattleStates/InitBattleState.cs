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
        yield return new WaitForEndOfFrame();
        unitController.InitUnits();
        yield return new WaitForEndOfFrame();
        gridController.InitUnitMap(unitController.GetUnits());
        unitController.units.Print();
        //Tile t = new Tile(0, 0);
        //for now we're just selecting Blobby, but I guess we'll want to make this select our 'Lord' unit
        Unit u = unitController.SelectUnitByName("Blobby");
        Debug.Log(u.GetCurrentTile());
        Tile t = unitController.SelectUnitByName("Blobby").GetCurrentTile();
        
        yield return new WaitForEndOfFrame();
        TeleportSelectTile(u.GetCurrentTile());
        owner.ChangeState<MoveTargetState>();
    }
}
