using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

// Creates a text file in Resources which contains the last commit hash (for display in build)
public class PreBuild_SetCommit : IPostprocessBuildWithReport, IPreprocessBuildWithReport
{
	public int callbackOrder { get; }
	
	const string path = "Assets/Resources/commit.txt";
	
	public void OnPreprocessBuild(BuildReport report)
	{
		var commit = ShellHelper.Bash("git rev-parse HEAD").TrimEnd( '\r', '\n' );

		new FileInfo(path).Directory?.Create();
		
		File.WriteAllText(path,commit);
		AssetDatabase.ImportAsset(path,ImportAssetOptions.ForceSynchronousImport);
	}

	public void OnPostprocessBuild(BuildReport report)
	{
		if(AssetDatabase.LoadAssetAtPath<TextAsset>(path))
			AssetDatabase.DeleteAsset(path);
	}
}