using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkirmishPlayViewController : MonoBehaviour
{
    [SerializeField] public SkirmishBattlerPane rightPane;
    [SerializeField] public SkirmishBattlerPane leftPane;


    void Start()
    {
        //Add observers here for the start and end of a skirmish.  Maybe look into SKIRMISH START, might need to add another.
        this.AddObserver(OnShowSkirmishPlayView, NotificationBook.SHOW_SKIRMISH_PLAY_VIEW);
        this.AddObserver(OnHideSkirmishPlayView, NotificationBook.HIDE_SKIRMISH_PLAY_VIEW);

        this.AddObserver(OnRightPaneHealthChange, NotificationBook.RIGHT_PANE_HEALTH_CHANGE);
        this.AddObserver(OnLeftPaneHealthChange, NotificationBook.LEFT_PANE_HEALTH_CHANGE);
        this.AddObserver(OnHealthChangeFinished, NotificationBook.HEALTH_CHANGE_FINISHED);

        this.AddObserver(OnSkirmishStart, NotificationBook.SKIRMISH_START);
        rightPane.SetPosition("Hide");
        leftPane.SetPosition("Hide");
    }

    private void OnHealthChangeFinished(object arg1, object arg2)
    {
        this.PostNotification(NotificationBook.PLAY_NEXT_ROUND);
    }

    private void OnSkirmishStart(object arg1, object arg2)
    {
        this.PostNotification(NotificationBook.PLAY_NEXT_ROUND);
    }

    private void OnShowSkirmishPlayView(object sender, object sk)
    {
        Skirmish skirmish = sk as Skirmish;
        rightPane.enabled = true;
        leftPane.enabled = true;

        // Right now the initiator is always on the right but I will want to change this when I implement factions so that the player is on the right always, 
        // or if there is a situation in which a faction is acting on a friendly unit, then the initiator should always be on the right. 
        rightPane.Load(skirmish.initiatorStats);
        leftPane.Load(skirmish.receiverStats);
        ShowPanes();
        
    }
    private void OnHideSkirmishPlayView(object sender, object n_u)
    {
        //this is clearing the pane info and resetting it before the panes are hidden.  Need to figure out how to 
        HidePanes();
        rightPane.Clear();
        leftPane.Clear();
        rightPane.enabled = false;
        leftPane.enabled = false;
    }

    //I should make the show and hide functions coroutines.
    private void ShowPanes()
    {
        Tweener t1 = leftPane.SetPosition("Show");
        Tweener t2 = rightPane.SetPosition("Show");
        t1.easingControl.equation = EasingEquations.EaseInOutBack;
        t2.easingControl.equation = EasingEquations.EaseInOutBack;
    }

    private void HidePanes()
    {
        Tweener t1 = leftPane.SetPosition("Hide");
        Tweener t2 = rightPane.SetPosition("Hide");
        t1.easingControl.equation = EasingEquations.EaseInOutBack;
        t2.easingControl.equation = EasingEquations.EaseInOutBack;
    }

    private void OnLeftPaneHealthChange(object sender, object am)
    {
        Debug.Log("SkirmishPlayView: HealthChange");
        int amount = (am as int[])[0];

        if(amount <= 0)
        {
            Debug.Log("SkirmishPlayView: Damage");
            DamagePaneBy(leftPane, Mathf.Abs(amount));
        }
        else 
        {
            HealPaneBy(leftPane, amount);
        }
    }

    private void OnRightPaneHealthChange(object sender, object am)
    {
        Debug.Log("SkirmishPlayView: HealthChange");
        int amount = (am as int[])[0];

        if(amount <= 0)
        {
            Debug.Log("SkirmishPlayView: Damage");
            DamagePaneBy(rightPane, Mathf.Abs(amount));
        }
        else 
        {
            HealPaneBy(rightPane, amount);
        }
    }

    private void DamagePaneBy(SkirmishBattlerPane pane, int amount)
    {
        StartCoroutine(pane.DamageBy(amount));
    }

    private void HealPaneBy(SkirmishBattlerPane pane, int amount)
    {
        StartCoroutine(pane.HealBy(amount));
    }
}
