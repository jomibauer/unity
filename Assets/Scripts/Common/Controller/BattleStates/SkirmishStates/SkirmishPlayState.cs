using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkirmishPlayState : BattleState
{
    List<Round> rounds;
    bool unitDied;
    Unit initiator;
    Unit receiver;

    /* public override void Enable()
    {

        base.Enable();
        initiator = skirmishController.GetInitiator();
        receiver = skirmishController.GetReceiver();
        rounds = skirmishController.GetRounds();
        StartCoroutine(PlaySkirmish());
    } */

    public override void Enable()
    {
        base.Enable();
        unitDied = false;
        initiator = skirmishController.GetInitiator();
        receiver = skirmishController.GetReceiver();
        rounds = skirmishController.GetRounds();
        this.AddObserver(OnPlayNextRound, NotificationBook.PLAY_NEXT_ROUND);
        this.PostNotification(NotificationBook.SKIRMISH_START);
    }

    private void OnPlayNextRound(object arg1, object arg2)
    {
        Debug.Log("playing next round");
        PlaySkirmishNotify();
    }

    void PlaySkirmishNotify()
    {
        Debug.Log($"Rounds: {rounds.Count}");
        if(rounds.Count == 0) 
        { 
            Debug.Log("SkirmishFinished");
            if (unitDied)
            {
                skirmishController.SetDeadUnit(receiver.HP < 1 ? receiver: initiator);
                skirmishController.SetSkirmishKill();
                owner.ChangeState<SkirmishUnitDiedState>();
                return;
            }
            else
            {
                Debug.Log("ChangingState");
                owner.ChangeState<AwardExpState>();
                return;
            }
        }
        Round round = rounds.Pop(0);

        var damage = 0;
        if(round.hit)
        {
            Debug.Log("hit");
            damage += round.damage;
            if(round.crit)
            {
                damage *= 3;
            }
            if(round.actor == initiator)
            {
                skirmishController.SetSkirmishHit();
                this.PostNotification(NotificationBook.LEFT_PANE_HEALTH_CHANGE, new int[]{damage * -1});
                receiver.HP -= damage;
            }
            else
            {
                this.PostNotification(NotificationBook.RIGHT_PANE_HEALTH_CHANGE, new int[]{damage * -1});
                initiator.HP -= damage;
            }
        }
        else
        {
            //miss
            Debug.Log("miss");
            PlaySkirmishNotify();
        }
        //This is where we should Post a notification to the BattleAnimator with the turn data I think.
        //checking for death
        if(receiver.HP < 1 || initiator.HP < 1)
        {
            unitDied = true;
        }


        return;
    }

    IEnumerator PlaySkirmish()
    {
        bool unitDied = false;
        foreach(var round in rounds)
        {

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
                    this.PostNotification(NotificationBook.LEFT_PANE_HEALTH_CHANGE, new int[]{damage * -1});
                    receiver.HP -= damage;
                }
                else
                {
                    this.PostNotification(NotificationBook.RIGHT_PANE_HEALTH_CHANGE, new int[]{damage * -1});
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
