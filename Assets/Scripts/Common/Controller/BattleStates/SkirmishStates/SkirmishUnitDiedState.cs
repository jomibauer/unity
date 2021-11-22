using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkirmishUnitDiedState : BattleState
{
    Unit deadUnit;
    public override void Enable()
    {
        base.Enable();
        deadUnit = skirmishController.GetDeadUnit();
        unitController.KillUnit(deadUnit);
        StartCoroutine(KillUnitAndChangeState());
    }

    IEnumerator KillUnitAndChangeState()
    {
        deadUnit = skirmishController.GetDeadUnit();
        unitController.KillUnit(deadUnit);
        yield return null;
        owner.ChangeState<AwardExpState>();
        
    }
}
