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
        string[] stats = unit.data.ToString().Split(',');
        foreach(var stat in stats)
        {
            text.text += stat + "\n";
        }
        
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
