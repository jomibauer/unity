using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatAllEnemiesVictoryCondition : BaseVictoryCondition
{
    protected override void CheckForGameOver(Unit u)
    {
        base.CheckForGameOver(u);
        if (Victor == Factions.None && FactionIsDefeated(Factions.enemy))
        {
            Victor = Factions.player;
        }
    }
}