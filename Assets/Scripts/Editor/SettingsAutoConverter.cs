    using UnityEngine;
    using UnityEditor;
    using System;
    using System.IO;
    using System.Collections.Generic;
 
    public class SettingsAutoConverter : AssetPostprocessor
    {
      static Dictionary<string, Action> parsers; 
      static SettingsAutoConverter ()
      {
        parsers = new Dictionary<string, Action>();
        parsers.Add("units.csv", ParseUnits);
      }
      static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
      {
        for (int i = 0; i < importedAssets.Length; i++)
        {
          string fileName = Path.GetFileName( importedAssets[i] );
          if (parsers.ContainsKey(fileName))
          {
              parsers[fileName]();
          }
            
        }
        AssetDatabase.SaveAssets ();
        AssetDatabase.Refresh();
      }
      static void ParseUnits ()
      {
        string filePath = Application.dataPath + "/Resources/Data/units.csv";
        if (!File.Exists(filePath))
        {
          Debug.LogError("Missing unit Data: " + filePath);
          return;
        }
        string[] readText = File.ReadAllLines("Assets/Resources/Data/units.csv");
        filePath = "Assets/Resources/Units/";
        for (int i = 1; i < readText.Length; ++i)
        {
          UnitData unitData = ScriptableObject.CreateInstance<UnitData>();
          unitData.Load(readText[i]);
          string fileName = string.Format("{0}{1}.asset", filePath, unitData.unit_class);
          AssetDatabase.CreateAsset(unitData, fileName);
        }
      }
    }