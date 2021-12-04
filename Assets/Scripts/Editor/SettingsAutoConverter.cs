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
        
        parsers.Add("weapons.csv", ItemParser.ParseWeapons);
        parsers.Add("inventories.csv", ItemParser.ParseInventories);
        parsers.Add("units_stats.csv", UnitParser.ParsePlayerUnitStats);
        parsers.Add("unit_growths.csv", UnitParser.ParseGrowthStats);
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
      
    }