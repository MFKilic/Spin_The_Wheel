using UnityEngine;

public enum WheelBaseSpritesOption
{
    Bronze,
    Silver,
    Gold
}

[CreateAssetMenu(fileName = "WheelBaseSpritesOptions", menuName = "Game/WheelBaseSpritesOptions")]
public class WheelBaseSpritesOptions : ScriptableObject
{
    public Sprite bronzeSprite;
    public Sprite bronzeIndicator;
    public Sprite silverSprite;
    public Sprite silverIndicator;
    public Sprite goldSprite;
    public Sprite goldIndicator;

    public Sprite GetSprite(WheelBaseSpritesOption option)
    {
        switch (option)
        {
            case WheelBaseSpritesOption.Bronze:
                return bronzeSprite;
            case WheelBaseSpritesOption.Silver:
                return silverSprite;
            case WheelBaseSpritesOption.Gold:
                return goldSprite;
            default:
                return null;
        }
    }
    public Sprite GetSpriteIndicator(WheelBaseSpritesOption option)
    {
        switch (option)
        {
            case WheelBaseSpritesOption.Bronze:
                return bronzeIndicator;
            case WheelBaseSpritesOption.Silver:
                return silverIndicator;
            case WheelBaseSpritesOption.Gold:
                return goldIndicator;
            default:
                return null;
        }
    }
}
