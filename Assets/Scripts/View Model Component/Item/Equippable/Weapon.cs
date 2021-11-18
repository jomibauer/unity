using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equippable
{
    WeaponStats stats;
    string wpnName;
    void Start()
    {
        defaultSlot = EquipSlots.Weapon;
    }
    
    public void Load(string line)
    {
        Debug.Log(line);
        string[] _stats = line.Split(',');

        stats[WeaponStatTypes.LVL] = Convert.ToInt32(_stats[0]);
        stats[WeaponStatTypes.DAM] = Convert.ToInt32(_stats[1]);
        stats[WeaponStatTypes.HIT] = Convert.ToInt32(_stats[2]);
        stats[WeaponStatTypes.CRT] = Convert.ToInt32(_stats[3]);
        stats[WeaponStatTypes.WGT] = Convert.ToInt32(_stats[4]);
        stats[WeaponStatTypes.DUR] = Convert.ToInt32(_stats[5]);
        stats[WeaponStatTypes.RNG] = Convert.ToInt32(_stats[6]);
        stats[WeaponStatTypes.SPC] = Convert.ToInt32(_stats[7]);
        stats[WeaponStatTypes.ELE] = Convert.ToInt32(_stats[8]);
    }
    
}
