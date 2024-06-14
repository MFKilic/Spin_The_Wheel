using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFinisCardController : MonoBehaviour
{
    [SerializeField] private Image cardEndImage;
    [SerializeField] private TextMeshProUGUI cardEndText;
    [SerializeField] private TextMeshProUGUI cardEndNameText;
    private const string cardEndNameTextStr = "ui_card_end_item_name_text";
    private const string cardEndImageStr = "ui_card_end_item_image";
    private const string cardEndTextStr = "ui_card_end_item_text";
    private void OnValidate()
    {
        if(cardEndImage == null || cardEndText == null || cardEndNameText == null)
        {
            Transform[] childTranforms = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in childTranforms)
            {
              

                if (cardEndNameText == null)
                {
                    if (child.name == cardEndNameTextStr)
                    {
                        cardEndNameText = child.GetComponent<TextMeshProUGUI>();
                    }

                }

                if (cardEndImage == null)
                {
                    if (child.name == cardEndImageStr)
                    {
                        cardEndImage = child.GetComponent<Image>();
                    }

                }
                if (cardEndText == null)
                {
                    if (child.name == cardEndTextStr)
                    {
                        cardEndText = child.GetComponent<TextMeshProUGUI>();
                    }

                }

            }
        }
    }

    public void SetImage(Sprite sprite)
    {
       cardEndImage.sprite = sprite;
    }

    public void SetNumberText(string str)
    {
        cardEndText.text = str;
    }

    public void SetNameText(string str)
    {
        cardEndNameText.text = str;
    }

    
}
