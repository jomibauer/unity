using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : StateMachine
{
    public UnitController unitController;
    public GridController gridController;
    public SkirmishController skirmishController;
    public CameraRig cameraRig;
    public Tile pos;
    public GridCursor gridCursor;
    public LevelData levelData;
    public AbilityMenuPanelController abilityMenuPanelController;
    public Turn turn = new Turn();
    //commenting this out to see if we can get away with it.  A list of every unit is available in the unitController, so id prefer if we just use that list if possible.
    //public List<Unit> units;

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
        this.PostNotification(NotificationBook.DEBUG_ARRAY, ls);
    }
}