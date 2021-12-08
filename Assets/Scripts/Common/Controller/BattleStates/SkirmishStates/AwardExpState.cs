using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardExpState : BattleState
{
    public override void Enable()
    {
        base.Enable();
        AwardExp();
        this.AddObserver(OnAwardExpFinished, NotificationBook.AWARD_EXP_FINISHED);   
    }

    private void OnAwardExpFinished(object arg1, object arg2)
    {
        Debug.Log("Finished");
        owner.ChangeState<FinishSkirmishState>();
    }

    void AwardExp()
    {
        //yield return null;
        
        Skirmish skirmish = skirmishController.GetSkirmish();
        Unit expRecipient;
        Unit otherUnit;
        //I need a faction system next.  it'll make this clearer.
        // this actually works pretty well
        if (unitController.GetSelectedUnit() != null)
        {
            expRecipient = skirmish.initiator;
            otherUnit = skirmish.receiver;
            //ExperienceManager.AwardExp(skirmish, skirmish.initiator, skirmish.receiver);
        }else{
            expRecipient = skirmish.initiator;
            otherUnit = skirmish.receiver;
            //ExperienceManager.AwardExp(skirmish, skirmish.receiver, skirmish.initiator);
        }

        int exp = ExperienceManager.AwardExp(skirmish, expRecipient, otherUnit);
        this.PostNotification(NotificationBook.AWARD_EXP_START, exp);
        
        Debug.LogWarning("EXP AWARDED");
    }
}
