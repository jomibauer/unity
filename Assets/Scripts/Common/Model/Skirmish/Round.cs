using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round
{
    public bool hit;
    public int damage = 0;
    public bool crit;
    public Unit actor;
    public Round(bool hit, int damage, bool crit, Unit actor)
    {  
        this.hit = hit;
        this.damage = damage;
        this.crit = crit;
        this.actor = actor;
    }

    public override string ToString()
    {
        return $"ROUND: {actor}; HIT?:{hit}; CRIT?:{crit}; DAM:{damage}";
    }
}
