using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class ClassParser
{
    [MenuItem("Pre Production/Parse Classes")]
    public static void Parse()
    {
        CreateDirectories();
        ParseClasses();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static void CreateDirectories()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Classes"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Classes");
        }
    }

    private static void ParseClasses()
    {
        string readPath = $"{Application.dataPath}/Resources/Data/classes.csv";
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i<readText.Length; i++)
        {
            ParseClass(readText[i]);
        }
    }

    private static void ParseClass(string row)
    {
        ClassData classData = ScriptableObject.CreateInstance<ClassData>();
        
        classData.Load(row);
        
        AssetDatabase.CreateAsset(classData, $"Assets/Resources/Classes/{classData.dataName}.asset");
        EditorUtility.SetDirty(classData);
    }
}
