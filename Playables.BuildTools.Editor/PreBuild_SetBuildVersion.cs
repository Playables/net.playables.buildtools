using Playables.BuildTools;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

// sets the bundleVersionCode before building based on command line "-buildversion" arg
// currently implemented for android only
public class PreBuild_SetBuildVersion : IPostprocessBuildWithReport, IPreprocessBuildWithReport
{
	int? oldBuildVersion;
	
	public int callbackOrder { get; }
	
	public void OnPreprocessBuild(BuildReport report)
	{
		//var args = new string[]{"-buildversion","15"};
		var args = System.Environment.GetCommandLineArgs();

		if(!CommandlineParseUtils.HasArgument(args,"-buildversion"))
			return;
		
		var buildVersionStr = CommandlineParseUtils.GetArgumentData(args, "-buildversion");
		var buildVersion = int.Parse(buildVersionStr);

		switch (report.summary.platform)
		{
			case BuildTarget.Android:
				oldBuildVersion = PlayerSettings.Android.bundleVersionCode;
				PlayerSettings.Android.bundleVersionCode = buildVersion;
				Debug.Log($"Temporarily set build version to {buildVersion}");
				break;
		}
	}

	public void OnPostprocessBuild(BuildReport report)
	{
		if (oldBuildVersion.HasValue)
		{
			Debug.Log($"Revert build version to {oldBuildVersion.Value}");
			PlayerSettings.Android.bundleVersionCode = oldBuildVersion.Value;
			AssetDatabase.SaveAssets();
			oldBuildVersion = null;
		}
	}
}