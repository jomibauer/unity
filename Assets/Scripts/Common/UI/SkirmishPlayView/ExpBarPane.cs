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
    [SerializeField] public float expMoveSpeed = .1f;
    public int currentExp;
    public int remainingExp;
    // Start is called before the first frame update
    void Start()
    {
        this.panel.SetPosition("Hide", false);
        this.AddObserver(OnAwardExpStart, NotificationBook.AWARD_EXP_START);
        //this.AddObserver(OnSkirmishSetExp, NotificationBook.SKIRMISH_SET_EXP);
    }

    public void Load(Unit unit)
    {
        this.currentExp = unit.levelComponent.EXP;
    }

    public void Reset()
    {
        this.currentExp = 0;
        this.expBar.Reset();
    }

    private void OnAwardExpStart(object sender, object xp)
    {
        Debug.Log("AWARD EXP START");
        int exp = (int)xp;
        ShowPane();
        StartCoroutine(AwardExp(exp));
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

    private IEnumerator AwardExp(int exp)
    {
        remainingExp = exp;
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < exp; ++i)
        {
            if(currentExp == 100)
            {
                LevelUp();
            }
            currentExp += 1;
            remainingExp -= 1;
            this.expText.text = currentExp.ToString();
            this.expBar.Raise();
            yield return new WaitForSeconds(expMoveSpeed);
        }
        // this needs a bit more fleshing out when we can level up.  Leaving this bare, but we're going to have to change state and 
        // remember how much exp is left to give.  I think we might be able to do this on the AwardExpState, but it might be smarter to do 
        // it with the level component.
        StartCoroutine(FinishAwardingExp());
    }

    private IEnumerator FinishAwardingExp()
    {
        yield return new WaitForSeconds(0.7f);
        HidePane();
        yield return new WaitForSeconds(0.5f);
        this.PostNotification(NotificationBook.AWARD_EXP_FINISHED);
    }

    private void LevelUp()
    {
        
    }
}
