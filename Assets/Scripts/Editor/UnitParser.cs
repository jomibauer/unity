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

    static void ParseUnitStats(string line, Factions faction)
    {
        string[] elements = line.Split(',');
        GameObject obj = GetOrCreate(elements[0]);
        UnitStats u_stats = obj.GetComponent<UnitStats>();
        u_stats.unit_name = elements[0];
        u_stats.unit_class = elements[1];
        u_stats.movementType = (MovementTypes)Convert.ToInt32(elements[2]);
        for (int i = 3; i < UnitStats.statOrder.Length + 3; i++)
        {
            u_stats.startingStats[i-3] = Convert.ToInt32(elements[i]);
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
        GameObject obj = GetOrCreate(elements[0]);
        UnitStats u_stats = obj.GetComponent<UnitStats>();
        for (int i = 1; i < elements.Length-1; i++)
        {
            u_stats.growths[i-1] = Convert.ToInt32(elements[i]);
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
    static GameObject GetOrCreate (string unitName)
    {
        string fullPath = $"Assets/Resources/Units/{unitName}_stats.prefab";
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
