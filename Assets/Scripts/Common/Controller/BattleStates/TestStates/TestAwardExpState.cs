using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAwardExpState : BattleState
{   
    [SerializeField]TestSkirmishController TskirmishController;
    [SerializeField] Unit expRecipient;
    int amount;
    public override void Enable()
    {
        base.Enable();
        this.AddObserver(OnAwardExpFinished, NotificationBook.AWARD_EXP_FINISHED); 
        TskirmishController = FindObjectOfType<TestSkirmishController>();
        this.AddObserver(OnTestAwardEXP, "TEST_AWARD_EXP");
        //Init();
    }

    private void OnTestAwardEXP(object arg1, object xp)
    {
        amount = (int)xp;
        Init();
    }

    private void Init()
    {
        Skirmish skirmish = TskirmishController.TestGetSkirmish();
        expRecipient = skirmish.initiator;
        this.PostNotification(NotificationBook.AWARD_EXP_INIT, expRecipient);
        Debug.Log($"BEFORE:LVL{expRecipient.levelComponent.LVL}, xp:{expRecipient.levelComponent.EXP}");
        //I need a faction system next.  it'll make this clearer.
        // this actually works pretty well
       
        //tell levelComponent
        int exp = ExperienceManager.TestAwardExp(expRecipient, amount);
        //tell xpBar (levelUpComponent is told earlier in skirmishPlayViewController)
        // I think I may move the logic over to the skirmish view controller so i can switch between the levelupPane and the expBar more easily.
        this.PostNotification(NotificationBook.AWARD_EXP_SET_EXP, exp);
        Debug.Log($"after:LVL{expRecipient.levelComponent.LVL}, xp:{expRecipient.levelComponent.EXP}");
        this.PostNotification(NotificationBook.AWARD_EXP_START, exp);   
    }

    private void OnAwardExpFinished(object arg1, object arg2)
    {
        Debug.Log("Finished");
        owner.ChangeState<TestSkirmishInitState>();
    }

    
}
