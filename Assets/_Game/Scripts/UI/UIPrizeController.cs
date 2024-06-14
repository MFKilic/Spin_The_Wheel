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
    [SerializeField] private RectTransform prizeImageRectTransform;

    private string prizeName;
    private int textIndex = 0;

    private const string uiCardItemImageStr = "ui_card_panel_prize_image";
    private const string uiCardItemTextStr = "ui_card_panel_prize_image_text";

    private Vector3 finishCardVector;

    private void OnValidate()
    {
        if(prizeImageRectTransform == null)
        {
            if(prizeImage != null)
            {
                prizeImageRectTransform = prizeImage.transform.GetComponent<RectTransform>();
            }
        }
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

    public void SetImageSprite(Sprite sprite, Vector2 spriteSize)
    {
        if(sprite != null)
        {
            prizeImage.color = Color.white;
        }
        else
        {
            prizeImage.color = new Color(0, 0, 0, 0);
        }


       

        float currentWidth = prizeImageRectTransform.rect.width;
        float currentHeight = prizeImageRectTransform.rect.height;

        float scaleX = spriteSize.x / currentWidth;
        float scaleY = spriteSize.y / currentHeight;
        prizeImage.sprite = sprite;
        prizeImage.transform.localScale = Vector3.zero;
        Vector3 scale =  new Vector3(scaleX/9f, scaleY / 9f, 1);

        prizeImage.transform.DOScale(scale, 0.5f).SetEase(Ease.InOutBounce);

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

    public void SetFinishCardVector(Vector3 vector3)
    {
        finishCardVector = vector3;
    }

    public Vector3 GetFinishCardVector()
    {
        return finishCardVector;
    }
   
}
