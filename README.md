
Inventory
---



**SKIRMISH PLAY VIEW**

  SKIRMISH PLAY VIEW TIMING
- Skirmish play view timing is a little rough, but I think it's best to wait to fix this after implementing skirmish animations, as I will likely have to overhaul whatever fix I apply here.


 SKIRMISH PLAY VIEW INFO CLEAR TIMING
- Skirmish play view is clearing before the Hide animation finishes so the player can see the pane info change to "HP", empty strings, etc.  I think I need to use coroutines so we can await the end of the animation before clearing, but I'll figure it out later.
