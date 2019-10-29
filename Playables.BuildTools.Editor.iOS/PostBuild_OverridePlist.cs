using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class PostBuild_OverridePlist : IPostprocessBuildWithReport {

	// Create an Info.plist file at this path with all the overrides to apply after building
	public const string overridesPath = "Assets/Build/iOS/Info.plist";	
	
	public const string defaultOverridesPath = "Packages/net.playables.buildtools/iOS/Info.plist";

	
	public int callbackOrder => 0;

	public void OnPostprocessBuild(BuildReport report)
	{
		if (report.summary.platform == BuildTarget.iOS)
		{
			var plistPath = report.summary.outputPath + "/Info.plist";
			var plistString = File.ReadAllText(plistPath);
			Debug.Log($"Overriding {plistPath} with default overrides from {defaultOverridesPath}");
			plistString = OverridePlist(plistString, File.ReadAllText(defaultOverridesPath));
			
			if(File.Exists(overridesPath))
			{
				//Debug.Log($"Overriding {plistPath} with values from {overridesPath}");
				plistString = OverridePlist(plistString, File.ReadAllText(overridesPath));
			}
			File.WriteAllText(plistPath, plistString);
		}
	}

	static string OverridePlist(string pListStr, string overridesPlistStr)
	{
		PlistDocument plist = new PlistDocument();
		plist.ReadFromString(pListStr);
		
		PlistDocument plistOverrides = new PlistDocument();
		plistOverrides.ReadFromString(overridesPlistStr);
		
		foreach (var pair in plistOverrides.root.values)
		{
			plist.root[pair.Key] = pair.Value;
		}

		return plist.WriteToString();
	}
}