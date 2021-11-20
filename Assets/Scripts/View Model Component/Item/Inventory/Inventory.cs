using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int openSlots = 5;
    [SerializeField] List<InventoryItem> items;
    [SerializeField] Weapon equippedWeapon;

    void Start()
    {
        Unit unit = GetComponentInParent<Unit>();
        items = new List<InventoryItem>();
        this.Load(unit.unit_name);

    }

    public void Load(string name)
    {
        InventoryData _data = Resources.Load<InventoryData>("Items/Inventories/" + name);
  
        foreach(var item in _data.InventoryItemStrings)
        {
            if(item == null){
                continue;
            }
            else{
                AddItem(new InventoryItem(item));
            }
        }
    }

    public List<InventoryItem> GetItems()
    {
        return items;
    }

    public string GetItemAt(int index)
    {
        //return name in order to lookup ItemData SO for use.
        return items[index].name;
    }

    public void SwapItems(int from, int to)
    {
        items = items.Swap(from, to) as List<InventoryItem>;
    }

    public void AddItem(InventoryItem item)
    {
        if(openSlots > 0){
            items.Add(item);
        }
        openSlots -= 1;
    }

    public void UseItemAt(int index)
    {
        if (items[index].durability == null) { return; }
        items[index].durability -= 1;
        if (items[index].durability == 0)
        {
            RemoveItem(index);
        }
    }

    private void RemoveItem(int index)
    {
        items.Remove(items[index]);
        openSlots +=1;
        //notification? Probably to the skirmishplay state.  Maybe the weapon?  maybe we need a reference here though to avoid unequipping everyone with the same type of weapon equipped.
    }

    /* public void OnItemUsed(object sender, object useInfo)
    {
        
    }
 */
}
