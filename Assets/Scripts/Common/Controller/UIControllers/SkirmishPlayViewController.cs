using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkirmishPlayViewController : MonoBehaviour
{
    [SerializeField] public SkirmishBattlerPane rightPane;
    [SerializeField] public SkirmishBattlerPane leftPane;
    [SerializeField] public ExpBarPane expPane;
    [SerializeField] public LevelUpPane levelUpPane;
    [SerializeField] GameObject canvas;
    int remainingEXP;

    void Start()
    {
        rightPane.SetPosition("Hide", false);
        leftPane.SetPosition("Hide", false);
        canvas.SetActive(false);
        //Add observers here for talking to the skirmish states.  
        this.AddObserver(OnShowSkirmishPlayView, NotificationBook.SHOW_SKIRMISH_PLAY_VIEW);
        this.AddObserver(OnHideSkirmishPlayView, NotificationBook.HIDE_SKIRMISH_PLAY_VIEW);

        this.AddObserver(OnRightPaneHealthChange, NotificationBook.RIGHT_PANE_HEALTH_CHANGE);
        this.AddObserver(OnLeftPaneHealthChange, NotificationBook.LEFT_PANE_HEALTH_CHANGE);
        this.AddObserver(OnHealthChangeFinished, NotificationBook.HEALTH_CHANGE_FINISHED);

        this.AddObserver(OnSkirmishStart, NotificationBook.SKIRMISH_START);

    }

    private void OnHealthChangeFinished(object sender, object n_u)
    {
        this.PostNotification(NotificationBook.PLAY_NEXT_ROUND);
    }

    private void OnSkirmishStart(object sender, object n_u)
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
        StartCoroutine(ShowPanes());
        
        
    }
    private void OnHideSkirmishPlayView(object sender, object n_u)
    {
        //this is clearing the pane info and resetting it before the panes are hidden.  Need to figure out how to 
        StartCoroutine(HidePanes());

        rightPane.enabled = false;
        leftPane.enabled = false;
    }

    private IEnumerator ShowPanes()
    {
        canvas.SetActive(true);
        Tweener t1 = leftPane.SetPosition("Show");
        Tweener t2 = rightPane.SetPosition("Show");
        t1.easingControl.equation = EasingEquations.EaseInOutBack;
        t2.easingControl.equation = EasingEquations.EaseInOutBack;
        yield return new WaitForSeconds(2);
        this.PostNotification(NotificationBook.SKIRMISH_SETUP_COMPLETE);
    }

    private IEnumerator HidePanes()
    {
        Tweener t1 = leftPane.SetPosition("Hide");
        Tweener t2 = rightPane.SetPosition("Hide");
        t1.easingControl.equation = EasingEquations.EaseInOutBack;
        t2.easingControl.equation = EasingEquations.EaseInOutBack;
        yield return new WaitForSeconds(2);
        //I'm clearing the panes inside the coroutine so the panes have to wait til the animation finishes to remove the info.  This way, the panes don't change back to having no info til
        // they're offscreen and the player cant see them.
        rightPane.Clear();
        leftPane.Clear();
        canvas.SetActive(false);
    }

    private void OnLeftPaneHealthChange(object sender, object am)
    {
        //Debug.Log("[SkirmishPlayView.cs]: HealthChange");
        int amount = (am as int[])[0];

        if(amount <= 0)
        {
            //Debug.Log("[SkirmishPlayView.cs]: Damage");
            DamagePaneBy(leftPane, Mathf.Abs(amount));
        }
        else 
        {
            HealPaneBy(leftPane, amount);
        }
    }

    private void OnRightPaneHealthChange(object sender, object am)
    {
        //Debug.Log("[SkirmishPlayView.cs]: HealthChange");
        int amount = (am as int[])[0];

        if(amount <= 0)
        {
            //Debug.Log("[SkirmishPlayView.cs]: Damage");
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
