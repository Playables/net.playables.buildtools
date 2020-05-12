using Playables.BuildTools;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PreBuild_AndroidObb : IPostprocessBuildWithReport, IPreprocessBuildWithReport
{
	bool? oldUseObb;
	
	public int callbackOrder { get; }
	
	public void OnPreprocessBuild(BuildReport report)
	{
		//var args = new string[]{"-obb","0"};
		var args = System.Environment.GetCommandLineArgs();

		if(!CommandlineParseUtils.HasArgument(args,"-obb"))
			return;
		
		var useObbStr = CommandlineParseUtils.GetArgumentData(args, "-obb");
		var useObb = int.Parse(useObbStr) == 1;

		switch (report.summary.platform)
		{
			case BuildTarget.Android:
				oldUseObb = PlayerSettings.Android.useAPKExpansionFiles;
				PlayerSettings.Android.useAPKExpansionFiles = useObb;
				Debug.Log($"Temporarily set useAPKExpansionFiles to {useObb}");
				break;
		}
	}

	public void OnPostprocessBuild(BuildReport report)
	{
		if (oldUseObb.HasValue)
		{
			Debug.Log($"Revert useAPKExpansionFiles to {oldUseObb.Value}");
			PlayerSettings.Android.useAPKExpansionFiles = oldUseObb.Value;
			AssetDatabase.SaveAssets();
			oldUseObb = null;
		}
	}
}