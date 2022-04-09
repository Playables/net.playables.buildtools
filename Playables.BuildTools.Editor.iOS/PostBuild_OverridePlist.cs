using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class PostBuild_IOS_OverridePlist : IPostprocessBuildWithReport {
	
	public int callbackOrder => 100;

	public void OnPostprocessBuild(BuildReport report)
	{

		if (report.summary.platform == BuildTarget.iOS)
		{
			var overridePlistPath = "Assets/Build/iOS/Info.plist";
		

		if ( File.Exists(overridePlistPath))
		{
			PlistDocument plist = InfoPlistUtils.GetInfoPlistFromBuildReport(report);
			PlistDocument overrides = InfoPlistUtils.GetPlistFromFile(overridePlistPath);
				
			Debug.Log($"Overriding {InfoPlistUtils.GetInfoPlistPath(report)} with values from {overridePlistPath}");
			InfoPlistUtils.OverridePlist(plist, overrides);

			InfoPlistUtils.WriteInfoPlistForBuildReport(report, plist);
		}
		
		}
	}
}