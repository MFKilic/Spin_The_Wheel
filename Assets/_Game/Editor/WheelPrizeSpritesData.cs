using UnityEngine;

[CreateAssetMenu(fileName = "WheelPrizeSpritesData", menuName = "Game/Wheel Prize Sprites Data")]
public class WheelPrizeSpritesData : ScriptableObject
{
    public Sprite[] wheelPrizeSprites;

    [ContextMenu("Find Wheel Prize Sprites")]
    void FindWheelPrizeSprites()
    {
       
        string folderPath = "Assets/_Game/UIImages/WheelPrizeSprites";
        string[] spritePaths = UnityEditor.AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });

        wheelPrizeSprites = new Sprite[spritePaths.Length];

        for (int i = 0; i < spritePaths.Length; i++)
        {
            string spritePath = UnityEditor.AssetDatabase.GUIDToAssetPath(spritePaths[i]);
            wheelPrizeSprites[i] = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
        }
    }

    private void OnValidate()
    {
        FindWheelPrizeSprites();
    }
}
