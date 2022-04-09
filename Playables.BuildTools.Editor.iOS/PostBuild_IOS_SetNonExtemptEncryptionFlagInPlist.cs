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
			InfoPlistUtils.WriteInfoPlistKeyForBuildReport(report, InfoPlistKeys_IOS.ITSAppUsesNonExemptEncryption, new PlistElementBoolean(false));
	}
}