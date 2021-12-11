using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardExpState : BattleState
{
    public override void Enable()
    {
        base.Enable();
        this.AddObserver(OnAwardExpFinished, NotificationBook.AWARD_EXP_FINISHED); 

        Init();
    }

    private void Init()
    {
        Skirmish skirmish = skirmishController.GetSkirmish();
        Unit expRecipient;
        Unit otherUnit;
        //I need a faction system next.  it'll make this clearer.
        // this actually works pretty well
        if (unitController.GetSelectedUnit() != null)
        {
            expRecipient = skirmish.initiator;
            otherUnit = skirmish.receiver;
        }else{
            expRecipient = skirmish.initiator;
            otherUnit = skirmish.receiver;
        }
        
        this.PostNotification(NotificationBook.AWARD_EXP_INIT, expRecipient);
        //tell levelComponent & modify stats on the actual unit
        int exp = ExperienceManager.AwardExp(skirmish, expRecipient, otherUnit);

        this.PostNotification(NotificationBook.AWARD_EXP_START, exp);
    }

    private void OnAwardExpFinished(object sender, object n_u)
    {
        Debug.Log("Finished");
        owner.ChangeState<FinishSkirmishState>();
    }
}
