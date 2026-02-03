using UnityEditor;
using UnityEngine;

public static class BuildBundles
{
    [MenuItem("Build/Build Asset Bundles")]
    public static void BuildAllBundles()
    {
        var path = Application.streamingAssetsPath;

        BuildPipeline.BuildAssetBundles(
            path,
            BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget
        );

        Debug.Log("All bundles built!");
    }
}