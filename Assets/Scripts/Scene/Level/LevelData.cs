using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    /// <summary>
    /// When move between the scenes we need to keep LevelData to show Player result
    /// </summary>
    public static LevelData Instance { get; private set; }

    public float Score = 0;
    public int SceneID;
    
    public LevelData()
    {
        Instance = this;
    }
}
