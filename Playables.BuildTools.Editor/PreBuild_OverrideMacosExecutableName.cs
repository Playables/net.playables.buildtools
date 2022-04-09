using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PreBuild_OverrideMacosExecutableName : IPostprocessBuildWithReport, IPreprocessBuildWithReport
{
	public int callbackOrder { get; }

	string productName;
	
	public void OnPreprocessBuild(BuildReport report)
	{
		var buildToolsSettings = BuildToolsSettings.GetOrCreateSettings();
		if (buildToolsSettings.macosOverrideExecutableName)
		{
			productName = PlayerSettings.productName;
			var executableName = buildToolsSettings.macosExecutableName;
			Debug.Log($"Set macos executable name to {executableName} (instead of product name {productName})");
			PlayerSettings.productName = executableName;
		}
	}

	public void OnPostprocessBuild(BuildReport report)
	{
		if (BuildToolsSettings.GetOrCreateSettings().macosOverrideExecutableName)
		{
			PlayerSettings.productName = productName;
			productName = null;
		}
	}
}