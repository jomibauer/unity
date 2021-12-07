using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhaseState : BattleState
{
    public override void Enable()
    {
        base.Enable();
        StartCoroutine(NotImplementedSoSwapToMoveTarget());
    }

}
