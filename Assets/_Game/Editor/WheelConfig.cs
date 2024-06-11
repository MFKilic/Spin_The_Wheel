using UnityEngine;

[CreateAssetMenu(fileName = "NewWheelConfig", menuName = "Game/WheelConfig")]
public class WheelConfig : ScriptableObject
{
    public string wheelName; // �ark�n ad�
    public WheelBaseSpritesOptions baseSpriteOptions; // Se�enekleri i�eren ScriptableObject
    public WheelBaseSpritesOption selectedBaseSpriteOption; // Kullan�c�n�n se�imi
    public Sprite indicatorSprite; // �ark�n g�stergesi

    [Range(8, 8)]
    public int sliceCount = 8; // Slice say�s� (8'e sabitlenmi�)

    public WheelSlice[] slices = new WheelSlice[8]; // 8 dilimi tutan dizi

    public Sprite selectedSprite;

    private void OnValidate()
    {
        if (slices.Length != sliceCount)
        {
            System.Array.Resize(ref slices, sliceCount);
        }

        // Se�ilen sprite'� al
        if (baseSpriteOptions != null)
        {
            selectedSprite = baseSpriteOptions.GetSprite(selectedBaseSpriteOption);
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
