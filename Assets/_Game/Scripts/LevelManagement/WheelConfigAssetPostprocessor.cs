using UnityEditor;
using UnityEngine;
using System.IO;

public class WheelConfigAssetPostprocessor : AssetPostprocessor
{
    private const string targetFolderPath = "Assets/_Game/ScriptableObjects/Levels";

    static void OnPostprocessAllAssets(
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        foreach (string importedAsset in importedAssets)
        {
            if (importedAsset.StartsWith(targetFolderPath) && importedAsset.EndsWith(".asset"))
            {
                WheelConfig wheelConfig = AssetDatabase.LoadAssetAtPath<WheelConfig>(importedAsset);
                if (wheelConfig != null)
                {
                    UpdateAssetName(importedAsset);
                }
            }
        }
    }

    private static void UpdateAssetName(string assetPath)
    {       
        string[] guids = AssetDatabase.FindAssets("t:WheelConfig", new[] { targetFolderPath });

        int index = System.Array.IndexOf(guids, AssetDatabase.AssetPathToGUID(assetPath)) + 1;

        if (index > 0)
        {
            string newAssetName = $"[{index}]WheelLevel";
            string newPath = Path.Combine(Path.GetDirectoryName(assetPath), newAssetName + ".asset");

            newPath = AssetDatabase.GenerateUniqueAssetPath(newPath);

            AssetDatabase.RenameAsset(assetPath, newAssetName);
            AssetDatabase.SaveAssets();
        }
    }
}
