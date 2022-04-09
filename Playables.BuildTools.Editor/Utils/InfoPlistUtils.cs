using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;

public static class InfoPlistUtils
{
	public static void SetRootKey<T>(PlistDocument plist, PlistKey<T> key, T element) where T : PlistElement
	{
		plist.root[key.key] = element;
	}

	public static string GetInfoPlistPath(BuildReport report)
	{
		switch (report.summary.platform)
		{
			case BuildTarget.iOS:
				return report.summary.outputPath + "/Info.plist";
			case BuildTarget.StandaloneOSX:
				return report.summary.outputPath + "/Contents/Info.plist";
			default:
				return null;
		}
	}

	public static void OverridePlist(PlistDocument plist, PlistDocument plistOverrides)
	{
		foreach (var pair in plistOverrides.root.values)
		{
			plist.root[pair.Key] = pair.Value;
		}
	}

	public static PlistDocument GetPlistFromFile(string path)
	{
		PlistDocument plist = new PlistDocument();
		plist.ReadFromString(File.ReadAllText(path));

		return plist;
	}

	public static PlistDocument GetInfoPlistFromBuildReport(BuildReport report)
	{
		return GetPlistFromFile(GetInfoPlistPath(report));
	}

	public static void WriteInfoPlistForBuildReport(BuildReport report, PlistDocument plist)
	{
		File.WriteAllText(GetInfoPlistPath(report), plist.WriteToString());
	}

	public static void WriteInfoPlistKeyForBuildReport<T>(BuildReport report, PlistKey<T> key, T plistElement) where T : PlistElement
	{
		var plist = GetInfoPlistFromBuildReport(report);
		SetRootKey(plist, key, plistElement);
		WriteInfoPlistForBuildReport(report, plist);
	}
}