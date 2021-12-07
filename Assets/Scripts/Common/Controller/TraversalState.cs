using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversalState : BattleState
{
    public override void Enable()
    {
        base.Enable();
        this.AddObserver(OnFinishedMoving, NotificationBook.FINISHED_MOVING);
        unitController.ToggleUnitMover();
    }

    private void OnFinishedMoving(object arg1, object arg2)
    {
        unitController.ToggleUnitMover();
        owner.ChangeState<CommandSelectionState>();
    }

}