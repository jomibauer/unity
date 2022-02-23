using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatTargetVictoryCondition : BaseVictoryCondition
{
    public Unit target;
    protected override void CheckForGameOver(Unit u)
    {
        base.CheckForGameOver(u);
        if (Victor == Factions.None && IsDefeated(target))
        {
            Victor = Factions.player;
        }
    }
}