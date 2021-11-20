using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory", menuName ="Inventory")]
public class InventoryData : ScriptableObject
{
    public string inventoryOwner;
 
    [SerializeField] public string[] InventoryItemStrings;


    public void Load(string line)
    {
        string[] _data = line.Split(',');
        List<string> InventoryItems = new List<string>();
        inventoryOwner = _data[0];

        for (int i = 1; i< _data.Length; i++)
        {
            if(_data[i] == "")
            {
                continue;
            }
            try{
                InventoryItems.Add(_data[i]);
            }
            catch(System.NullReferenceException e) {
                Debug.Log(_data[i]);
                throw e;
            }
        }
        InventoryItemStrings = InventoryItems.ToArray();
    }
}
