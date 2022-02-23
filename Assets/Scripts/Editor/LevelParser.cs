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
            ParseLevel_obj(readText[i]);
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

    private static void ParseLevel_obj(string row)
    {
        string[] elements = row.Split(',');
        GameObject obj = GetOrCreate(elements[0]);

        Level level = obj.GetComponent<Level>();

        //set name and tileset
        level.levelName = elements[0];
        string tileSetPath = $"Assets/Resources/Tilesets/{elements[1]}.asset";

        level.tileSet = AssetDatabase.LoadAssetAtPath<TileSet>(tileSetPath);

        //use name to get the level's grid and content csvs.
        string gridPath = $"{Application.dataPath}/Resources/Data/levels/grids/{level.levelName}.csv";
        string[] readGrid = File.ReadAllLines(gridPath);
        string contentsPath = $"{Application.dataPath}/Resources/Data/levels/contents/{level.levelName}.csv";
        string[] readContent = File.ReadAllLines(contentsPath);

        //grid/contents.csv are the same dimensions as the level.  Use the size of the arrays to set the level's height + length.
        level.height = readGrid.Length;
        level.length = readGrid[0].Split(',').Length;
        //AssetDatabase.CreateAsset(level, $"Assets/Resources/Levels/{level.levelName}.asset");
        EditorUtility.SetDirty(level);
    }

    private static GameObject GetOrCreate(string levelName)
    {
        string fullPath = $"Assets/Resources/Levels/Components/{levelName}.prefab";
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);
        if (obj == null)
        {
            obj = Create(fullPath);
        }
        return obj;
    }

    private static GameObject Create(string fullPath)
    {
        GameObject instance = new GameObject ("temp");
        instance.AddComponent<Level>();
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(instance, fullPath);
        GameObject.DestroyImmediate(instance);
        return prefab;
    }

    private static Dictionary<Factions, Dictionary<Tile,Unit>> ParseUnitMaps(string levelName)
    {
        Dictionary<Factions, Dictionary<Tile,Unit>> res = new Dictionary<Factions, Dictionary<Tile,Unit>>();
        foreach(var faction in EnumExtensions.GetValues<Factions>())
        {
            if(faction == Factions.None) { continue; }
            string mapPath;
            switch(faction)
            {
                case Factions.enemy:
                    mapPath = $"Assets/Resources/Data/levels/units/enemies/{levelName}/enemy_grid.csv";
                    break;
                case Factions.friendly_other:
                    mapPath = $"Assets/Resources/Data/levels/units/friendly_others/{levelName}/friendly_others_grid.csv";
                    break;
                case Factions.unfriendly_other:
                    mapPath = $"Assets/Resources/Data/levels/units/unfriendly_others/{levelName}/unfriendly_others_grid.csv";
                    break;
                default:
                    mapPath = $"Assets/Resources/Data/levels/units/player/{levelName}/player_grid.csv";
                    break;
            }

            Dictionary<Tile, Unit> unitGrid = new Dictionary<Tile, Unit>();
            try {
                string[] grid = File.ReadAllLines(mapPath);
                unitGrid = ParseUnitGrid(grid, faction.ToString());
            }
            catch (DirectoryNotFoundException e) 
            {
                Debug.Log($"[LevelParser.cs]: Directory not found at {mapPath} \n Continuing to create level without units for {faction.ToString()}");
                unitGrid = new Dictionary<Tile, Unit>();
            }
            res[faction] = unitGrid.Count > 0 ? unitGrid : null;
        }

        return res;
    }

    private static Dictionary<Tile, Unit> ParseUnitGrid(string[] grid, string faction)
    {
        Dictionary<Tile, Unit> unitGrid = new Dictionary<Tile, Unit>();
        for(int y = grid.Length - 1; y >= 0; --y)
        {
            int realRowIndex = Mathf.Abs(y - (grid.Length - 1));
            string[] row = grid[y].Split(',');
            for(int x = 0; x < row.Length; ++x)
            {
                if(row[x] == "") { continue; }
                unitGrid[new Tile(x, realRowIndex)] = AssetDatabase.LoadAssetAtPath<Unit>($"Assets/Resources/Units/{faction}/{row[x]}.prefab");
            }
        }
        return unitGrid;
    }
}
