using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPane : MonoBehaviour
{
    UnitStats unitStats;
    [SerializeField] public UIPanel panel;
    [SerializeField] Text nameText;
    [SerializeField] Text lvlText;
    [SerializeField] Text hpText;
    [SerializeField] Text strText;
    [SerializeField] Text sklText;
    [SerializeField] Text spdText;
    [SerializeField] Text defText;
    [SerializeField] Text intText;
    [SerializeField] Text resText;
    [SerializeField] Text lckText;
    Dictionary<StatTypes, Text> textReference;
    Dictionary<StatTypes, int> levelPackage;

    // Start is called before the first frame update
    void Start()
    {
        textReference = new Dictionary<StatTypes, Text>();
        panel.SetPosition("Hide", false);
        textReference[StatTypes.MHP] = hpText;
        textReference[StatTypes.STR] = strText;
        textReference[StatTypes.SKL] = sklText;
        textReference[StatTypes.SPD] = spdText;
        textReference[StatTypes.DEF] = defText;
        textReference[StatTypes.INT] = intText;
        textReference[StatTypes.RES] = resText;
        textReference[StatTypes.LCK] = lckText;


    }

    public void Clear()
    {
        this.nameText.text = "";
        this.lvlText.text = "0";
        this.hpText.text = "0";
        this.strText.text = "0";
        this.sklText.text = "0";
        this.spdText.text = "0";
        this.defText.text = "0";
        this.intText.text = "0";
        this.resText.text = "0";
        this.lckText.text = "0";
        levelPackage = null;
        unitStats = null;
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

    public void LoadStats(Unit unit)
    {
        this.unitStats = unit.unitStats;
        this.nameText.text = unit.unit_name;
        this.lvlText.text = unit.levelComponent.LVL.ToString();
        LoadView();
    }

    public void LoadView()
    {
        /* //this is confusing...
        for(int i = 0; i < UnitStats.statOrder.Length - 2; ++i)
        {
            textReference[(StatTypes)i].text = unitStats.stats[(StatTypes)i].ToString();
        } */
        this.hpText.text = unitStats.stats[StatTypes.MHP].ToString();
        this.strText.text = unitStats.stats[StatTypes.STR].ToString();
        this.sklText.text = unitStats.stats[StatTypes.SKL].ToString();
        this.spdText.text = unitStats.stats[StatTypes.SPD].ToString();
        this.defText.text = unitStats.stats[StatTypes.DEF].ToString();
        this.intText.text = unitStats.stats[StatTypes.INT].ToString();
        this.resText.text = unitStats.stats[StatTypes.RES].ToString();
        this.lckText.text = unitStats.stats[StatTypes.LCK].ToString();
    }

    public void LoadLevelPackage(Dictionary<StatTypes, int> package)
    {
        this.levelPackage = package;
    }

    public IEnumerator PlayLevelUp()
    {
        IterateStatBy(lvlText, 1);
        yield return new WaitForSeconds(0.75f);
        lvlText.color = Color.white;
        foreach(var key in textReference.Keys)
        {
            if (levelPackage[key] == 0) { continue; }
            IterateStatBy(textReference[key], levelPackage[key]);
            
            yield return new WaitForSeconds(0.5f);
            textReference[key].color = Color.white;
        }
    }

    private void IterateStatBy(Text statText, int amount)
    {
        if(amount == 0) { return; }
        Debug.Log($"{statText.text}, {amount}");
        //this function is just changing the stat for now, but i'm making it now so we can use it to animate the stat changes in the future.
        int stat = Convert.ToInt32(statText.text);
        statText.text = (stat += 1).ToString();
        statText.color = Color.green;
    }
}
