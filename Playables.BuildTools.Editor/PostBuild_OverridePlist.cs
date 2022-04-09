using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class PostBuild_OverridePlist : IPostprocessBuildWithReport {

	
	public int callbackOrder => 0;

	public void OnPostprocessBuild(BuildReport report)
	{
		var defaultOverridesBasePath = "Packages/net.playables.buildtools/Defaults";
		var customOverrideBasePath = "Assets/Build";

		string plistPath = null;
		string defaultOverridesPath = null;
		string customOverridesPath = null;
		
		if (report.summary.platform == BuildTarget.iOS)
		{
			plistPath = report.summary.outputPath + "/Info.plist";
			defaultOverridesPath = $"{defaultOverridesBasePath}/iOS/Info.plist";
			customOverridesPath = $"{customOverrideBasePath}/iOS/Info.plist";
		}
		if ( report.summary.platform == BuildTarget.StandaloneOSX)
		{
			plistPath = report.summary.outputPath + "/Contents/Info.plist";
			defaultOverridesPath = $"{defaultOverridesBasePath}/macos/Info.plist";
			customOverridesPath = $"{customOverrideBasePath}/macos/Info.plist";
		}

		if (plistPath != null)
		{
			var plistString = File.ReadAllText(plistPath);
			PlistDocument plist = new PlistDocument();
			plist.ReadFromString(plistString);

			Debug.Log($"Overriding {plistPath} with default overrides from {defaultOverridesPath}");
			
			PlistDocument plistOverrides = new PlistDocument();
			plistOverrides.ReadFromString(File.ReadAllText(defaultOverridesPath));
			OverridePlist(plist, plistOverrides);
				
			if(File.Exists(customOverridesPath))
			{
				Debug.Log($"Overriding {plistPath} with values from {customOverridesPath}");
				PlistDocument plistCustomOverrides = new PlistDocument();
				plistCustomOverrides.ReadFromString(File.ReadAllText(customOverridesPath));
				OverridePlist(plist, plistCustomOverrides);
			}
			
			File.WriteAllText(plistPath, plist.WriteToString());
		}
	}


	static void OverridePlist(PlistDocument plist, PlistDocument plistOverrides)
	{
		foreach (var pair in plistOverrides.root.values)
		{
			plist.root[pair.Key] = pair.Value;
		}
	}
}