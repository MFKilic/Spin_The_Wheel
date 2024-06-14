using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class RunTimeAtlasController : MonoBehaviour
{
    private const string atlasPath = "Assets/_Game/UIImages/MenuAtlas.spriteatlasv2";
    [SerializeField] private SpriteAtlas atlas;

    private void OnValidate()
    {
#if UNITY_EDITOR
        atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(atlasPath);
#endif
    }

    public void SetImageToAtlas(Image image, string str)
    {        
      image.sprite = atlas.GetSprite(str);
    }
}
