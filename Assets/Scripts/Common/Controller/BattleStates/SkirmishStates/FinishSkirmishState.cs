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
        this.PostNotification(NotificationBook.HIDE_SKIRMISH_PLAY_VIEW);
        skirmishController.Clear();
        turn.Clear();

        yield return null;
        owner.ChangeState<CompleteTurnState>();
    }
}
