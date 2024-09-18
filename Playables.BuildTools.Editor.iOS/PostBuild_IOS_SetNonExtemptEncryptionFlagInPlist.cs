using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;

public class PostBuild_IOS_SetNonExtemptEncryptionFlagInPlist : IPostprocessBuildWithReport
{
	public int callbackOrder => -100;

	public void OnPostprocessBuild(BuildReport report)
	{
		if(report.summary.platform == BuildTarget.iOS)
		{
			var plistPath = InfoPlistUtils.GetPlistPath(report);
			InfoPlistUtils.WritePlistKey(plistPath, InfoPlistKeys_IOS.ITSAppUsesNonExemptEncryption, new PlistElementBoolean(false));
		}
	}
}