using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WheelBaseSpritesOption))]
public class WheelBaseSpriteOptionDrawer : PropertyDrawer
{
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Enum popup göster
        property.enumValueIndex = EditorGUI.Popup(position, label.text, property.enumValueIndex, property.enumDisplayNames);

        EditorGUI.EndProperty();
    }
}
