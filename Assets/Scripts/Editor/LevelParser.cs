using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class LevelParser 
{
    [MenuItem("Pre Production/Parse Levels")]
    public static void Parse()
    {
        CreateDirectories();
        ParseLevels();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

   

    static void CreateDirectories()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Levels"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        }
        
    }
    //parse a level by name

    //get actual Level from a csv named after the level in resources.data.levels.{levelName}.grid
    // -- this should have a csv representation of the level with numbers.  From this, our levelData can fill in its grid, TileInfoMap, and 
    // length and height

    //get special contents (locked doors, chests) from csv named after the level in resources.data.levels.{levelName}.contents
    // -- this should be a csv shaped like the grid one, but this should be empty except for the cells that contain stuff.  Those cells should contain
    // the name of the prefab that theyre going to instantiate as the contents of the TileInfo at that spot.

    //then we need to load the level tileset.

    private static void ParseLevels()
    {
        string readPath = $"{Application.dataPath}/Resources/Data/levels/levels.csv";
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i<readText.Length; i++)
        {
            ParseLevel(readText[i]);
        }
    }

    private static void ParseLevel(string row)
    {
        //Init level data and split row 
        LevelData level = ScriptableObject.CreateInstance<LevelData>();
        string[] elements = row.Split(',');

        //set name and tileset
        level.levelName = elements[0];
        string tileSetPath = $"Assets/Resources/Tilesets/{elements[1]}.asset";
        Debug.Log(tileSetPath);
        level.tileSet = AssetDatabase.LoadAssetAtPath<TileSet>(tileSetPath);

        //use name to get the level's grid and content csvs.
        string gridPath = $"{Application.dataPath}/Resources/Data/levels/grids/{level.levelName}.csv";
        string[] readGrid = File.ReadAllLines(gridPath);
        string contentsPath = $"{Application.dataPath}/Resources/Data/levels/contents/{level.levelName}.csv";
        string[] readContent = File.ReadAllLines(contentsPath);

        //grid/contents.csv are the same dimensions as the level.  Use the size of the arrays to set the level's height + length.
        level.height = readGrid.Length;
        level.length = readGrid[0].Split(',').Length;
        
        AssetDatabase.CreateAsset(level, $"Assets/Resources/Levels/{level.levelName}.asset");
        EditorUtility.SetDirty(level);
        
    }
}
