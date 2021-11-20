using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkirmishPreviewPane : MonoBehaviour
{
    string curPos;
    UIPanel panel;
    [SerializeField] Text initiatorName;
    [SerializeField] Text initiatorMaxHP;
    [SerializeField] Text initiatorHP;
    [SerializeField] Text initiatorDamage;
    [SerializeField] Text initiatorHit;
    [SerializeField] Text initiatorCrit;

    [SerializeField] Text receiverName;
    [SerializeField] Text receiverMaxHP;
    [SerializeField] Text receiverHP;
    [SerializeField] Text receiverDamage;
    [SerializeField] Text receiverHit;
    [SerializeField] Text receiverCrit;
    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<UIPanel>();
        panel.SetPosition("HideLeft", false);
        this.AddObserver(OnSkirmishPanePopulate, NotificationBook.SKIRMISH_PANE_POPULATE);
        this.AddObserver(OnShowSkirmishPreviewPane, NotificationBook.SHOW_SKIRMISH_PREVIEW_PANE);
        this.AddObserver(OnHideSkirmishPreviewPane, NotificationBook.HIDE_SKIRMISH_PREVIEW_PANE);
    }

    private void OnSkirmishPanePopulate(object sender, object sk)
    {
        Skirmish skirmish = sk as Skirmish;

        initiatorName.text = skirmish.initiator.name;
        initiatorMaxHP.text = "/" + skirmish.initiatorStats.MaxHP.ToString();
        initiatorHP.text = skirmish.initiator.HP.ToString();
        initiatorDamage.text = skirmish.initiatorStats.dam.ToString();
        if(skirmish.initiatorStats.turns == 2)
        {
            initiatorDamage.text += "x2";
        }
        initiatorHit.text = skirmish.initiatorStats.hit.ToString();
        initiatorCrit.text = skirmish.initiatorStats.crit.ToString();

        receiverName.text = skirmish.receiver.name;
        receiverMaxHP.text = "/" + skirmish.receiverStats.MaxHP.ToString();
        receiverHP.text = skirmish.receiverStats.HP.ToString();
        receiverDamage.text = skirmish.receiverStats.dam.ToString();
        if(skirmish.receiverStats.turns == 2)
        {
            receiverDamage.text += "x2";
        }
        receiverHit.text = skirmish.receiverStats.hit.ToString();
        receiverCrit.text = skirmish.receiverStats.crit.ToString();
        
    }

    private void OnHideSkirmishPreviewPane(object sender, object p = null)
    {
        if(curPos == "ShowRight")
        {
            Tweener t = panel.SetPosition("HideRight", true);
            t.easingControl.equation = EasingEquations.EaseInOutBack;
        } 
        else 
        {
            Tweener t = panel.SetPosition("HideLeft", true);
            t.easingControl.equation = EasingEquations.EaseInOutBack;
        }
    }

    private void OnShowSkirmishPreviewPane(object sender, object pos)
    {
        curPos = pos.ToString();
        Tweener t = panel.SetPosition(curPos, true);
        t.easingControl.equation = EasingEquations.EaseInOutBack;   
    }


}
