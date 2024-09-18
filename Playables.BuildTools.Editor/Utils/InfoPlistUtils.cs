using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public static class InfoPlistUtils
{
	public static void SetRootKey<T>(PlistDocument plist, PlistKey<T> key, T element) where T : PlistElement
	{
		plist.root[key.key] = element;
	}


	public static void OverridePlist(PlistDocument plist, PlistDocument plistOverrides)
	{
		foreach (var pair in plistOverrides.root.values)
		{
			plist.root[pair.Key] = pair.Value;
		}
	}
	
	public static void WritePlistKey<T>(string path, PlistKey<T> key, T plistElement) where T : PlistElement
	{
		var plist = ReadPlist(path);
		SetRootKey(plist, key, plistElement);
		WritePlist( plist,path);
	}

	public static PlistDocument ReadPlist(string path)
	{
		PlistDocument plist = new PlistDocument();
		plist.ReadFromString(File.ReadAllText(path));

		return plist;
	}

	public static void WritePlist(PlistDocument plist,string path)
	{
		File.WriteAllText(path, plist.WriteToString());
	}

	public static string GetPlistPath(BuildTarget platform)
	{
		return platform switch
		{
			BuildTarget.iOS => $"/Info.plist",
			BuildTarget.StandaloneOSX => $"/Contents/Info.plist",
			_ => null
		};
	}

	public static string GetPlistPath(BuildReport report)
	{
		return $"{report.summary.outputPath}{GetPlistPath(report.summary.platform)}";
		
	}
}