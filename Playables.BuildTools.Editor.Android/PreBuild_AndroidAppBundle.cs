using Playables.BuildTools;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PreBuild_AndroidAppBundle : IPostprocessBuildWithReport, IPreprocessBuildWithReport
{
	bool? oldUseBundle;
	
	public int callbackOrder { get; }
	
	public void OnPreprocessBuild(BuildReport report)
	{
		//var args = new string[]{"-android_app_bundle","1"};
		var args = System.Environment.GetCommandLineArgs();

		if(!CommandlineParseUtils.HasArgument(args,"-android_app_bundle"))
			return;
		
		var useBundleStr = CommandlineParseUtils.GetArgumentData(args, "-android_app_bundle");
		var useBundle = int.Parse(useBundleStr) == 1;

		switch (report.summary.platform)
		{
			case BuildTarget.Android:
				oldUseBundle = EditorUserBuildSettings.buildAppBundle;
				EditorUserBuildSettings.buildAppBundle = useBundle;
				Debug.Log($"Temporarily set buildAppBundle to {useBundle}");
				break;
		}
	}

	public void OnPostprocessBuild(BuildReport report)
	{
		if (oldUseBundle.HasValue)
		{
			Debug.Log($"Revert buildAppBundle to {oldUseBundle.Value}");
			EditorUserBuildSettings.buildAppBundle = oldUseBundle.Value;
			AssetDatabase.SaveAssets();
			oldUseBundle = null;
		}
	}
}