using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName ="Weapon")]
public class WeaponData : ScriptableObject
{
    public string wpnName;
    public int damage;
    public int weight;
    public string level;
    public int durability;
    //range should be 2 numbers max.  in the csv, write it as a range e.g. min-max.  the dash is important
    public int[] range;
    //I think we could use notifications here to have the weapon get its special abilities in a skirmish.  put the notification string here as
    //SPECIAL, then post the notification (if there is one) when initialising the skirmish. 
    //use a '-' dash here to separate multiple special weapon abilities.  Hopefully only a few weapons need multiple specials.
    public string[] SPECIAL;
    public string element;
    public int hit;
    public int crit;

    public void Load(string line)
    {
        Debug.Log(line);
        string[] stats = line.Split(',');
        wpnName = stats[0];
        damage = Convert.ToInt32(stats[1]);
        weight = Convert.ToInt32(stats[2]);
        level = stats[3];
        durability = Convert.ToInt32(stats[4]);
        range = Array.ConvertAll(stats[5].Split('-'), int.Parse);
        SPECIAL = stats[6].Split('-');
        element = stats[7];
        hit = Convert.ToInt32(stats[8]);
        crit = Convert.ToInt32(stats[9]);

    }
}
