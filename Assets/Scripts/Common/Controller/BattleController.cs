using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : StateMachine
{
    public UnitController unitController;
    public GridController gridController;
    public CameraRig cameraRig;
    public Tile pos;
    public GridCursor gridCursor;
    public LevelData levelData;
    public AbilityMenuPanelController abilityMenuPanelController;
    public Turn turn = new Turn();
    public List<Unit> units;
    

    void Start()
    {

        ChangeState<InitBattleState>();
    } 

    void Update()
    {
        if(unitController.GetSelectedUnit())
        {
            UIPanelUpdate();
        }
    }

    void UIPanelUpdate()
    {
        Unit unit = unitController.GetSelectedUnit();
        List<string> ls = new List<string>();
        ls.Add(unit.ToString());
        ls.Add($"TurnInfo: moved:{turn.hasUnitMoved}, acted:{turn.hasUnitActed}");
        ls.Add($"UnitInfo: moved:{unit.hasUnitMoved}, acted:{unit.hasUnitActed}");
        this.PostNotification("UNIT_MOVEMENT", ls);
    }
}