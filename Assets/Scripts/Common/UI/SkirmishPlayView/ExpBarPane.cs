using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarPane : MonoBehaviour
{
    [SerializeField] ExpBar expBar;
    [SerializeField] UIPanel panel;
    [SerializeField] Text expText;
    [SerializeField] public float expMoveSpeed = .07f;
    public int currentExp;
    public int remainingExp;
    // Start is called before the first frame update
    void Start()
    {
        this.panel.SetPosition("Hide", false);
        
        //this.AddObserver(OnSkirmishSetExp, NotificationBook.SKIRMISH_SET_EXP);
    }

    

    public void Load(Unit unit)
    {
        
        this.currentExp = unit.levelComponent.EXP;
        this.expBar.Load(currentExp);
        this.expText.text = currentExp.ToString();
        Debug.LogWarning($"[ExpBarPane.cs]: loaded: {currentExp}");
    }

    public void Clear()
    {
        this.currentExp = 0;
        this.expBar.Reset();
    }

    public void ShowPane(bool animate = true)
    {
        
        Tweener t = panel.SetPosition("Show", animate);
        t.easingControl.equation = EasingEquations.EaseInOutBack;
    }

    public void HidePane(bool animate = true)
    {
        
        Tweener t = panel.SetPosition("Hide", animate);
        t.easingControl.equation = EasingEquations.EaseInOutBack;
    }

    public int IncrementBar()
    {
        currentExp += 1;
        this.expText.text = currentExp.ToString();
        this.expBar.Raise();
        return currentExp;
    }

    public void ResetExp()
    {
        this.currentExp -= 100;
        this.expBar.Reset();
        this.expText.text = "0";
    }

    private IEnumerator FinishAwardingExp()
    {
        yield return new WaitForSeconds(0.7f);
        HidePane();
        yield return new WaitForSeconds(0.5f);
        this.PostNotification(NotificationBook.AWARD_EXP_FINISHED);
    }

}
