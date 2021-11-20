using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardExpState : BattleState
{
    //Dont think I should do this with listeners.  IDK how I would time when the notifications got posted.  Maybe I need some persistent gameobject that tracks skirmish data
    //(EXP Manager? could need a skirmish controller), then this state just polls that object and then displays the exp growth anim, then transistions back to move target or 
    //to level up.
    public override void Enable()
    {
        base.Enable();
        StartCoroutine(AwardAndExit());   
    }

    IEnumerator AwardAndExit()
    {
        yield return null;
        Debug.LogWarning("EXP AWARDED");
        unitController.RefreshUnits();

        owner.ChangeState<FinishSkirmishState>();
    }

    

}
