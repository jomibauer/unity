using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int this[WeaponStatTypes s]
    {
      get { return _data[(int)s]; }
      set { SetValue(s, value); } 
    }
    int[] _data = new int[ (int)WeaponStatTypes.Count ];
    public void SetValue (WeaponStatTypes type, int value)
    {
        _data[(int)type] = value;
    }

    public void Clear()
    {
      foreach(var statType in WeaponStatTypes.GetValues(typeof(WeaponStatTypes)))
      {
        SetValue((WeaponStatTypes)statType, 0);
      }
    }
}
