using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassData : ScriptableObject
{
    public string className;
    public string dataName;
    public MovementTypes movementType;
    public int[] statCaps = new int[8];
    /* public int HP_cap;
    public int STR_cap;
    public int DEF_cap;
    public int SKL_cap;
    public int SPD_cap;
    public int INT_cap;
    public int RES_cap;
    public int LCK_cap; */
    public int MOV;

    public void Load(string line)
    {
        Debug.Log(line);
        string[] stats = line.Split(',');
        className = stats[0];
        dataName = stats[1];
        movementType = (MovementTypes)Convert.ToInt32(stats[2]);
        for(int i=3; i< stats.Length - 1; ++i)
        {
            statCaps[i -3] = Convert.ToInt32(stats[i]);
        }
        /* HP_cap = Convert.ToInt32(stats[3]);
        STR_cap = Convert.ToInt32(stats[4]);
        DEF_cap = Convert.ToInt32(stats[5]);
        SKL_cap = Convert.ToInt32(stats[6]);
        SPD_cap = Convert.ToInt32(stats[7]);
        INT_cap = Convert.ToInt32(stats[8]);
        RES_cap = Convert.ToInt32(stats[9]);
        LCK_cap = Convert.ToInt32(stats[10]); */
        MOV = Convert.ToInt32(stats[11]);

    }
}
