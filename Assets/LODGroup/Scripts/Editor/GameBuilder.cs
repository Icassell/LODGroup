using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public static class GameBuilder 
{

    [MenuItem("Tools/BuildWindowsDevelopment")]
    public static void BuildWindowsDevelopment()
    {
        var buildOptions = BuildOptions.None;
        buildOptions |= BuildOptions.Development;
        buildOptions |= BuildOptions.AllowDebugging;
        buildOptions |= BuildOptions.EnableDeepProfilingSupport;

        var buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = GetBuildScenes(),
            targetGroup =BuildTargetGroup.Standalone,
            target = BuildTarget.StandaloneWindows64,
            locationPathName = Directory.GetCurrentDirectory()+"/Build/LodGroupStreaming.exe",
            options = buildOptions
        };
        
        BuildPlayer(buildPlayerOptions);
    }


    private static void BuildPlayer(BuildPlayerOptions buildPlayerOptions)
    {
        AddressableAssetSettings.CleanPlayerContent();
        AddressableAssetSettings.BuildPlayerContent();
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
    
    
    
    private static string[] GetBuildScenes()
    {
        var scenes = EditorBuildSettings.scenes;
        var sceneList = (from scene in scenes where scene.enabled select scene.path).ToList();

        if (sceneList.Count >= 1) return sceneList.ToArray();
        Debug.LogError("No Scene To Build,Go To BuildSetting!");
        return new[] {""};
    }
}
