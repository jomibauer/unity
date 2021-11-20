using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatTypes
{
    LVL, // Level
    EXP, // Experience
    RNK, // Rank.  0 if unpromoted, 1 if promoted. 2 for special boss characters
    HP,  // Hit Points
    MHP, // Max Hit Points
    STR, // Strength
    DEF, // Physical Defense
    SKL, // Skill
    SPD, // Speed
    INT, // Intelligence
    RES, // Resistance
    LCK, // Luck
    CON, // Constitution
    MOV, // Move Range
    Count
}