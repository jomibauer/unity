using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelectionState : BattleState
{
    Skirmish skirmish;
    List<Tile> attackRange;
    public int selection { get; private set; }

    public override void Enable()
    {
        base.Enable();
        gridCursor.EnableSprite();
        attackRange = gridController.GetAttackRangeAndDraw(unitController.GetSelectedUnit());
        selection = 0;
        Next();
    }

    public override void Disable()
    {
        base.Disable();
        gridController.ClearPathTilemapAndPathfinding();
        gridCursor.DisableSprite();
    }
    protected override void OnConfirm(object sender, object arg)
    {
        if (skirmish == null) { throw new Exception("SkirmishInit with null skirmish.  did you add a skirmish to the skirmish controller?"); }
        owner.ChangeState<SkirmishInitState>();
    }

    protected override void OnCancel(object sender, object arg)
    {
        this.PostNotification(NotificationBook.HIDE_SKIRMISH_PREVIEW_PANE);
        owner.ChangeState<WeaponSelectionState>();
    }
    
    protected override void OnMove(object sender, object t)
    {
        Tile tile = (Tile)t;
        //Debug.Log(tile);
        if (tile.x > 0 || tile.y < 0)
        {
            Next();
        }
        else
        {
            Previous();
        }
    }

    public void Next ()
    {
        for (int i = selection + 1; i < selection + attackRange.Count; ++i)
        {
            
            int index = i % attackRange.Count;

            if(EnemyInTile(attackRange[index]))
            { 
                selection = index;
                skirmishController.InitNewSkirmish(unitController.GetSelectedUnit(), unitController.GetUnitAt(attackRange[index]), "MELEE");
                skirmish = skirmishController.GetSkirmish();
                Debug.Log($"Selecting Target: {skirmish.initiator} vs {skirmish.receiver}");
                TeleportSelectTile(attackRange[index]);
                GetSkirmishPreview();
                break; 
            }
        }
    }

    public void Previous ()
    {
        for (int i = selection - 1 + attackRange.Count; i > selection; --i)
        {
            int index = i % attackRange.Count;
            if(EnemyInTile(attackRange[index]))
            { 
                selection = index;
                skirmishController.InitNewSkirmish(unitController.GetSelectedUnit(), unitController.GetUnitAt(attackRange[index]), "MELEE");
                skirmish = skirmishController.GetSkirmish();
                TeleportSelectTile(attackRange[index]);
                GetSkirmishPreview();
                break;  
            }
        }
    }

    bool EnemyInTile(Tile tile)
    {
        return unitController.GetUnitAt(tile) != null;
    }

    internal void GetSkirmishPreview()
    {
        this.PostNotification(NotificationBook.SKIRMISH_PANE_POPULATE, skirmish);
        //Something here to figure out where the pane should go.  I don't think it matters now, because the camera has no boundaries on it.
        //eventually when it gets to the point where I limit the camera to the gridmap, I'll need to do some stuff here to figure out the
        // selected unit's position relative to the camera.
        this.PostNotification(NotificationBook.SHOW_SKIRMISH_PREVIEW_PANE, "ShowLeft");
    }
}
