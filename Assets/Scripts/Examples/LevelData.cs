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
}
