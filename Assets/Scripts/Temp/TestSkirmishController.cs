using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkirmishController : SkirmishController
{
    Skirmish skirmish;
    [SerializeField]Unit Tinitiator;
    [SerializeField]Unit Treceiver;
  
    // Start is called before the first frame update
    void Start()
    {
        //this.AddObserver(OnSkirmishStart, "SKIRMISH_START");
        skirmish = null;
    }

    

    internal Skirmish TestGetSkirmish()
    {
        return new Skirmish(Tinitiator, Treceiver, "MELEE");
    }

    
}
