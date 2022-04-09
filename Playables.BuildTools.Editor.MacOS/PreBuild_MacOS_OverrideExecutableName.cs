using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class PreBuild_MacOS_OverrideExecutableName : IPostprocessBuildWithReport, IPreprocessBuildWithReport
{
	public int callbackOrder => 0;

	string productName;

	public void OnPreprocessBuild(BuildReport report)
	{
		if (report.summary.platform == BuildTarget.StandaloneOSX)
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
	}

	public void OnPostprocessBuild(BuildReport report)
	{
		if (report.summary.platform == BuildTarget.StandaloneOSX)
		{
			if (BuildToolsSettings.GetOrCreateSettings().macosOverrideExecutableName)
			{
				PlayerSettings.productName = productName;

				Debug.Log($"Updating Info.plist to use original product name ({productName})");
				InfoPlistUtils.WriteInfoPlistKeyForBuildReport(report, InfoPlistKeys_MacOS.CFBundleName, new PlistElementString(productName));

				productName = null;
			}
		}
	}
}