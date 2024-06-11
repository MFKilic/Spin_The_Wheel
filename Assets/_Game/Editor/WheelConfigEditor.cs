using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WheelConfig))]
public class WheelConfigEditor : Editor
{
    private const float spriteDisplaySize = 100f; // Sprite gösterim boyutu

    public override void OnInspectorGUI()
    {
        // Base Inspector'ý çiz
        DrawDefaultInspector();

        // WheelConfig instance'ýný al
        WheelConfig wheelConfig = (WheelConfig)target;

        // wheelBaseSprite'i daha büyük göster
        if (wheelConfig.selectedSprite != null)
        {
            GUILayout.Label("Wheel Base Sprite Preview:");
            GUILayout.Label(new GUIContent(wheelConfig.selectedSprite.texture), GUILayout.Width(spriteDisplaySize), GUILayout.Height(spriteDisplaySize));
        }

        // indicatorSprite'i daha büyük göster
        if (wheelConfig.indicatorSprite != null)
        {
            GUILayout.Label("Indicator Sprite Preview:");
            GUILayout.Label(new GUIContent(wheelConfig.indicatorSprite.texture), GUILayout.Width(spriteDisplaySize), GUILayout.Height(spriteDisplaySize));
        }

        // slices için sprite'larý daha büyük göster
        if (wheelConfig.slices != null)
        {
            for (int i = 0; i < wheelConfig.slices.Length; i++)
            {
                if (wheelConfig.slices[i].sliceSprite != null)
                {
                    GUILayout.Label($"Slice {i + 1} Sprite Preview:");
                    GUILayout.Label(new GUIContent(wheelConfig.slices[i].sliceSprite.texture), GUILayout.Width(spriteDisplaySize), GUILayout.Height(spriteDisplaySize));
                }
            }
        }
    }
}
