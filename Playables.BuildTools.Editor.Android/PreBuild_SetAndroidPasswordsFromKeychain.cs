using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class PreBuild_SetAndroidPasswordsFromKeychain : IPreprocessBuildWithReport{
	
	public int callbackOrder { get; }
	public void OnPreprocessBuild(BuildReport report)
	{
		if (report.summary.platform == BuildTarget.Android)
		{
			PlayerSettings.keyaliasPass = MacosKeychain.GetKeychainPassword("unity_android_keyalias_password","unity_android_keyalias");
			PlayerSettings.keystorePass = MacosKeychain.GetKeychainPassword("unity_android_keystore_password","unity_android_keystore");
		}
	}
}