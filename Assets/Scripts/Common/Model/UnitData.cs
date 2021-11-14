using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Unit", menuName ="Unit")]
public class UnitData : ScriptableObject
{
    public int maxHP;
    public string unit_class;
    public int damage;
    public int move;
    public int defense;

    // Start is called before the first frame update
    public void Load(string line)
    {
        Debug.Log(line);
        string[] stats = line.Split(',');
        unit_class = stats[0];
        maxHP = Convert.ToInt32(stats[1]);
        damage = Convert.ToInt32(stats[2]);
        move = Convert.ToInt32(stats[3]);
        defense = Convert.ToInt32(stats[4]);

    }

    public override string ToString()
   {
      return $"Class:{unit_class},HP:{maxHP},Damage:{damage},Move:{move},Defense:{defense}";
   }
}
