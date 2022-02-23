using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPlayerPhaseState : BattleState
{
    public override void Enable()
    {
        base.Enable();
        StartCoroutine(ResetUnitsAndChangeState());
    }

    private IEnumerator ResetUnitsAndChangeState()
    {
        yield return null;
        turn.Clear();
        unitController.ResetUnitsOnFaction(Factions.player);
        owner.ChangeState<EnemyPhaseState>();
    }
}
