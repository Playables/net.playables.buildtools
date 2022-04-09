using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


static class BuildToolsSettingsProvider
{
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        // First parameter is the path in the Settings window.
        // Second parameter is the scope of this setting: it only appears in the Project Settings window.
        var provider = new SettingsProvider("Project/Build Tools Settings", SettingsScope.Project)
        {
            // By default the last token of the path is used as display name if no label is provided.
            label = "Build Tools Settings",
            // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
            guiHandler = (searchContext) =>
            {
                var settings = BuildToolsSettings.GetSerializedSettings();
                EditorGUILayout.LabelField("Mac OS:",EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(settings.FindProperty("macosOverrideExecutableName"), new GUIContent("Override Executable Name"));
                EditorGUILayout.PropertyField(settings.FindProperty("macosExecutableName"), new GUIContent("Executable Name"));
                settings.ApplyModifiedProperties();
            },

            // Populate the search keywords to enable smart search filtering and label highlighting:
            keywords = new HashSet<string>(new[] { "Build","Tools","Settings" })
        };

        return provider;
    }
}