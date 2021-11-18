using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkirmishPlayState : BattleState
{
    List<Round> rounds;
    Unit initiator;
    Unit receiver;
    public override void Enable()
    {
        //Not starting this state?

        base.Enable();
        initiator = skirmishController.GetInitiator();
        receiver = skirmishController.GetReceiver();
        rounds = skirmishController.GetRounds();
        StartCoroutine(PlaySkirmish());
    }


    IEnumerator PlaySkirmish()
    {
        bool unitDied = false;
        foreach(var round in rounds)
        {
            Debug.Log("Round Start");
            var damage = 0;
            if(round.hit)
            {
                
                damage += round.damage;
                if(round.crit)
                {
                    damage *= 3;
                }
                if(round.actor == initiator)
                {
                    skirmishController.SetSkirmishHit();
                    receiver.HP -= damage;
                }
                else
                {
                    initiator.HP -= damage;
                }
            }
            else
            {
                //miss
            }
            //This is where we should Post a notification to the BattleAnimator with the turn data I think.
            //checking for death
            if(receiver.HP < 1 || initiator.HP < 1)
            {
                unitDied = true;
                
                break;
            }
            Debug.Log("Round");
            yield return new WaitForSecondsRealtime(1);
        }
        if (unitDied)
        {
            skirmishController.SetDeadUnit(receiver.HP < 1 ? receiver: initiator);
            skirmishController.SetSkirmishKill();
            owner.ChangeState<SkirmishUnitDiedState>();
        }
        else
        {
            owner.ChangeState<AwardExpState>();
        }
    }
}
