using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class UnitParser 
{
    [MenuItem("Pre Production/Parse Unit Stats")]
    public static void Parse()
    {
        CreateDirectories();
        ParsePlayerUnitStats();
        ParseEnemyUnitStats();
        ParseGrowthStats();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static void CreateDirectories()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Units"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Units");
        }
    }

    public static void ParsePlayerUnitStats()
    {
        string readPath = $"{Application.dataPath}/Resources/Data/units/units_stats.csv";
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i<readText.Length; i++)
        {
            ParseUnitStats(readText[i], Factions.player);
        }
    }

    public static void ParseEnemyUnitStats()
    {
        string readPath = $"{Application.dataPath}/Resources/Data/units/enemies/enemy_stats.csv";
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i<readText.Length; i++)
        {
            ParseUnitStats(readText[i], Factions.enemy);
        }
    }
    public static void ParseFriendlyOtherUnitStats()
    {
        string readPath = $"{Application.dataPath}/Resources/Data/units/friendly_other_stats.csv";
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i<readText.Length; i++)
        {
            ParseUnitStats(readText[i], Factions.friendly_other);
        }
    }
    public static void ParseUnfriendlyOtherUnitStats()
    {
        string readPath = $"{Application.dataPath}/Resources/Data/units/unfriendly_other_stats.csv";
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i<readText.Length; i++)
        {
            ParseUnitStats(readText[i], Factions.unfriendly_other);
        }
    }

    static void ParseUnitStats(string line, Factions faction)
    {
        string[] elements = line.Split(',');
        GameObject obj = GetOrCreate(elements[0], faction);
        UnitStats u_stats = obj.GetComponent<UnitStats>();
        ClassData classStats = Resources.Load<ClassData>($"Classes/{elements[2]}");
        u_stats.unit_name = elements[1] == "" ?  $"{classStats.className}_{elements[0]}" : elements[1];
        switch(faction)
        {
            case Factions.player:
                u_stats.color = "b";
                break;
            case Factions.enemy:
                u_stats.color = "r";
                break;
            case Factions.friendly_other:
                u_stats.color = "g";
                break;
            case Factions.unfriendly_other:
                u_stats.color = "p";
                break;
        }
        u_stats.LoadExpAndLevel(Convert.ToInt32(elements[4]), Convert.ToInt32(elements[3]));

        //this is a little cheap n nasty.  It does the job, but the for loop is pretty ugly, and I feel like I can probably separate the class and stat concerns a bit better.
        
        u_stats.unit_class = classStats;
        u_stats.movementType = classStats.movementType;
        u_stats.unitStats[u_stats.unitStats.Length - 1] = classStats.MOV;
        for (int i = 5; i < UnitStats.statOrder.Length + 4; i++)
        {
            u_stats.unitStats[i-5] = Convert.ToInt32(elements[i]);
            if(i - 5 > classStats.statCaps.Length -1) {continue;}
            u_stats.statCaps[i-5] = classStats.statCaps[i-5];
        }
        u_stats.faction = faction;

        EditorUtility.SetDirty(u_stats);
    }

    public static void ParseGrowthStats()
    {
        string readPath = $"{Application.dataPath}/Resources/Data/units/unit_growths.csv";
        string[] readText = File.ReadAllLines(readPath);
        Debug.Log(readText);
        for (int i = 1; i<readText.Length; i++)
        {
            ParseGrowthStats(readText[i]);
        }
    }

    static void ParseGrowthStats (string line)
    {
        string[] elements = line.Split(',');
        GameObject obj = GetOrCreateGrowths(elements[0]);
        UnitStats u_stats = obj.GetComponent<UnitStats>();
        for (int i = 2; i < elements.Length-1; i++)
        {
            u_stats.growths[i-2] = Convert.ToInt32(elements[i]);
        }
        EditorUtility.SetDirty(u_stats);
    }

    static StatModifierFeature GetFeature (GameObject obj, StatTypes type)
    {
        StatModifierFeature[] smf = obj.GetComponents<StatModifierFeature>();
        for (int i = 0; i < smf.Length; ++i)
        {
            if (smf[i].type == type)
            {
                return smf[i];
            }
        }
        StatModifierFeature feature = obj.AddComponent<StatModifierFeature>();
        feature.type = type;
        return feature;
    }
    static GameObject GetOrCreate (string unitName, Factions faction)
    {
        string fullPath;
        switch(faction)
        {
            case Factions.enemy:
                fullPath = $"Assets/Resources/Units/enemy/{unitName}_stats.prefab";
                break;
            case Factions.friendly_other:
                fullPath = $"Assets/Resources/Units/friendly_other/{unitName}_stats.prefab";
                break;
            case Factions.unfriendly_other:
                fullPath = $"Assets/Resources/Units/unfriendly_other/{unitName}_stats.prefab";
                break;
            default:
                fullPath = $"Assets/Resources/Units/player/{unitName}_stats.prefab";
                break;
        }
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);
        if (obj == null)
        {
            obj = Create(fullPath);
        }
        return obj;
    }

    static GameObject GetOrCreateGrowths(string unitName)
    {
        string fullPath = $"Assets/Resources/Units/Player/{unitName}_stats.prefab";
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);
        if (obj == null)
        {
            obj = Create(fullPath);
        }
        return obj;
    }
    static GameObject Create (string fullPath)
    {
        GameObject instance = new GameObject ("temp");
        instance.AddComponent<UnitStats>();
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(instance, fullPath);
        GameObject.DestroyImmediate(instance);
        return prefab;
    }
}
