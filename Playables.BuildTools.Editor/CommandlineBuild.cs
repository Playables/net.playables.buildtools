using System;
using System.Text;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

// used for fastlane builds
public static class CommandlineBuild
{
	public static void Build()
	{
		//var args = new string[]{"-buildtarget","IOS","-buildpath","Builds/ios"};
		var args = Environment.GetCommandLineArgs();
		
		var cancel = false;

		var buildTarget = CommandlineParseUtils.GetArgumentData(args, "-buildtarget");
		if(string.IsNullOrEmpty(buildTarget))
		{
			Debug.LogWarning("no build platform set (use \"-buildtarget <buildtarget>\")");
			cancel = true;
		}
		
		var buildPath = CommandlineParseUtils.GetArgumentData(args, "-buildpath");
		if(string.IsNullOrEmpty(buildPath))
		{
			Debug.LogWarning("no build path set (use \"-buildpath <platform>\")");
			cancel = true;
		}
		
		if(cancel)
		{
			Debug.LogWarning("Build canceled (not enough information)");
			return;
		}
		
		var buildTargetParsed = (BuildTarget) Enum.Parse(typeof(BuildTarget), buildTarget, true);
		
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.locationPathName = buildPath;
		buildPlayerOptions.target = buildTargetParsed;
		buildPlayerOptions.scenes = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);
		buildPlayerOptions.options = BuildOptions.None;

		Debug.Log($"Starting build: buildTarget='{buildTargetParsed}' buildPath='{buildPath}'");
		
		BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
		BuildSummary summary = report.summary;
		
		
		if (summary.result == BuildResult.Succeeded)
		{
			Debug.Log($"Build succeeded: {(summary.totalSize * 1e-6)} MB {summary.totalTime:hh\\:mm\\:ss}");
		}

		if (summary.result == BuildResult.Failed)
		{
			throw new UnityException("Build failed"); //throw exception to make sure exit code is not 0
		}
	}
}