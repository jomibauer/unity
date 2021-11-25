Health Bar
---
- work is on `develop` branch

**Future Changes**
  SKIRMISH PLAY VIEW TIMING
- Skirmish play view timing is a little rough, but I think it's best to wait to fix this after implementing skirmish animations, as I will likely have to overhaul whatever fix I apply here.
  SKIRMISH PLAY VIEW INFO CLEAR TIMING
- Skirmish play view is clearing before the Hide animation finishes so the player can see the pane info change to "HP", empty strings, etc.  I think I need to use coroutines so we can await the end of the animation before clearing, but I'll figure it out later.

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
