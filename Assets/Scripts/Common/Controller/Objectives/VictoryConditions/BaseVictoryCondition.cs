using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseVictoryCondition : MonoBehaviour
{
    protected BattleController bc;
    Factions victor = Factions.None;
    public Factions Victor
    {
        get { return victor; }
        protected set { victor = value; }
    }
    protected virtual void Awake()
    {
        bc = GetComponent<BattleController>();
    }

    protected virtual void OnEnable ()
    {
      this.AddObserver(OnHPDidChangeNotification, Stats.DidChangeNotification(StatTypes.HP));
      this.AddObserver(OnUnitDied, NotificationBook.UNIT_DIED);
    }

    protected virtual void OnUnitDied(object sender, object u)
    {
        CheckForGameOver((Unit)u);
    }

    protected virtual void CheckForGameOver(Unit u)
    {
        if(u.behavior.mustSurvive)
        {
            Victor = Factions.enemy;
        }
    }

    protected virtual void OnDisable ()
    {
      this.RemoveObserver(OnHPDidChangeNotification, Stats.DidChangeNotification(StatTypes.HP));
    }
    protected virtual void OnHPDidChangeNotification (object sender, object args)
    {
      
    }
    //adding an optional minHp argument here so its possible to end a battle before we kill a target, in the case we want to keep an enemy general around without killing them for story purposes.
    protected virtual bool IsDefeated (Unit unit, int minHp = 0)
    {     
      Stats stats = unit.GetComponent<Stats>();
      return stats[StatTypes.HP] == minHp;
    }

    protected virtual bool FactionIsDefeated(Factions type)
    {
        List<Unit> faction = bc.unitController.GetUnitsInFaction(type);
        for (int i=0; i < faction.Count; ++i)
        {
            if(!IsDefeated(faction[i]))
            {
                return false;
            }
        }
        return true;
    }
}
