using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SkirmishStatSet
{
    public int hit;
    public int dam;
    public int crit;
    public int turns;
    public int HP;
    public int MaxHP;
    public string unit_name;
    public Factions faction;

    public SkirmishStatSet(int hit, int dam, int crit, int turns, Unit unit)
    {
        this.hit = hit;
        this.dam = dam;
        this.crit = crit;
        this.turns = turns;
        this.HP = unit.HP;
        this.MaxHP = unit.stats[StatTypes.MHP];
        this.unit_name = unit.unit_name;
        this.faction = unit.GetFaction();
    }

}
public class Skirmish
{
    
    public Unit initiator;
    public SkirmishStatSet initiatorStats; 
    
    public Unit receiver;
    public SkirmishStatSet receiverStats; 

    public int range;

    #region EXP
    //Weapon, friendly, or statusStaff
    public string skirmishType;
    public bool didHit;
    public bool didKill;
    #endregion

    //Type can be "MELEE" so far
    public Skirmish(Unit initiator, Unit receiver, string skirmishType)
    {
        
        this.initiator = initiator;
        this.receiver = receiver;
        this.range = DistanceBetween(initiator.currentTile, receiver.currentTile);
        CalcSkirmishStats(initiator, receiver);
        this.skirmishType = skirmishType;

    }

    public int DistanceBetween(Tile tileA, Tile tileB)
    {
        return (tileA.x - tileB.x) + (tileA.y - tileB.y);
    }
    public void CalcSkirmishStats(Unit initiator, Unit receiver)
    {
        
        Debug.Log($"[Skirmish.cs]: skirmish range is {range}");
        initiatorStats = new SkirmishStatSet
            (CalcHit(initiator, receiver)
            , CalcDam(initiator, receiver)
            , CalcCrit(initiator, receiver)
            , CalcTurns(initiator, receiver)
            , initiator);
        //if receiver is in range
        if(UnitInRange(receiver))
        {
            receiverStats = new SkirmishStatSet
            (CalcHit(receiver, initiator)
            , CalcDam(receiver, initiator)
            , CalcCrit(receiver, initiator)
            , CalcTurns(receiver, initiator)
            , receiver);
        }
        else
        {
            Debug.Log("[Skirmish.cs]: reciever not in range!");
            receiverStats = new SkirmishStatSet
                (0
                , 0
                , 0
                , 0
                , receiver);
        }
    }
    public int CalcDam(Unit initiator, Unit receiver)
    {
        var calculatedDamage = 0;
        if(initiator.weapon.type == WeaponTypes.song || initiator.weapon.type == WeaponTypes.poem)
        {
            calculatedDamage = GetIntDamage(initiator, receiver);
        }
        else {
            calculatedDamage = GetStrDamage(initiator, receiver);
        }
        return  calculatedDamage >= 0 ? calculatedDamage : 0;
    }

    private int GetStrDamage(Unit initiator, Unit receiver)
    {
        return initiator.stats[StatTypes.STR] + initiator.GetWeaponDamage() - receiver.stats[StatTypes.DEF];
    }

    private int GetIntDamage(Unit initiator, Unit receiver)
    {
        return initiator.stats[StatTypes.INT] + initiator.GetWeaponDamage() - receiver.stats[StatTypes.RES];
    }

    public int CalcHit(Unit initiator, Unit receiver)
    {
        var weaponWeightPenalty = initiator.stats[StatTypes.CON] - initiator.GetWeaponWeight() >= 0 ? 0 : initiator.stats[StatTypes.CON] - initiator.GetWeaponWeight() * -1;
        return (initiator.stats[StatTypes.SKL] * 10) + initiator.stats[StatTypes.LCK] - weaponWeightPenalty;
    }
    
    public int CalcCrit(Unit initiator, Unit receiver)
    {
        return initiator.stats[StatTypes.LCK] + initiator.GetWeaponCrit();
    }

    public int CalcTurns(Unit initiator, Unit receiver)
    {
        var weaponWeightPenalty = initiator.stats[StatTypes.CON] - initiator.GetWeaponWeight() >= 0 ? 0 : initiator.stats[StatTypes.CON] - initiator.GetWeaponWeight() * -1;
        var calculatedSpeed = (initiator.stats[StatTypes.SPD] - weaponWeightPenalty) - receiver.stats[StatTypes.SPD]/2;
        if(calculatedSpeed <= 1.7)
        {
            return 1;
        } else {
            return 2;
        }
    }

    public bool UnitInRange(Unit unit)
    {
        int[] attackRange = unit.weapon.attackRange;
        return attackRange[attackRange.Length-1] >= range;
    }

    public void GetExpModifiers()
    {

    }
}
