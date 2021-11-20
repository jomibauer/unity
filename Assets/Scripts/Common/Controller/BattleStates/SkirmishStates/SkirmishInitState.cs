using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkirmishInitState : BattleState
{
    public override void Enable()
    {
        base.Enable();
        if (skirmishController.GetSkirmish() == null) { throw new Exception("No skirmish on the skirmishController when trying to initialize skirmish."); }
        skirmishController.InitRounds();
        this.PostNotification(NotificationBook.HIDE_SKIRMISH_PREVIEW_PANE, null);
        StartCoroutine(StartSkirmish());
        //owner.ChangeState<MoveTargetState>();
    }

    IEnumerator StartSkirmish()
    {
        yield return null;
        //Set up animation here?
        owner.ChangeState<SkirmishPlayState>();
    }
}
