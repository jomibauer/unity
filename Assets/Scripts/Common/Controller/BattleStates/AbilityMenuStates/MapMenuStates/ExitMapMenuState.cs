using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMapMenuState : BaseMenuState
{
    public override void Enable()
    {
        base.Enable();
        StartCoroutine(BackToMoveTarget());
    }

    private IEnumerator BackToMoveTarget()
    {
        gridCursor.EnableSprite();
        mapMenuController.SetLastSelection(-1);
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }

    public override void Disable()
    {
        base.Disable();
        mapMenuController.HideMenu();
    }
    protected override void Cancel()
    {
        throw new System.NotImplementedException();
    }

    protected override void Confirm()
    {
        throw new System.NotImplementedException();
    }

    protected override void LoadMenu()
    {
        throw new System.NotImplementedException();
    }
    
}