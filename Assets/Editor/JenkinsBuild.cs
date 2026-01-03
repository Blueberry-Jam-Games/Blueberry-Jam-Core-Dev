using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

using System.Collections.Generic;

public class JenkinsBuild : Editor
{
    [MenuItem("Blueberry Jam/Build/Windows")]
    public static void BuildWindows()
    {
        BuildPlayerOptions build_player_options = new BuildPlayerOptions();
        List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);

        string[] scenes_from_settings = new string[scenes.Count];
        for (int i = 0; i < scenes.Count; i++)
        {
            scenes_from_settings[i] = scenes[i].path;
            //Debug.Log("scene: " + scenes[i].path);
        }

        build_player_options.scenes = scenes_from_settings;
        build_player_options.locationPathName = "Build/Windows/Blueberry-Jam-Core.exe";
        build_player_options.target = BuildTarget.StandaloneWindows64;

        build_player_options.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(build_player_options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Windows Build succeeded");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Windows Build failed");
        }
    }

    [MenuItem("Blueberry Jam/Build/WebGL")]
    public static void BuildWebGL()
    {
        BuildPlayerOptions build_player_options = new BuildPlayerOptions();
        List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);

        string[] scenes_from_settings = new string[scenes.Count];
        for (int i = 0; i < scenes.Count; i++)
        {
            scenes_from_settings[i] = scenes[i].path;
        }

        build_player_options.scenes = scenes_from_settings;
        build_player_options.locationPathName = "Build/WebGL/Blueberry-Jam-Core";
        build_player_options.target = BuildTarget.WebGL;

        build_player_options.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(build_player_options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("WebGL Build succeeded");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("WebGL Build failed");
        }
    }

    [MenuItem("Blueberry Jam/Build/Linux")]
    public static void BuildLinux()
    {
        BuildPlayerOptions build_player_options = new BuildPlayerOptions();
        List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);

        string[] scenes_from_settings = new string[scenes.Count];
        for (int i = 0; i < scenes.Count; i++)
        {
            scenes_from_settings[i] = scenes[i].path;
        }

        build_player_options.scenes = scenes_from_settings;
        build_player_options.locationPathName = "Build/Linux/Blueberry-Jam-Core";
        build_player_options.target = BuildTarget.StandaloneLinux64;

        build_player_options.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(build_player_options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Another Test Connor");
            Debug.Log("Testing" + string.IsNullOrEmpty(summary.outputPath));
            Debug.Log("Linux Build succeeded" + summary.outputPath);
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Linux Build failed");
        }
    }
}
