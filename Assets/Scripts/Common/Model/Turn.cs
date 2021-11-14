using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    public Unit actor;
    public bool hasUnitMoved;
    public bool hasUnitActed;
    public bool lockMove;
    Tile startTile;

    public void Change (Unit current)
    {
        //I think its a good idea to check that we're changing units before resetting this class.
        if(actor != current){
            actor = current;
            hasUnitMoved = current.hasUnitMoved;
            hasUnitActed = current.hasUnitActed;
            if(hasUnitActed) { lockMove = true; }
            else{ lockMove = false; }
            startTile = actor.currentTile;
        }
    }
    public void UndoMove ()
    {
        actor.hasUnitMoved = false;
        hasUnitMoved = false;
        actor.TeleportTo(startTile);
    }
    
}
