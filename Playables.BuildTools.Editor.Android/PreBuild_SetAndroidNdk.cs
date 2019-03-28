using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

// This is a fix for burst complier errors (ANDROID_NDK_ROOT not found)
public class PreBuild_SetAndroidNdk : IPreprocessBuildWithReport
{
	public int callbackOrder { get; }
	public void OnPreprocessBuild(BuildReport report)
	{
		if (report.summary.platform == BuildTarget.Android)
		{
			var ndkHome = EditorPrefs.GetString("AndroidNdkRootR16b");
			EditorPrefs.SetString("AndroidNdkRoot",ndkHome);
		}
	}
}