using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTalkToUnitObjective : BaseObjective
{
    Unit target;
    bool talkedToConditionMet;
    List<Unit> mustBeTalkedToBy;

    void Start()
    {
        this.AddObserver(OnTargetTalkedTo, NotificationBook.UNIT_TALKED_TO + target.unit_name);
    }

    private void OnTargetTalkedTo(object arg1, object u)
    {
        if(mustBeTalkedToBy.Count > 0)
        {
            talkedToConditionMet = mustBeTalkedToBy.Contains((Unit)u);
        }
        else
        {
            talkedToConditionMet = true;
        }
    }

    protected override bool Goal()
    {
        return talkedToConditionMet;
    }

    protected override void Result()
    {
        throw new NotImplementedException();
    }
}