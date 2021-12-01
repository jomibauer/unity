using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityMenuState : BattleState
{
    protected string menuTitle;
    protected List<string> menuOptions;
    public override void Enable ()
    {
        base.Enable ();
        SelectTile(turn.actor.targetTile);
        LoadMenu();
        this.PostNotification(NotificationBook.INPUT_ON);
    }
    public override void Disable ()
    {
        base.Disable ();
        abilityMenuPanelController.Hide();
    }
    protected override void OnConfirm (object sender, object e)
    {
        Confirm();
    }
    protected override void OnMove (object sender, object t)
    {
        Tile tile = (Tile)t;
        if (tile.x > 0 || tile.y < 0)
        abilityMenuPanelController.Next();
        else
        abilityMenuPanelController.Previous();
    }

    protected override void OnCancel (object sender, object e)
    {
        Cancel();
    }
    protected abstract void LoadMenu ();
    protected abstract void Confirm ();
    protected abstract void Cancel ();
}
