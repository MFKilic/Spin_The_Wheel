using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Config
{
    [CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Game/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public List<WheelConfig> wheelConfigs;

        private void OnValidate()
        {
#if UNITY_EDITOR
            LoadWheelConfigs();
#endif
        }

#if UNITY_EDITOR
        private void LoadWheelConfigs()
        {
            wheelConfigs = new List<WheelConfig>();

            // Klasör yolunu belirtin
            string folderPath = "Assets/_Game/ScriptableObjects/Levels";

            // Bu klasörden tüm WheelConfig varlýklarýný yükleyin
            string[] guids = AssetDatabase.FindAssets("t:WheelConfig", new[] { folderPath });

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                WheelConfig wheelConfig = AssetDatabase.LoadAssetAtPath<WheelConfig>(assetPath);
                if (wheelConfig != null)
                {
                    wheelConfigs.Add(wheelConfig);
                }
            }
        }
#endif
    }
}

