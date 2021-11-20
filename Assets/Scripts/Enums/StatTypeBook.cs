using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StatTypeBook
{
    //This is so we can save string keys that reference stat types in our data folders
    public static Dictionary<string, WeaponStatTypes> Weapons = new Dictionary<string, WeaponStatTypes>(){
        ["DAM"] = WeaponStatTypes.DAM, // damage
        ["HIT"] = WeaponStatTypes.HIT, // hit %
        ["CRT"] = WeaponStatTypes.CRT, // crit
        ["WGT"] = WeaponStatTypes.WGT, // weight
        ["DUR"] = WeaponStatTypes.DUR // durability
    };

    public static Dictionary<string, StatTypes> UnitStats = new Dictionary<string, StatTypes>(){
        ["LVL"] = StatTypes.LVL, // Level
        ["EXP"] = StatTypes.EXP, // Experience
        ["RNK"] = StatTypes.RNK, // Rank.  0 if unpromoted & 1 if promoted. 2 for special boss characters
        ["HP "]= StatTypes.HP,  // Hit Points
        ["MHP"]= StatTypes.MHP, // Max Hit Points
        ["STR"] = StatTypes.STR, // Strength
        ["DEF"] = StatTypes.DEF, // Physical Defense
        ["SKL"] = StatTypes.SKL, // Skill
        ["SPD"] = StatTypes.SPD, // Speed
        ["INT"] = StatTypes.INT, // Intelligence
        ["RES"] = StatTypes.RES, // Resistance
        ["LCK"] = StatTypes.LCK, // Luck
        ["CON"] = StatTypes.CON, // Constitution
        ["MOV"] = StatTypes.MOV // Move Range
    };

}
