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
        owner.ChangeState<AwardExpState>();
    }
}
