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
    public CameraRig cameraRig {get { return owner.cameraRig; }}
    public LevelData levelData {get { return owner.levelData; }}
    public GridCursor gridCursor {get { return owner.gridCursor; }}
    public Tile pos {get { return owner.pos; } set { owner.pos = value; }}
    public AbilityMenuPanelController abilityMenuPanelController { get { return owner.abilityMenuPanelController; }}
    public Turn turn { get { return owner.turn; }}
    public List<Unit> units { get { return owner.units; }}

    protected virtual void Awake()
    {
        owner = GetComponent<BattleController>();
        //gameController = GetComponent<GameController>();
    }

    protected override void AddObservers()
    {
        this.AddObserver(OnMove, "MOVE");
        this.AddObserver(OnConfirm, "CONFIRM");
        this.AddObserver(OnCancel, "CANCEL");
        this.AddObserver(OnInfo, "INFO");
        this.AddObserver(OnPause, "PAUSE");
    }

    protected override void RemoveObservers()
    {
        this.RemoveObserver(OnMove, "MOVE");
        this.RemoveObserver(OnConfirm, "CONFIRM");
        this.RemoveObserver(OnCancel, "CANCEL");
        this.RemoveObserver(OnInfo, "INFO");
        this.RemoveObserver(OnPause, "PAUSE");
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

}
