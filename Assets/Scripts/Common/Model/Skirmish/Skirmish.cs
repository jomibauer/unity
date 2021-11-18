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

    public SkirmishStatSet(int hit, int dam, int crit, int turns, Unit unit)
    {
        this.hit = hit;
        this.dam = dam;
        this.crit = crit;
        this.turns = turns;
        this.HP = unit.HP;
        this.MaxHP = unit.stats[StatTypes.MHP];
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

    public int CalcHit(Unit initiator, Unit receiver)
    {
        return (initiator.stats[StatTypes.SKL] * 10) + initiator.stats[StatTypes.LCK];
    }
    public int CalcDam(Unit initiator, Unit receiver)
    {
        var calculatedDamage = initiator.stats[StatTypes.STR] - receiver.stats[StatTypes.DEF];
        return  calculatedDamage >= 0 ? calculatedDamage : 0;
    }
    public int CalcCrit(Unit initiator, Unit receiver)
    {
        return initiator.stats[StatTypes.LCK] * 2;
    }

    public int CalcTurns(Unit initiator, Unit receiver)
    {
        var calculatedSpeed = initiator.stats[StatTypes.SPD] - receiver.stats[StatTypes.SPD]/2;
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
