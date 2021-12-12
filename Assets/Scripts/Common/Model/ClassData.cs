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
        for(int i=4; i< stats.Length - 1; ++i)
        {
            statCaps[i -4] = Convert.ToInt32(stats[i]);
        }

        MOV = Convert.ToInt32(stats[12]);

    }
}
