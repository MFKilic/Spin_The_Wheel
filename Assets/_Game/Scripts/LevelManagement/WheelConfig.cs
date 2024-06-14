using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWheelConfig", menuName = "Game/WheelConfig")]
public class WheelConfig : ScriptableObject
{
    private int _wheelLevel;
    [SerializeField] private WheelBaseSpritesOptions _baseSpriteOptions; 
    public WheelBaseSpritesOption selectedBaseSpriteOption_value;
    public bool randomizeWheel;
    [SerializeField] private WheelPrizeSpritesData _prizeSpritesData;

    [Range(8, 8)]
    private int _sliceCount = 8; 

    public WheelSlice[] slices = new WheelSlice[8]; 

    [HideInInspector] public Sprite selectedSprite;
    [HideInInspector] public Sprite indicatorSprite; 

    public int maxSpin_value;
    public int minSpin_value;

    public int spinDuration_value;

    

    

    private void OnValidate()
    {

        if (_baseSpriteOptions == null)
        {
            _baseSpriteOptions = Resources.Load<WheelBaseSpritesOptions>("WheelBaseSpritesOptions");
        }
        if(_prizeSpritesData == null)
        {
            _prizeSpritesData = Resources.Load<WheelPrizeSpritesData>("WheelPrizeSpritesData");
        }
        if (slices.Length != _sliceCount)
        {
            System.Array.Resize(ref slices, _sliceCount);
        }  
        if (_baseSpriteOptions != null)
        {
            selectedSprite = _baseSpriteOptions.GetSprite(selectedBaseSpriteOption_value);
            indicatorSprite = _baseSpriteOptions.GetSpriteIndicator(selectedBaseSpriteOption_value);  
        }
        if(randomizeWheel)
        {
            for(int i = 0; i < slices.Length; i++)
            {
                slices[i].sliceSprite_value = _prizeSpritesData.wheelPrizeSprites[UnityEngine.Random.Range(0,_prizeSpritesData.wheelPrizeSprites.Length)];
                slices[i].rewardAmount_value = UnityEngine.Random.Range(1, 100) *  (1 + (int)selectedBaseSpriteOption_value);
            }
            randomizeWheel = false;
        }


        BombControl();

        SliceNameControl();

        UpdateWheelLevelFromName();
    }

    private void BombControl()
    {
        foreach (var slice in slices)
        {
            if(slice.isBomb_value)
            {
                slice.sliceSprite_value = _prizeSpritesData.bombSprite;
                slice.rewardAmount_value = 0;
            }
            else
            {
                if (slice.sliceSprite_value == _prizeSpritesData.bombSprite)
                {
                    slice.sliceSprite_value = _prizeSpritesData.wheelPrizeSprites[UnityEngine.Random.Range(0, _prizeSpritesData.wheelPrizeSprites.Length)];
                    slice.rewardAmount_value = UnityEngine.Random.Range(1, 100) * (1 + (int)selectedBaseSpriteOption_value); 
                }
            }
        }
    }

    private void SliceNameControl()
    {
        foreach (var slice in slices)
        {
            if (slice.sliceSprite_value != null)
            {
                string spriteName = slice.sliceSprite_value.name;
                int iconIndex = spriteName.IndexOf("Icon", StringComparison.OrdinalIgnoreCase);

                if (iconIndex != -1)
                {

                    int startIndex = spriteName.IndexOf('_', iconIndex + "Icon".Length) + 1;
                    if (startIndex > 0 && startIndex < spriteName.Length)
                    {
                        string afterIcon = spriteName.Substring(startIndex);
                        string[] nameParts = afterIcon.Split('_');

                        System.Text.StringBuilder newNameBuilder = new System.Text.StringBuilder();

                        foreach (string part in nameParts)
                        {
                            if (!string.IsNullOrWhiteSpace(part))
                            {
                                if (newNameBuilder.Length > 0)
                                {
                                    newNameBuilder.Append(" ");
                                }
                                newNameBuilder.Append(char.ToUpper(part[0]) + part.Substring(1));
                            }
                        }

                        slice.sliceName = newNameBuilder.ToString();
                    }
                }
                else
                {
                    string[] nameParts = spriteName.Split('_');
                    slice.sliceName = nameParts[nameParts.Length - 1];
                }
            }
        }
    }

    private void UpdateWheelLevelFromName()
    {
        string objectName = name; 
        int startIndex = objectName.IndexOf('[') + 1; 
        int endIndex = objectName.IndexOf(']'); 

        if (startIndex > 0 && endIndex > startIndex)
        {
            string levelString = objectName.Substring(startIndex, endIndex - startIndex);
            if (int.TryParse(levelString, out int level))
            {
                _wheelLevel = level; 
            }
        }
    }
}

[System.Serializable]
public class WheelSlice
{
    public string sliceName; 
    public Sprite sliceSprite_value; 
    public int rewardAmount_value; 
    public bool isBomb_value; 
}
