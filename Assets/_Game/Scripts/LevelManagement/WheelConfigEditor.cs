using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(WheelConfig))]
public class WheelConfigEditor : Editor
{
    private const float spriteDisplaySize = 100f; 

    public override void OnInspectorGUI()
    {
       
        DrawDefaultInspector();

     
        WheelConfig wheelConfig = (WheelConfig)target;

       
        if (wheelConfig.selectedSprite != null)
        {
            GUILayout.Label("Wheel Base Sprite Preview:");
            GUILayout.Label(new GUIContent(wheelConfig.selectedSprite.texture), GUILayout.Width(spriteDisplaySize), GUILayout.Height(spriteDisplaySize));
        }

        
        if (wheelConfig.indicatorSprite != null)
        {
            GUILayout.Label("Indicator Sprite Preview:");
            GUILayout.Label(new GUIContent(wheelConfig.indicatorSprite.texture), GUILayout.Width(spriteDisplaySize), GUILayout.Height(spriteDisplaySize));
        }

       
        if (wheelConfig.slices != null)
        {
            for (int i = 0; i < wheelConfig.slices.Length; i++)
            {
                if (wheelConfig.slices[i].sliceSprite_value != null)
                {
                    GUILayout.Label($"Slice {i + 1} Sprite Preview:");
                    GUILayout.Label(new GUIContent(wheelConfig.slices[i].sliceSprite_value.texture), GUILayout.Width(spriteDisplaySize), GUILayout.Height(spriteDisplaySize));
                }
            }
        }
    }
}
#endif