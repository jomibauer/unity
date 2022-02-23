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
        levelController.LoadUnitMaps();
        yield return new WaitForEndOfFrame();
        // wait between inits to ensure everything is finishing in the previous init, as they depend on each other.
        gridController.InitMapAndPathfinding(levelController.GetLevel());
        yield return new WaitForEndOfFrame();
        //make changes here in unitcontroller.
        unitController.InitUnits(levelController.GetUnitMaps());
        yield return new WaitForEndOfFrame();
        //We could probably use the levelmaps here too, tbh!!
        gridController.InitUnitMap(unitController.GetUnits());
        //Tile t = new Tile(0, 0);
        //for now we're just selecting Blobby, but I guess we'll want to make this select our 'Lord' unit
        Unit u = unitController.SelectUnitByName("Blobby");
        Debug.Log(u.GetCurrentTile());
        Tile t = u.GetCurrentTile();

        TeleportSelectTile(u.GetCurrentTile());
        yield return new WaitForEndOfFrame();
        owner.ChangeState<MoveTargetState>();
    }
}
