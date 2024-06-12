using System;
using TemplateFx;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWheelConfig", menuName = "Game/WheelConfig")]
public class WheelConfig : ScriptableObject
{
    public event Action<int> OnConfigChanged;
    public int wheelLevel; // �ark�n ad�
    public WheelBaseSpritesOptions baseSpriteOptions; // Se�enekleri i�eren ScriptableObject
    public WheelBaseSpritesOption selectedBaseSpriteOption; // Kullan�c�n�n se�imi

    [Range(8, 8)]
    private int _sliceCount = 8; // Slice say�s� (8'e sabitlenmi�)

     public WheelSlice[] slices = new WheelSlice[8]; // 8 dilimi tutan dizi

    [HideInInspector] public Sprite selectedSprite;
    [HideInInspector] public Sprite indicatorSprite; // �ark�n g�stergesi

    public int maxSpin_value;
    public int minSpin_value;

    public int spinDuration_value;

    public UISpinManager spinManager;

    private void OnValidate()
    {
        if (spinManager == null)
        {
            spinManager = FindObjectOfType<UISpinManager>();
        }
        if (slices.Length != _sliceCount)
        {
            System.Array.Resize(ref slices, _sliceCount);
        }

        // Se�ilen sprite'� al
        if (baseSpriteOptions != null)
        {
            selectedSprite = baseSpriteOptions.GetSprite(selectedBaseSpriteOption);
            indicatorSprite = baseSpriteOptions.GetSpriteIndicator(selectedBaseSpriteOption);
            // Se�ilen sprite ile yap�lacak i�lemler burada olabilir
        }
       

        foreach (var slice in slices)
        {
            if (slice.sliceSprite != null)
            {
                string spriteName = slice.sliceSprite.name;
                int iconIndex = spriteName.IndexOf("Icon");

                if (iconIndex != -1)
                {
                    // "Icon" kelimesinden sonraki "_" karakterinden sonras�n� al
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
}

[System.Serializable]
public class WheelSlice
{
    public string sliceName; // Dilimin ad�
    public Sprite sliceSprite; // Dilimin sprite'�
    public int rewardAmount; // �d�l miktar� (Varsa)
    public bool isBomb; // Bu dilimin bomba olup olmad���n� belirten bayrak
}
