using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameClassListener : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        this.AddObserver(OnUnitInfo, "UNIT_INFO");
        this.AddObserver(OnTileInfo, "TILE_INFO");
    }

    private void OnTileInfo(object arg1, object arg2)
    {
        text.text = "";
    }

    private void OnUnitInfo(object sender, object u)
    {
        text.text="";
        Unit unit = (Unit)u;

       /* // string[] stats = unit.data.ToString().Split(',');
        for(int i = 0; i < (int)StatTypes.Count; i++ )
        {
            text.text += unit.stats[] + "\n";
        } */
        text.text += unit.unit_name + "\n";
        text.text += $"{unit.GetClassName()} \n";
        text.text += $"EXP: {unit.levelComponent.EXP}";

        
        
    }
}
