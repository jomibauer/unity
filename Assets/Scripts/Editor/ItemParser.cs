using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class ItemParser
{
    [MenuItem("Pre Production/Parse Item Stats")]
    public static void Parse()
    {
        CreateDirectories();
        ParseWeapons();
        //ParseAccessories();
        //ParseConsumables();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    static void CreateDirectories()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Items"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Items");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Items/Weapons"))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Items", "Weapons");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Items/Consumables"))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Items", "Consumables");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Items/Accessories"))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Items", "Accessories");
        }
    }

    public static void ParseWeapons()
    {
        string readPath = $"{Application.dataPath}/Resources/Data/Weapons.csv";
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i<readText.Length; i++)
        {
            ParseWeapon(readText[i]);
        }
    }

    static void ParseWeapon(string line)
    {
        WeaponData weapon = ScriptableObject.CreateInstance<WeaponData>();
        weapon.Load(line);

        string filePath = "Assets/Resources/Items/Weapons/";
        string fileName = $"{filePath}{weapon.wpnName}.asset";

        AssetDatabase.CreateAsset(weapon, fileName);
    }

    static GameObject GetOrCreate (string itemType, string unitName)
    {
        string fullPath = $"Assets/Resources/Items/{itemType}/{unitName}.prefab";
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
        instance.AddComponent<WeaponStats>();
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(instance, fullPath);
        GameObject.DestroyImmediate(instance);
        return prefab;
    }
}
