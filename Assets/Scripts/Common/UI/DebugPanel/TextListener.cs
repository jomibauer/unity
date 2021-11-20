using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextListener : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        this.AddObserver(OnTileInfo, "TILE_INFO");
        this.AddObserver(OnUnitInfo, "UNIT_INFO");
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
        text.text += unit.HP + "/" + unit.stats[StatTypes.MHP] + "\n";
        text.text += $"STR: {unit.stats[StatTypes.STR]} \n";
        text.text += $"DEF: {unit.stats[StatTypes.DEF]} \n";
        text.text += $"SPD: {unit.stats[StatTypes.SPD]} \n"; 
        text.text += $"MOV: {unit.stats[StatTypes.MOV]} \n";
        
        text.text += $"EXP: {unit.levelComponent.EXP}";
    }

    private void OnTileInfo(object sender, object t)
    {
        Tile tile = (Tile)t;
        text.text = tile.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
