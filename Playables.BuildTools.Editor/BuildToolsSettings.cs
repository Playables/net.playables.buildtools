using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildToolsSettings : ScriptableObject
{
	public const string k_MyCustomSettingsPath = "Assets/Editor/BuildToolsSettings.asset";

	public bool macosOverrideExecutableName = false;
	public string macosExecutableName = "";

	public static BuildToolsSettings GetOrCreateSettings()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(k_MyCustomSettingsPath));

		var settings = AssetDatabase.LoadAssetAtPath<BuildToolsSettings>(k_MyCustomSettingsPath);
		if (settings == null)
		{
			settings = ScriptableObject.CreateInstance<BuildToolsSettings>();
			AssetDatabase.CreateAsset(settings, k_MyCustomSettingsPath);
			AssetDatabase.SaveAssets();
		}

		return settings;
	}

	internal static SerializedObject GetSerializedSettings()
	{
		return new SerializedObject(GetOrCreateSettings());
	}
}