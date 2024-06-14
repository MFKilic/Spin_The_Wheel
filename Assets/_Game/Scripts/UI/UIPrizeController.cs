using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPrizeController : MonoBehaviour
{
    
    [SerializeField] private Image prizeImage;
    [SerializeField] private TextMeshProUGUI prizeText;

    private string prizeName;
    private int textIndex = 0;

    private const string uiCardItemImageStr = "ui_card_panel_prize_image";
    private const string uiCardItemTextStr = "ui_card_panel_prize_image_text";

    private void OnValidate()
    {
        if (prizeImage == null || prizeText == null)
        {
            Transform[] childTranforms = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in childTranforms)
            {

                if (prizeImage == null)
                {
                    if (child.name == uiCardItemImageStr)
                    {
                        prizeImage = child.GetComponent<Image>();
                    }

                }
                if (prizeText == null)
                {
                    if (child.name == uiCardItemTextStr)
                    {
                        prizeText = child.GetComponent<TextMeshProUGUI>();
                    }

                }

            }
        }
    }

    public void SetText(int plusIndex)
    {
        if(plusIndex == 0)
        {
            prizeText.text = string.Empty;
            textIndex = 0;
            return;
        }

        int endValue = textIndex + plusIndex;
        DOTween.To(() => textIndex, x => {
            textIndex = x;
            prizeText.text = x.ToString();
        }, endValue, 0.5f).SetEase(Ease.Linear);
    }

    public void SetImageSprite(Sprite sprite)
    {
        if(sprite != null)
        {
            prizeImage.color = Color.white;
        }
        else
        {
            prizeImage.color = new Color(0, 0, 0, 0);
        }
      
        prizeImage.sprite = sprite;
        prizeImage.transform.localScale = Vector3.zero;

        prizeImage.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutBounce);

    }

    public void SetPrizeName(string str)
    {
        prizeName = str;
    }

    public string GetName()
    {
        return prizeName;
    }

    public Image GetImage()
    {
        return prizeImage;
    }

    public TextMeshProUGUI GetText()
    {
        return prizeText;
    }
   
}
