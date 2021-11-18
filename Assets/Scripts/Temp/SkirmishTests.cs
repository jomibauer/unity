using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To run these tests, you need a scene with at least two units, a tile map, a skirmish controller, and a game object with this script attached.
//make sure to put units in this component's fields in the inspector, and to also put the tilemap onto the units' fields in the inspector.
// also comment out the unit's update function
public class SkirmishTests : MonoBehaviour
{
    [SerializeField] UnitData dataA;
    [SerializeField] UnitData dataB;
    [SerializeField] Unit unitA;
    [SerializeField] Unit unitB;
    [SerializeField]SkirmishController skirmishController;

    void Start()
    {
        Unit testInitiator = unitA;
        Unit testReceiver = unitB;
        testInitiator.currentTile = new Tile(0,1);
        testReceiver.currentTile = new Tile(0,0);

        Skirmish skirmish = new Skirmish(testInitiator, testReceiver, "MELEE");
        this.PostNotification("SKIRMISH_START", skirmish);
        RunPreviewTest();
        Debug.Log($"========&&&&&&=========");
        RunRoundsTest();
    }


    void RunPreviewTest()
    {
        Skirmish skirmish = skirmishController.GetSkirmish();
        if (skirmish == null) { throw new System.Exception("Skirmish doesn't exist! Make sure to notify the skirmishController that a skirmish has started before switching to this state."); }
        
        //Create UI panel displaying skirmish stats.
        Debug.Log($"{skirmish.initiator}");
        Debug.Log($"========");
        Debug.Log($"{skirmish.initiatorStats.hit}");
        Debug.Log($"{skirmish.initiatorStats.dam}");
        Debug.Log($"{skirmish.initiatorStats.crit}");

        Debug.Log($"{skirmish.receiver}");
        Debug.Log($"========");
        Debug.Log($"{skirmish.receiverStats.hit}");
        Debug.Log($"{skirmish.receiverStats.dam}");
        Debug.Log($"{skirmish.receiverStats.crit}");
    }

    void RunRoundsTest()
    {
        Debug.Log("ONE");
        List<Round> rounds = skirmishController.GetRounds();
        Debug.Log(rounds.Count);
        foreach(var round in rounds) {Debug.Log(round);}
        Debug.Log($"========");
        Debug.Log("TWO");
        rounds = skirmishController.GetRounds();
        foreach(var round in rounds) {Debug.Log(round);}
    }
}
