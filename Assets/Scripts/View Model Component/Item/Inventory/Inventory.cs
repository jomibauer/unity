using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int openSlots = 5;
    public List<InventoryItem> items = new List<InventoryItem>();
    [SerializeField] Weapon equippedWeapon;
    [SerializeField] List<string> item_strings = new List<string>();

    //How to do this with non Player units?  Name should probably be something like class + id.  I probably need to do a rethink of how I access unit data & use something more universal.
    public void Load(string name, Factions faction)
    {
        InventoryData _data = Resources.Load<InventoryData>($"Items/Inventories/{faction}/{name}");
        if(_data == null)
        {
            Debug.Log(name);
            throw new NullReferenceException();
        }
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
            item_strings.Add(item.displayName);
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
