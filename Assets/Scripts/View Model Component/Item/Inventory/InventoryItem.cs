using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class InventoryItem 
{
    public string name;
    public string displayName;
    public string type;
    public int? durability;

    public InventoryItem(string line)
    {
        Load(line);   
    }
    public void Load(string line)
    {
        TextInfo textInfo = new CultureInfo("en-US",false).TextInfo;
        string[] _data = line.Split('|');

        name = _data[0];
        displayName = textInfo.ToTitleCase(name.Replace('_', ' '));
        type = _data[1];
        //I think eventually I'll want a way to identify durability as full and add it here.  maybe another book, but there might be a less annoying way.
        if (_data[2] == "--") 
        {
            durability = null;
        }
        else 
        {
            durability = Convert.ToInt32(_data[2]);
        }

    }

    public override string ToString()
    {
        return name + ", " + displayName;
    }
}
