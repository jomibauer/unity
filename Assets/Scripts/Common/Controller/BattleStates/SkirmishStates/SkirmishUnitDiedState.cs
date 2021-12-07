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
        StartCoroutine(KillUnitAndChangeState());
    }

    IEnumerator KillUnitAndChangeState()
    {

        unitController.KillUnit(deadUnit);
        yield return null;
        owner.ChangeState<AwardExpState>();
        
    }
}
