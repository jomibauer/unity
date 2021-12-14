using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassData : ScriptableObject
{
    public string className;
    public string dataName;
    public int RNK;
    public MovementTypes movementType;
    public List<WeaponTypes> weaponTypes;
    public int[] statCaps = new int[8];
    public int MOV;

    public void Load(string line)
    {
        
        Debug.Log(line);
        string[] stats = line.Split(',');
        className = stats[0];
        dataName = stats[1];
        movementType = (MovementTypes)Convert.ToInt32(stats[2]);
        RNK = Convert.ToInt32(stats[3]);

        weaponTypes = new List<WeaponTypes>();
        foreach(var type in stats[4].Split('|')) { weaponTypes.Add((WeaponTypes)Convert.ToInt32(type)); }

        for(int i=5; i< stats.Length - 1; ++i)
        {
            statCaps[i - 5] = Convert.ToInt32(stats[i]);
        }

        MOV = Convert.ToInt32(stats[stats.Length - 1]);

    }
}
