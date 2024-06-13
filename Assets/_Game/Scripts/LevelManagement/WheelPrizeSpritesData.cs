using UnityEngine;

[CreateAssetMenu(fileName = "WheelPrizeSpritesData", menuName = "Game/Wheel Prize Sprites Data")]
public class WheelPrizeSpritesData : ScriptableObject
{
    public Sprite[] wheelPrizeSprites;
    public Sprite bombSprite;

    [ContextMenu("Find Wheel Prize Sprites")]
    void FindWheelPrizeSprites()
    {
#if UNITY_EDITOR
        string folderPath = "Assets/_Game/UIImages/WheelPrizeSprites";
        string bombSpritePath = "Assets/_Game/UIImages/ui_card_icon_death.png";

       
        string[] spritePaths = UnityEditor.AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });
        wheelPrizeSprites = new Sprite[spritePaths.Length];
        for (int i = 0; i < spritePaths.Length; i++)
        {
            string spritePath = UnityEditor.AssetDatabase.GUIDToAssetPath(spritePaths[i]);
            wheelPrizeSprites[i] = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
        }

     
        bombSprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(bombSpritePath);
#endif
    }

    private void OnValidate()
    {
        FindWheelPrizeSprites();
    }
}
