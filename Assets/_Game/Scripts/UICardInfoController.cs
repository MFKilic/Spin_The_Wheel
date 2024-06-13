using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TemplateFx;

public class UICardInfoController : MonoBehaviour
{
    [SerializeField] private Image cardInfoImage;
    [SerializeField] private TextMeshProUGUI cardInfoText;
    private const string cardInfoImageStr = "ui_card_item_image";
    private const string cardInfoTextStr = "ui_card_item_text";
    private Vector3 startPos;
    private Transform posTr;
    private void OnValidate()
    {
        if (cardInfoImage == null || cardInfoText == null)
        {
            Transform[] childTranforms = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in childTranforms)
            {

                if (cardInfoImage == null)
                {
                    if (child.name == cardInfoImageStr)
                    {
                        cardInfoImage = child.GetComponent<Image>();
                    }

                }
                if (cardInfoText == null)
                {
                    if (child.name == cardInfoTextStr)
                    {
                        cardInfoText = child.GetComponent<TextMeshProUGUI>();
                    }

                }

            }
        }
    }

    private void Start()
    {
        startPos = transform.position;
    }
    private void StartAnim()
    {
        transform.position = startPos;
        posTr = UIManager.Instance.viewPlay.prizeManager.CheckListImage(cardInfoImage, GetNumber());
        transform.localScale = new Vector3(0, 1, 1);
        transform.DOScaleX(1, 0.5f).OnComplete(GoToLogPosition);

    }
    public int GetNumber()
    {
        int number;
        if (int.TryParse(cardInfoText.text, out number))
        {
            return number;
        }
        else
        {
            Debug.LogError("Text is not a valid number");
            return 0; // veya uygun bir varsayýlan deðer döndürebilirsiniz
        }
    }
    private void GoToLogPosition()
    {
      
        Debug.Log(posTr.position + "PosTrPos");
        transform.DOMove(posTr.position, 1).OnComplete(() =>
        {

            UIManager.Instance.viewPlay.prizeManager.SetImageAndText();

        });
        transform.DOScale(Vector3.one * 0.15f, 1).OnComplete(()=> transform.localScale = Vector3.zero);
    }


    public void SetInfoCard(Sprite sprite, int number)
    {
        cardInfoImage.sprite = sprite;
        cardInfoText.text = number.ToString();
        StartAnim();
    }
}
