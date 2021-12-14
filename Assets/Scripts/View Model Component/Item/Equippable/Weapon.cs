using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equippable
{
    public WeaponStats stats;
    public string lvl;
    public int[] attackRange;
    List<string[]> features;
    public string element;
    public WeaponTypes type;
    public string wpnName;
    void Start()
    {
        defaultSlot = EquipSlots.Weapon;
        //Equip("Iron_lance");
    }

    void Load(WeaponData weaponData)
    {
        this.wpnName = weaponData.wpnName;
        this.lvl = weaponData.level;
        this.type = weaponData.type;
        stats[WeaponStatTypes.DAM] = Convert.ToInt32(weaponData.damage);
        stats[WeaponStatTypes.HIT] = Convert.ToInt32(weaponData.hit);
        stats[WeaponStatTypes.CRT] = Convert.ToInt32(weaponData.crit);
        stats[WeaponStatTypes.WGT] = Convert.ToInt32(weaponData.weight);
        stats[WeaponStatTypes.DUR] = Convert.ToInt32(weaponData.durability);
        this.attackRange = weaponData.range;
        this.features = weaponData.features;
        this.element = weaponData.element;
    }

    public void Equip(WeaponData weapon)
    {
        this.Load(weapon);
        //IDK if this is the best way.  An optimization might be to copy the pattern in Equippable.  Rather than adding and destroying the features every time, I could add only once, 
        // then activate and deactivate whenever necessary. this makes sense because it's likely we'll be switching up weapons on the same unit, so keeping the features on but deactivating
        // them could be more efficient.
        foreach(var feature in this.features)
        {
            StatModifierFeature newFeature = this.gameObject.AddComponent<StatModifierFeature>();
            newFeature.type = StatTypeBook.UnitStats[feature[0]];
            newFeature.amount = Convert.ToInt32(feature[1]);
        }
        
    }
    
    public void Unequip()
    {
        this.name = null;
        this.attackRange = null;
        this.lvl = null;
        this.element = null;
        //This could be a problem.  Technically, when clearing out a units weapon, I'm just equipping a sword.
        this.type = 0;
        this.features = null;

        this.stats.Clear();
        this.ClearFeatures();
    }

    void ClearFeatures()
    {
        StatModifierFeature[] smfs = this.gameObject.GetComponents<StatModifierFeature>();
        foreach(StatModifierFeature smf in smfs)
        {
            Destroy(smf as StatModifierFeature);
        }
    }

}
