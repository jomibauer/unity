using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExperienceManager
{

  //I think I can do all the calculations with the valueChangeException.  Here's the formula we'll try using first:
  /* 
    WEAPON ATTACKING:
    base = 1
    tookNoDamage(){ base += 2 }
    rankDeltaIs1(){ base += 2 }
    killPot = 15 + (-5 * rankDelta)
    ifBoss: killPot += 25
    atkExp = 10 + deltaLevel/2 (round up)
    killExp = atkExp + deltaLevel + killPot
    ------------
    awardedExp = base + attackExp + killExp 
    
    STAVES & SMOKING:
    Different equation as the units involved are friendly to each other & wont be killing or damaging.
    multValueMod applied based on weapon rank.
    base = 9
    E - x1
    D - x1.2
    C - x1.4
    B - x1.8
    A - x2
    S - x2.5
    ----------
    awardedExp = base x multMod[weaponRank]

    Status staves exception:
    base = 1;
    hitBonus = 17;
    ifBoss(){hitBonus += 7};
    ifHit(){ (base += hitBonus) x multMod[weaponRank] }
    ----------
    awardedExp = base;

    RANKS:
    rank 0: unpromoted.  listed level and expLevel are the same
    rank 1: promoted.  exp level = 20 + ListedLevel.

  */
  public static int TestAwardExp(Unit recipient, int amount)
  {
    recipient.levelComponent.EXP += amount;
    return amount;

  }
  public static int AwardExp(Skirmish skirmish, Unit player, Unit opponent)
  {
    if (skirmish == null) { throw new System.Exception("Tried to award exp, but there was no skirmish available in the ExperienceManager."); }
    //only implemented weapon attack so far.
    int exp = 1;

    int rankDelta = opponent.levelComponent.RNK - player.levelComponent.RNK;
    int deltaLevel = (opponent.levelComponent.RNK * 20 + opponent.levelComponent.LVL) - (player.levelComponent.RNK * 20 + player.levelComponent.LVL);

    if(rankDelta > 0) { exp += rankDelta * 2; }
                  //int tookNoDamage(){ base += 2 }

    int killPot = 15 + (5 * rankDelta);
                  //if(opponent.isBoss){ killPot += 25; }
    int atkExp = 10 + deltaLevel/2;
    int killExp = atkExp + deltaLevel + killPot;
    //=========================================
    //I think the calculations happen here.  I think I can post a skirmish to this manager, or assign a skirmish then post that I want to 
    //award exp, then modify player.levelComponent.EXP directly.  If my understanding is correct, then when the stat changes, the Stats class
    //posts a notification to the level component, which applies the floor and ceiling for me.

    //I dont know how the level component only listens to when it changes and not when everybody's changes.  Maybe it has to do with
    //how the notification string is being set?  I also add a third parameter to AddObserver, which might also be how its choosing what to
    //listen to.

    //When I implement levelling up, I think I'll be posting that notification from the levelComponent.OnEXPDidChange()


    if(skirmish.didHit)
    {
      exp += atkExp;
    }
    if(skirmish.didKill)
    {
      exp += killExp;
    }

    
    player.levelComponent.EXP += exp;
    return exp;
  }

}