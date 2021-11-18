using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This applies a curve to exp required to level up based on our level.  I can modify this to accomplish what I want, which is for the amount of exp required to level up
// to remain constant(100 per level), but for the value of battles to decrease based on how far above the enemy the hero is.(level 1 kills a level 1, nets like 30 exp. 
// level 5 kills a level 1, nets only say 10 exp.)
public class LevelComponent : MonoBehaviour
{
  #region Consts
  public const int minLevel = 1;
  public const int maxLevel = 99;
  public const int maxExperience = 100;
  #endregion
  #region Fields / Properties
  public int LVL
  {
    get { return stats[StatTypes.LVL]; }
  }
  public int EXP
  {
    get { return stats[StatTypes.EXP]; }
    set { stats[StatTypes.EXP] = value; }
  }

  public int RNK
  {
    get { return stats[StatTypes.RNK]; }
  }
  Stats stats;
  #endregion
  #region MonoBehaviour
  void Awake ()
  {
    stats = GetComponent<Stats>();
  }
  void OnEnable ()
  {
    this.AddObserver(OnExpWillChange, Stats.WillChangeNotification(StatTypes.EXP), stats);
    this.AddObserver(OnExpDidChange, Stats.DidChangeNotification(StatTypes.EXP), stats);
  }
  void OnDisable ()
  {
    this.RemoveObserver(OnExpWillChange, Stats.WillChangeNotification(StatTypes.EXP), stats);
    this.RemoveObserver(OnExpDidChange, Stats.DidChangeNotification(StatTypes.EXP), stats);
  }
  #endregion
  #region Event Handlers
  void OnExpWillChange (object sender, object args)
  {
    ValueChangeException vce = args as ValueChangeException;
    vce.AddModifier(new ClampValueModifier(int.MaxValue, 1, maxExperience));
  }
  
  void OnExpDidChange (object sender, object args)
  {
    if(EXP >= 100)
    {
      //Level up
      EXP -= 100;
      stats.SetValue(StatTypes.LVL, LVL + 1, false);
    }
    stats.SetValue(StatTypes.EXP, EXP, false);
  }
  #endregion
  #region Public
  public void Init (int level, int experience)
  {
    stats.SetValue(StatTypes.LVL, level, false);
    stats.SetValue(StatTypes.EXP, experience, false);
  }
  #endregion
}