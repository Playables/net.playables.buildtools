using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

#if !UNITY_CLOUD_BUILD
public class PreBuild_SetAndroidPasswordsFromKeychain : IPreprocessBuildWithReport{
	
	public int callbackOrder { get; }
	public void OnPreprocessBuild(BuildReport report)
	{
		if (report.summary.platform == BuildTarget.Android)
		{
			var keystoreName = Path.GetFileNameWithoutExtension(PlayerSettings.Android.keystoreName);

			PlayerSettings.keyaliasPass = MacosKeychain.GetKeychainPassword(keystoreName,"unity_android_keyalias");
			PlayerSettings.keystorePass = MacosKeychain.GetKeychainPassword(keystoreName,"unity_android_keystore");
		}
	}
}
#endif