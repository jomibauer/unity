using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{

    #region Fields / Properties
    public IList<Equippable> items { get { return _items.AsReadOnly(); }}
    List<Equippable> _items = new List<Equippable>();
    #endregion
    #region Public
    public void Equip (Equippable item, EquipSlots slot)
    {
        UnEquip(slot);
        _items.Add(item);
        item.transform.SetParent(transform);
        item.currentSlot = slot;
        item.OnEquip();
        this.PostNotification(NotificationBook.EQUIPPED_ITEM, item);
    }
    public void UnEquip (Equippable item)
    {
        item.OnUnEquip();
        item.currentSlot = EquipSlots.None;
        item.transform.SetParent(transform);
        _items.Remove(item);
        this.PostNotification(NotificationBook.UNEQUIPPED_ITEM, item);
    }
    
    public void UnEquip (EquipSlots slot)
    {
        for (int i = _items.Count - 1; i >= 0; --i)
        {
            Equippable item = _items[i];
            if ( (item.currentSlot & slot) != EquipSlots.None )
            {
                UnEquip(item);
            }
        }
    }
    #endregion
}
