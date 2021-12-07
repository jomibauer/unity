using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardExpState : BattleState
{
    public override void Enable()
    {
        base.Enable();
        StartCoroutine(AwardAndExit());   
    }

    IEnumerator AwardAndExit()
    {
        yield return null;
        
        Skirmish skirmish = skirmishController.GetSkirmish();
        //I need a faction system next.  it'll make this clearer.
        if (unitController.GetSelectedUnit() != null)
        {
            ExperienceManager.AwardExp(skirmish, skirmish.initiator, skirmish.receiver);
        }else{
            ExperienceManager.AwardExp(skirmish, skirmish.receiver, skirmish.initiator);
        }
        
        Debug.LogWarning("EXP AWARDED");


        owner.ChangeState<FinishSkirmishState>();
    }

    

}
