using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.OSXStandalone;

public class PostBuild_MacOS_ReSignApp : IPostprocessBuildWithReport {
	
	public int callbackOrder => 1000;

	public void OnPostprocessBuild(BuildReport report)
	{
		if(report.summary.platform == BuildTarget.StandaloneOSX)
			MacOSCodeSigning.CodeSignAppBundle(report.summary.outputPath);
	}
}