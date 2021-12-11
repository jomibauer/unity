using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    #region Fields/Properties
    public static readonly StatTypes[] statOrder = new StatTypes[]
    {
        StatTypes.MHP,
        StatTypes.STR,
        StatTypes.DEF,
        StatTypes.SKL,
        StatTypes.SPD,
        StatTypes.INT,
        StatTypes.RES,
        StatTypes.LCK,
        StatTypes.CON,
        StatTypes.MOV
    };
    public int[] statCaps = new int[ statOrder.Length];
    public int[] unitStats = new int[ statOrder.Length ];
    public int[] growths = new int[ statOrder.Length ];
    public string unit_name;
    public string color;
    public Dictionary<StatTypes, int> levelUpRes;
    public MovementTypes movementType;
    public Factions faction;
    public ClassData unit_class;
    public Stats stats;

    //fields just for loading unit xp and lvl from the unit data into the level component, not meant to be used outside the unit factory
    int LOAD_XP;
    int LOAD_LVL;
    #endregion

    #region Monobehaviour
    void Start()
    {
        stats = gameObject.GetComponentInParent<Stats>();
        levelUpRes = new Dictionary<StatTypes, int>();
        this.AddObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.LVL), stats);
    }
    void OnDestroy()
    {
        this.RemoveObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.LVL), stats);
    }
    #endregion

    #region Public
    //these public methods aren't useful to me at the moment, but I may be able to modify them for promotions later.
    //this does seem like its a little more complicated that I'll need, since my units will only class change once 
    //and it's not reversible.
    public void ClassAs ()
    {
        stats = gameObject.GetComponentInParent<Stats>();
        this.AddObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.LVL), stats);
        Feature[] features = GetComponentsInChildren<Feature>();

        for (int i = 0; i < features.Length; ++i)
        {
            features[i].Activate(gameObject);
        }
        
    }
    public void ClassChange ()
    {
        Feature[] features = GetComponentsInChildren<Feature>();

        for (int i = 0; i < features.Length; ++i)
        {
            features[i].Deactivate();
        }
        
        this.RemoveObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.LVL), stats);
        stats = null;
    }

    //I should change this to load from two places.  One if its the first time we've seen the unit, then from a player save file for everything subsequent    
    public void LoadStats ()
    {
        stats = GetComponentInParent<Stats>();
        for (int i = 0; i < statOrder.Length; ++i)
        {
            StatTypes type = statOrder[i];
            stats.SetValue(type, unitStats[i], false);
        }
        stats.SetValue(StatTypes.HP, stats[StatTypes.MHP], false);
    }

    public void LoadExpAndLevel(int exp, int lvl)
    {
        LOAD_XP = exp;
        LOAD_LVL = lvl;
    }

    public int GetLoadExp()
    {
        return LOAD_XP;
    }
    public int GetLoadLevel()
    {
        return LOAD_LVL;
    }

    #endregion

    #region Event Handlers
    protected virtual void OnLvlChangeNotification (object sender, object args)
    {
        Debug.Log("levelDidChange");
        int oldValue = (int)args;
        int newValue = stats[StatTypes.LVL];
        for (int i = oldValue; i < newValue; ++i)
        {
            LevelUp();
        }
    }
    #endregion
    #region Private
    void LevelUp ()
    {
        //record any stat changes in this array to tell the skirmishPlayViewController
        //pick a random stat to give a random growth a boost.
        StatTypes luckyStat = statOrder[UnityEngine.Random.Range(0, statOrder.Length - 3)];
        for (int i = 0; i < statOrder.Length; ++i)
        {
            StatTypes type = statOrder[i];
            int growth = growths[i];
            if(type == luckyStat)
            {
                growth += stats[StatTypes.LCK];
            }

            int value = stats[type];
            
            if (growth >= GetSoftRng())
            {
                value++;
                //if a stat moves up, put 1 in the levelUpResult array
                levelUpRes[type] = 1;
            } else {levelUpRes[type] = 0;}
            stats.SetValue(type, value, false);
        }
        Debug.Log("levelUp");
    }

    int GetSoftRng()
    {
        int rand1 = UnityEngine.Random.Range(1, 101);
        int rand2 = UnityEngine.Random.Range(1, 101);

        return rand1+rand2/2;
    }
    #endregion
}
