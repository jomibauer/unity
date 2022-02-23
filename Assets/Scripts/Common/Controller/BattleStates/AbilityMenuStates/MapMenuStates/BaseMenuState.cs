using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenuState : BattleState
{
    protected List<string> menuOptions;
    public BaseMenuController controller;
    
    public override void Enable ()
    {
        base.Enable ();

        this.PostNotification(NotificationBook.INPUT_ON);
    }
    public override void Disable ()
    {
        base.Disable ();
    }
    protected override void OnConfirm (object sender, object e)
    {
        Confirm();
    }
    protected override void OnMove (object sender, object t)
    {
        Tile tile = (Tile)t;
        if (tile.y < 0)
        controller.Next();
        else if (tile.y > 0)
        controller.Previous();
    }

    protected override void OnCancel (object sender, object e)
    {
        Cancel();
    }
    protected abstract void LoadMenu ();
    protected abstract void Confirm ();
    protected abstract void Cancel ();
}
