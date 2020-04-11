using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;

// set versioning system to "apple-generic" so fastlane can change the build version for testflight later
public class PostBuild_SetVersioningSystem : IPostprocessBuildWithReport {

	public int callbackOrder { get { return 0; } }

	public void OnPostprocessBuild(BuildReport report)
	{
		if (report.summary.platform == BuildTarget.iOS)
		{
			// PBX project
			PBXProject project = new PBXProject();
			string sPath = PBXProject.GetPBXProjectPath(report.summary.outputPath);
			project.ReadFromFile(sPath);

			#if UNITY_2019_3_OR_NEWER
			string g = project.GetUnityMainTargetGuid();
			#else
			string tn = PBXProject.GetUnityTargetName();
			string g = project.TargetGuidByName(tn);
			#endif

            
			project.AddBuildProperty(g,
				"VERSIONING_SYSTEM",
				"apple-generic");
			project.AddBuildProperty(g,
				"CURRENT_PROJECT_VERSION",
				 "0");

			// modify frameworks and settings as desired
			File.WriteAllText(sPath, project.WriteToString());
		}
	}
}