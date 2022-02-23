using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkirmishController : MonoBehaviour
{
    Skirmish skirmish;
    [SerializeField]Unit initiator;
    [SerializeField]Unit receiver;
    Unit deadUnit;
    List<Round> rounds;
    // Start is called before the first frame update
    void Start()
    {
        skirmish = null;
    }

    public void Clear()
    {
        skirmish = null;
        initiator = null;
        receiver = null;
        deadUnit = null;
        rounds = new List<Round>();
    }

    internal Skirmish GetSkirmish()
    {
        return skirmish;
    }

    internal Unit GetInitiator()
    {
        return initiator;
    }

    internal Unit GetReceiver()
    {
        return receiver;
    }

    internal Unit GetDeadUnit()
    {
        return deadUnit;
    }
    public void SetDeadUnit(Unit unit)
    {
        deadUnit = unit;
    }

    

    public void InitNewSkirmish(Unit initiator, Unit receiver, string type)
    {
        skirmish = new Skirmish(initiator, receiver, type);
        this.initiator = skirmish.initiator;
        this.receiver = skirmish.receiver;
        Debug.Log($"[SkirmishController.cs]: On skirmish start: {initiator} vs {receiver}");
    }

    private Round GetRound(SkirmishStatSet attackerStats, Unit attacker)
    {
        int seedA = UnityEngine.Random.Range(0, 101);
        int seedB = UnityEngine.Random.Range(0, 101);
        bool hit = ((seedA + seedB) / 2) <= attackerStats.hit;
        int dam = attackerStats.dam;
        bool crit = ((seedA + seedB) / 2) <= attackerStats.crit;

        return new Round(hit, dam, crit, attacker);
    }


    //If I'm using a brave weapon, I'll probably use a different function than Interleave here.  
    //I could do a check on each round for "hasBraveWeapon" and then just add two rounds for each turn the stats have.
    //Then, I can modify Interleave to have two bool args, one for initiator and one for receiver.
    // for whichever is true, I can take their rounds from the source lists two at a time rather than one.
    // then I dont have to mess with logic much up here.
    public void InitRounds()
    {
        List<Round> initiatorRounds = new List<Round>();
        List<Round> receieverRounds = new List<Round>();

        for(int i = 0; i < skirmish.initiatorStats.turns; i++)
        {
            initiatorRounds.Add(GetRound(skirmish.initiatorStats, initiator));
        }

        for(int r = 0; r < skirmish.receiverStats.turns; r++)
        {
            receieverRounds.Add(GetRound(skirmish.receiverStats, receiver));
        }

        if(receieverRounds.Count < 1) { rounds = initiatorRounds; }

        rounds = initiatorRounds.Interleave(receieverRounds).ToList(); 
    }

    public List<Round> GetRounds()
    {
        return rounds;
    }

    public void SetSkirmishHit()
    {
        skirmish.didHit = true;
    }

    public void SetSkirmishKill()
    {
        skirmish.didKill = true;
    }
}
