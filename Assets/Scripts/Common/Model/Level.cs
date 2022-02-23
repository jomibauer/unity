using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string levelName;
    public int height;
    public int length;
    public TileSet tileSet;
    public Dictionary<Factions, Dictionary<Tile, Unit>> unitMaps;
    //public Dictionary<Factions, Dictionary<Tile,Unit>> unitMaps;

    public void LoadUnitMaps()
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

        unitMaps = res;
    }

    private static Dictionary<Tile, Unit> ParseUnitGrid(string[] grid, string faction)
    {
        GameObject genericUnit = Resources.Load($"Prefabs/Unit/GenericUnit") as GameObject;
        Dictionary<Tile, Unit> unitGrid = new Dictionary<Tile, Unit>();
        for(int y = grid.Length - 1; y >= 0; --y)
        {
            int realRowIndex = Mathf.Abs(y - (grid.Length - 1));
            string[] row = grid[y].Split(',');
            for(int x = 0; x < row.Length; ++x)
            {
                if(row[x] == "") { continue; }
                //UnitStats unitStats = AssetDatabase.LoadAssetAtPath<UnitStats>($"Assets/Resources/Units/{faction}/{row[x]}_stats.prefab");
                GameObject tempUnitObj = Instantiate(genericUnit, new Vector3(0,0,0) ,Quaternion.identity);
                Unit tempUnit = tempUnitObj.GetComponentInChildren<Unit>();
                tempUnit.dataPath = $"{faction}/{row[x]}";

                Debug.Log($"[Level.ParseUnitGrid]: {tempUnit}");
                unitGrid[new Tile(x, realRowIndex)] = tempUnit;
            }
        }
        Destroy(genericUnit);
        return unitGrid;
    }
}
