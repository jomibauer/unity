using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="New Level", menuName ="Level")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public int height;
    public int length;
    public TileSet tileSet;
    

}
