using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class PostBuild_OverridePlist : IPostprocessBuildWithReport
{
	public int callbackOrder => 100;

	public void OnPostprocessBuild(BuildReport report)
	{
		var plistPath = InfoPlistUtils.GetPlistPath(report);

		if (plistPath == null)
			return;

		var overridePlistPath = report.summary.platform switch
		{
			BuildTarget.iOS => "Assets/Build/iOS/Info.plist",
			BuildTarget.StandaloneOSX => "Assets/Build/macOS/Info.plist",
			_ => null
		};

		if (File.Exists(overridePlistPath))
		{
			PlistDocument plist = InfoPlistUtils.ReadPlist(plistPath);
			PlistDocument overrides = InfoPlistUtils.ReadPlist(overridePlistPath);

			Debug.Log($"Overriding {plistPath} with values from {overridePlistPath}");
			InfoPlistUtils.OverridePlist(plist, overrides);

			InfoPlistUtils.WritePlist(plist, plistPath);
		}
	}
}