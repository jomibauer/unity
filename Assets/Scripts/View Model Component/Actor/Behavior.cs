using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    public bool mustSurvive 
    // if a unit dies and this is true, game over
    {
        get {return mustSurvive;}
        protected set {mustSurvive = value;}
    }
    public bool canRescue
    // a unit can "rescue" another unit, allowing them to share a square and hp bar for a speed penalty.  Mounted units only probably.
    {
        get {return canRescue;}
        protected set {canRescue = value;}
    }
    public bool canCapture
    // a unit can "capture" a throne/special tile to win.
    {
        get {return canCapture;}
        protected set {canCapture = value;}
    }
    public bool canTalk
    {
        get {return canTalk; }
        protected set {canTalk = value;}
    }
    public bool hasBattleConvo
    // a unit has a special battle convo when fighting for the first time
    {
        get {return hasBattleConvo;}
        protected set {hasBattleConvo = value;}
    }

    public List<Unit> canTalkTo
    {
        get { return canTalkTo; }
        protected set {canTalkTo = value;}
    }
    public List<Unit> specialConvoWith
    {
        get { return specialConvoWith; }
        protected set { specialConvoWith = value; }
    }

    public List<Unit> specialBattleConvoWith
    {
        get { return specialBattleConvoWith; }
        protected set {specialBattleConvoWith = value;}
    }

    public Behavior(bool mustSurvive = false
                  , bool canRescue = false
                  , bool canCapture = false
                  , bool canTalk = false
                  , bool hasBattleConvo = false
                  , List<Unit> canTalkTo = null
                  , List<Unit> specialConvoWith = null
                  , List<Unit> specialBattleConvoWith = null )
    // defaults since 9/10 these behaviors will look like this (due to generic enemies), we can just use a Behavior component 
    // with an empty constructor for them instead of making components for every unit
    {
        this.mustSurvive = mustSurvive;
        this.canRescue = canRescue;
        this.canCapture = canCapture;
        this.hasBattleConvo = hasBattleConvo;
        this.canTalkTo = canTalkTo;
        this.specialConvoWith = specialConvoWith;
        this.specialBattleConvoWith = specialBattleConvoWith;
    }
}