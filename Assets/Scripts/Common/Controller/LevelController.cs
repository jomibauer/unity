using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelController : MonoBehaviour
{   
    [SerializeField]string levelName;
    [SerializeField]Level level;

    void Awake()
    {
        level = Resources.Load<Level>($"Levels/Components/{levelName}");
    }

    public void LoadUnitMaps()
    {
        level.LoadUnitMaps();
    }

    public Level GetLevel()
    {
        return level;
    }

    public Dictionary<Factions, Dictionary<Tile,Unit>> GetUnitMaps()
    {
        return level.unitMaps;
    }
}
