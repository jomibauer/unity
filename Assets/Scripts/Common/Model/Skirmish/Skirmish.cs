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

    public Skirmish(Unit initiator, Unit receiver, string skirmishType)
    {
        
        this.initiator = initiator;
        this.receiver = receiver;
        CalcSkirmishStats(initiator, receiver);
        this.range = DistanceBetween(initiator.currentTile, receiver.currentTile);
        this.skirmishType = skirmishType;

    }

    public int DistanceBetween(Tile tileA, Tile tileB)
    {
        return (tileA.x - tileB.x) + (tileA.y - tileB.y);
    }

    // need to add another check for receiver range to this data.  This checks if they have the right weapon equipped to attack back.
    // I think these calculations will all need to add an extra arg spot for weapons in order to do this right.  I should check here 
    // for whether units can attack and set turns=0 so I dont have to worry later about changing the logic to not generate rounds 
    // later in the skirmish controller.
    public void CalcSkirmishStats(Unit initiator, Unit receiver)
    {

        initiatorStats = new SkirmishStatSet
            (CalcHit(initiator, receiver)
            , CalcDam(initiator, receiver)
            , CalcCrit(initiator, receiver)
            , CalcTurns(initiator, receiver)
            , initiator);
        //if receiver is in range
        receiverStats = new SkirmishStatSet
            (CalcHit(receiver, initiator)
            , CalcDam(receiver, initiator)
            , CalcCrit(receiver, initiator)
            , CalcTurns(receiver, initiator)
            , receiver);
    }
    public int CalcDam(Unit initiator, Unit receiver)
    {
        var calculatedDamage = (initiator.stats[StatTypes.STR] + initiator.GetWeaponDamage()) - receiver.stats[StatTypes.DEF];
        return  calculatedDamage >= 0 ? calculatedDamage : 0;
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

    public void GetExpModifiers()
    {

    }
}
