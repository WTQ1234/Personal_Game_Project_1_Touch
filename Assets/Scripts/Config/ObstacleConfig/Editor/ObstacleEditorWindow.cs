#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using System.Linq;
using HRL;
using Sirenix.OdinInspector;

public class ObstacleEditorWindow : BasicConfigWindow
{
    private static string mFileName_Obstacle = "Obstacle[{0}]";
    private static string mAssetPath_Obstacle = "Assets/Resources/ScriptableObject/ObstacleInfo";
    private static string mTitle_AllAssets_Obstacle = "1.所有障碍";

    [MenuItem("配置/主流程/障碍")]
    private static void Open()
    {
        var window = GetWindow<ObstacleEditorWindow>();
        // 设置标题
        GUIContent titleContent = new GUIContent();
        titleContent.text = "障碍配置";
        window.titleContent = titleContent;
        // 设置位置
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        // 添加基础配置
        if (!AssetPath.ContainsKey("属性路径"))
        {
            AssetPath.Add("默认数据名", mFileName_Obstacle);
            AssetPath.Add("属性路径", mAssetPath_Obstacle);
        }
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        base.BuildMenuTree();
        // 浏览当前所有属性
        ObstacleOverview.Instance.UpdateOverview();
        // 将具体属性添加到列表
        if (ObstacleOverview.Instance.AllInfos.Length > 0)
        {
            mCurTree.Add(mTitle_AllAssets_Obstacle, new BasicInfoTable<ObstacleInfo>(ObstacleOverview.Instance.AllInfos));
            mCurTree.AddAllAssetsAtPath(mTitle_AllAssets_Obstacle, mAssetPath_Obstacle, typeof(ObstacleInfo), true, true);
        }
        // 后续处理
        AfterCreateBuildMenuTree();
        return mCurTree;
    }

    protected override void OnBeginDrawEditors()
    {
        if (this.MenuTree == null)
        {
            return;
        }
        var selected = this.MenuTree?.Selection?.FirstOrDefault();
        var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;
        // 绘制工具栏
        SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
        {
            if (selected != null)
            {
                GUILayout.Label(selected.Name);
            }
            if (SirenixEditorGUI.ToolbarButton(new GUIContent("选中当前文件")))
            {
                SelectCurAssetFile();
            }
            if (SirenixEditorGUI.ToolbarButton(new GUIContent("创建新障碍配置")))
            {
                int assetNumber = FindAssetNumber(mAssetPath_Obstacle, mFileName_Obstacle);
                Debug.Log(assetNumber);
                string curFileName = string.Format(mFileName_Obstacle, assetNumber);
                ScriptableObjectCreator.ShowDialog<ObstacleInfo>(mAssetPath_Obstacle, curFileName, (obj, fileName) =>
                {
                    obj.Id = assetNumber;
                    obj.Name = obj.name;
                    obj.FileName = fileName;
                    obj.InitAfterCreateFile();
                    base.TrySelectMenuItemWithObject(obj);
                });
            }
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }
}
#endif
