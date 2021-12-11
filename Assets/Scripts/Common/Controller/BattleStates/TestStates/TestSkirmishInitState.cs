using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkirmishInitState : BattleState
{
    [SerializeField]TestSkirmishController TskirmishController;
    public override void Enable()
    {
        base.Enable();
        TskirmishController = FindObjectOfType<TestSkirmishController>();
        //if (skirmishController.GetSkirmish() == null) { throw new Exception("No skirmish on the skirmishController when trying to initialize skirmish."); }
        //skirmishController.InitRounds();
        this.PostNotification(NotificationBook.HIDE_SKIRMISH_PREVIEW_PANE, null);
        this.PostNotification(NotificationBook.SHOW_SKIRMISH_PLAY_VIEW, TskirmishController.TestGetSkirmish());
        
        //owner.ChangeState<MoveTargetState>();
    }

    private void OnSkirmishReady(object arg1, object arg2)
    {
        StartCoroutine(StartSkirmish());
    }

    IEnumerator StartSkirmish()
    {
        yield return null;
        //Set up animation here?
        owner.ChangeState<SkirmishPlayState>();
    }
}