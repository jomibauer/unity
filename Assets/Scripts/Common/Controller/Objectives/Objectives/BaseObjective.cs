using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObjective : MonoBehaviour
{
    protected abstract bool Goal();
    protected abstract void Result();
    protected virtual void CheckGoal()
    {
        if(Goal())
        {
            Result();
        }
    }
}
