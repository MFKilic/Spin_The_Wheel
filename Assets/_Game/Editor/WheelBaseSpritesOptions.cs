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
    public Sprite silverSprite;
    public Sprite goldSprite;

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
}
