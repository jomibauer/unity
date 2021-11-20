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
        ParseStartingStats();
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

    public static void ParseStartingStats()
    {
        string readPath = $"{Application.dataPath}/Resources/Data/units_starting_stats.csv";
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i<readText.Length; i++)
        {
            ParseStartingStats(readText[i]);
        }
    }

    static void ParseStartingStats(string line)
    {
        string[] elements = line.Split(',');
        GameObject obj = GetOrCreate(elements[0]);
        UnitStats u_stats = obj.GetComponent<UnitStats>();
        u_stats.unit_name = elements[0];
        u_stats.unit_class = elements[1];
        for (int i = 2; i < UnitStats.statOrder.Length + 2; i++)
        {
            u_stats.startingStats[i-2] = Convert.ToInt32(elements[i]);
        }
    }

    public static void ParseGrowthStats()
    {

        string readPath = $"{Application.dataPath}/Resources/Data/unit_growths.csv";
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
        string fullPath = $"Assets/Resources/Units/{unitName}.prefab";
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
