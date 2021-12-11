using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceViewController : MonoBehaviour
{
    [SerializeField] public ExpBarPane expPane;
    [SerializeField] public LevelUpPane levelUpPane;
    int remainingEXP;
    UnitStats unitStats;
    void Start()
    {
        this.AddObserver(OnAwardExpStart, NotificationBook.AWARD_EXP_START);
        this.AddObserver(OnAwardExpInit, NotificationBook.AWARD_EXP_INIT);
    }

    private void OnShowLevelPane(object sender, object n_u)
    {
        levelUpPane.ShowPane();
    }

    private void OnHideLevelPane(object sender, object n_u)
    {
        levelUpPane.HidePane();
    }

    private void OnAwardExpStart(object sender, object xp)
    {
        this.remainingEXP = (int)xp;
        
        //if the dictionary has stuff in it, that means theres been a level up.
        if(WasLevelUp())
        {
            this.levelUpPane.LoadLevelPackage(unitStats.levelUpRes);
        }

        StartCoroutine(PlayAwardExp());
    }
    private void OnAwardExpInit(object sender, object u)
    {
        Unit unit  = (Unit)u;
        unitStats = unit.unitStats;
        expPane.Load(unit);
        levelUpPane.LoadStats(unit);
    }

    private IEnumerator PlayAwardExp()
    {
        int currentExp;
        float moveSpeed = expPane.expMoveSpeed;

        expPane.ShowPane();
        //wait for it to show
        yield return new WaitForSeconds(1f);

        //start awarding exp
        for (int i = 0; i < remainingEXP; ++i)
        {
            currentExp = expPane.IncrementBar();
            
            // if we reached 100, there should've been a level up, so play reset the bar to 0 and play the level up.
            if(currentExp == 100)
            {
                expPane.ResetExp();
                yield return StartCoroutine(PlayLevelUp());
                // clear out the levelUpResult after playing the level up.
                unitStats.levelUpRes = new Dictionary<StatTypes, int>();
            }
            // award each exp unit at the set speed.
            yield return new WaitForSeconds(moveSpeed);
        }

        // linger on the final exp total for a bit
        yield return new WaitForSeconds(0.7f);

        expPane.HidePane();
        //wait for animation to finish
        yield return new WaitForSeconds(0.5f);

        // clear out the UI after hiding the elements then finish awarding EXP.
        this.expPane.Clear();
        this.levelUpPane.Clear();
        unitStats = null;
        this.PostNotification(NotificationBook.AWARD_EXP_FINISHED);
    }

    private IEnumerator PlayLevelUp()
    {
        levelUpPane.ShowPane();
        //wait for it to show
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(levelUpPane.PlayLevelUp());
        yield return new WaitForSeconds(1f);
        levelUpPane.HidePane();
        //wait for animation to finish
        yield return new WaitForSeconds(0.5f);
    }

    bool WasLevelUp()
    {
        //if theres a levelUpResult with entries, that means theres been an unapplied levelup, so a level up happened.
        return unitStats.levelUpRes.Count != 0;
    }
}
