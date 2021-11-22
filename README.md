Health Bar
---
- work is on `develop` branch

**Future Changes**
- Now that the prefab is working, I want to add it to the skirmish states.  Add two to a new canvas called "SkirmishPlayCanvas" and have them load up unit HP whenever a skirmish happens.  Disable them whenever a skirmish isnt happening.

Inventory
---

Some notes:
- Inventory function relies on including a spreadsheet in the Resources/Data/ folder called inventories.csv
- In that csv, InventoryItems are represented in as a three part string separated by pipes: {item_name}|{type}|{durability}
- one three-part InventoryItem goes in each column or Slot, and there are a max of 5 slots in an inventory.
- {item name} needs to matchup with one of the items in the Resources folder, otherwise when using it, the item will do nothing.  Might even throw an error if it can't find a matching item.

**Future changes**
---
**WRITE DATA**
- right now, we're just reading data, but as with every feature we add that interacts with our Data/ folder, we want to write to those files too, eventually.  This means persistent inventories across levels and play sessions.

**CONNECT INVENTORYITEMS TO ITEM SCRIPTABLEOBJECTS**
- For one, durability is not fully implemented.  But getting the full stats before a skirmish is important.  We need to think about how we're going to access the actual Items in corresponding InventoryItems.  Separating them works now, but eventually we want to connect them to the actual Item SOs that have the item stats so we can preview their stats in menus, & get item durability for unused weapons.
