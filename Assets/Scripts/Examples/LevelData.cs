using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum LevelType {
    Tutorial,
    Normal,
    Hardmode
}

/// <summary>
/// Level data class that holds information of a level that can be then used to create UI
/// </summary>
[CreateAssetMenu(menuName = "Shade/LevelData")]
public class LevelData : ScriptableObject {
    [Header("Use either scene index or scene name to determine which scene is loaded for this level")]
    public int sceneIndex;
    public string sceneName;

    public string levelDisplayName;

    public LevelType levelType;


    // Calling this list will automatically load all assets from Resource-folders that are type
    // LevelData, if they haven't been loaded yet.
    private static List<LevelData> LevelDataList {
        get {
            if (s_levelDataList == null || s_levelDataList.Count <= 0) {
                s_levelDataList = Resources.LoadAll<LevelData>();
            }

            return s_levelDataList;
        }
    }
    private static List<LevelData> s_levelDataList;


    // Returns all level datas
    public static List<LevelData> GetAll() {
        return LevelDataList;
    }


    // Returns all level datas with given level type
    public static List<LevelData> GetAll(LevelType levelType) {
        return GetAll().FindAll(x => x.levelType == levelType);
    }
}
