using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    //Super simple helper component.  enable it to start moving the selected unit, then disable it when the unit is finished moving.  This gets touched only by the traversalState.
    //it posts a notification back to the traversal state when the units done moving.  This lets me use an Update() function for silky smooth movement while not having it checking each 
    // individual unit every frame to see if it needs to move.  Much kinder to the old computer.
    UnitController unitController;
    void Start()
    {
        unitController = GetComponentInParent<UnitController>();
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!unitController.UnitFinishedMoving())
        {
            unitController.MoveSelectedUnit();
        }
        else 
        {
            this.PostNotification(NotificationBook.FINISHED_MOVING);
        }
    }
}
