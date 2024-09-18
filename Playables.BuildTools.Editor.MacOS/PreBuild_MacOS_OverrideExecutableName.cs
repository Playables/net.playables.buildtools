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
	bool isOverrideActive = false;

	public void OnPreprocessBuild(BuildReport report)
	{
		if (report.summary.platform != BuildTarget.StandaloneOSX)
			return;

		var buildToolsSettings = BuildToolsSettings.GetOrCreateSettings();
		if (buildToolsSettings.macosOverrideExecutableName)
		{
			productName = PlayerSettings.productName;
			var executableName = buildToolsSettings.macosExecutableName;
			Debug.Log($"Set macos executable name to {executableName} (instead of product name {productName})");
			PlayerSettings.productName = executableName;
			isOverrideActive = true;
		}
	}

	public void OnPostprocessBuild(BuildReport report)
	{
		if (!isOverrideActive)
			return;

		PlayerSettings.productName = productName;

		Debug.Log($"Updating Info.plist to use original product name ({productName})");
		var plistPath = InfoPlistUtils.GetPlistPath(report);
		InfoPlistUtils.WritePlistKey(plistPath, InfoPlistKeys_MacOS.CFBundleName, new PlistElementString(productName));

		productName = null;
		isOverrideActive = false;
	}
}