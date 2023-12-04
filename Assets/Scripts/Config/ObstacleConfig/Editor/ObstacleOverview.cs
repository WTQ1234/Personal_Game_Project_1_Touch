using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.Utilities;
using Sirenix.OdinInspector;

[GlobalConfig("Assets/Resources/ScriptableObject/EditorOverviews")]
public class ObstacleOverview : GlobalConfig<ObstacleOverview>
{
    [ReadOnly]
    [ListDrawerSettings(Expanded = true)]
    public ObstacleInfo[] AllInfos;

    [Button(ButtonSizes.Medium), PropertyOrder(-1)]
    public void UpdateOverview()
    {
        // Finds and assigns all scriptable objects of type Character
        this.AllInfos = AssetDatabase.FindAssets("t:ObstacleInfo")
            .Select(guid => AssetDatabase.LoadAssetAtPath<ObstacleInfo>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToArray();
    }
}
