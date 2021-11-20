using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSkirmishState : BattleState
{
    // Start is called before the first frame update
    public override void Enable()
    {
        base.Enable();
        StartCoroutine(FinishSkirmish());
    }

    IEnumerator FinishSkirmish()
    {
        skirmishController.Clear();
        turn.Clear();
        unitController.DeactivateUnit(unitController.GetSelectedUnit());
        unitController.DeselectUnit();
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }
}
