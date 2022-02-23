using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class BattleState : GameState
{
    protected BattleController owner;
    public UnitController unitController {get { return owner.unitController; }}
    public GridController gridController {get { return owner.gridController; }}
    public SkirmishController skirmishController {get { return owner.skirmishController;}}
    public CameraRig cameraRig {get { return owner.cameraRig; }}
    public LevelController levelController {get { return owner.levelController; }}
    public GridCursor gridCursor {get { return owner.gridCursor; }}
    public Tile pos {get { return owner.pos; } set { owner.pos = value; }}
    public AbilityMenuPanelController abilityMenuPanelController { get { return owner.abilityMenuPanelController; }}
    public MapMenuController mapMenuController { get { return owner.mapMenuController; }}
    public Turn turn { get { return owner.turn; }}
    //public List<Unit> units { get { return owner.units; }}

    protected virtual void Awake()
    {
        owner = GetComponent<BattleController>();
        //gameController = GetComponent<GameController>();
    }

    protected override void AddObservers()
    {
        this.AddObserver(OnMove, NotificationBook.MOVE);
        this.AddObserver(OnConfirm, NotificationBook.CONFIRM);
        this.AddObserver(OnCancel, NotificationBook.CANCEL);
        this.AddObserver(OnInfo, NotificationBook.INFO);
        this.AddObserver(OnPause, NotificationBook.PAUSE);
    }

    protected override void RemoveObservers()
    {
        this.RemoveObserver(OnMove, NotificationBook.MOVE);
        this.RemoveObserver(OnConfirm, NotificationBook.CONFIRM);
        this.RemoveObserver(OnCancel, NotificationBook.CANCEL);
        this.RemoveObserver(OnInfo, NotificationBook.INFO);
        this.RemoveObserver(OnPause, NotificationBook.PAUSE);
    }

    protected virtual void OnMove(object sender, object arg)
    {

    }

    protected virtual void OnConfirm(object sender, object arg)
    {

    }

    protected virtual void OnCancel(object sender, object arg)
    {

    }

    protected virtual void OnInfo(object sender, object arg)
    {

    }

    protected virtual void OnPause(object sender, object arg)
    {

    }

    protected virtual void SelectTile(Tile tile)
    {
        if (pos == tile || !gridController.CheckTile(tile)) { return; }

        pos = tile;
        gridCursor.MoveTo(gridController.GetWorldPositionFromTile(pos));
    }

    protected virtual void TeleportSelectTile(Tile tile)
    {
        if (pos == tile || !gridController.CheckTile(tile)) { return; }

        pos = tile;
        gridCursor.TeleportCursor(pos);
    }

    protected virtual IEnumerator NotImplementedSoSwapToMoveTarget()
    {
        yield return new WaitForSeconds(3);
        owner.ChangeState<MoveTargetState>();
    }

}
