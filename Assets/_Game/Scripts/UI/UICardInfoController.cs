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
    [SerializeField] private Image cardInfoImageBackGround;
    [SerializeField] private TextMeshProUGUI cardInfoText;
    [SerializeField] private TextMeshProUGUI cardInfoNameText;
    [SerializeField] private GameObject cardInfoGiveUpButton;
    [SerializeField] private GameObject cardInfoContinueButton;
    private const string cardInfoNameTextStr = "ui_card_item_name_text";
    private const string cardInfoImageStr = "ui_card_item_image";
    private const string cardInfoTextStr = "ui_card_item_text";
    private const string cardInfoGiveUpButtonStr = "ui_card_button_give_up";
    private const string cardInfoContinueButtonStr = "ui_card_button_continue";
    private Vector3 startPos;
    private Color normalColor;
    private Transform posTr;

    private bool isBomb;
    private void OnValidate()
    {
        if (cardInfoImage == null ||
            cardInfoText == null ||
            cardInfoNameText == null ||
            cardInfoGiveUpButton == null ||
            cardInfoContinueButton == null ||
            cardInfoImageBackGround == null)
        {
            if (cardInfoImageBackGround == null)
            {
                cardInfoImageBackGround = GetComponent<Image>();
            }

            Transform[] childTranforms = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in childTranforms)
            {
                if (cardInfoGiveUpButton == null)
                {
                    if (child.name == cardInfoGiveUpButtonStr)
                    {
                        cardInfoGiveUpButton = child.gameObject;
                    }

                }

                if (cardInfoContinueButton == null)
                {
                    if (child.name == cardInfoContinueButtonStr)
                    {
                        cardInfoContinueButton = child.gameObject;
                    }

                }

                if (cardInfoNameText == null)
                {
                    if (child.name == cardInfoNameTextStr)
                    {
                        cardInfoNameText = child.GetComponent<TextMeshProUGUI>();
                    }

                }

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

    private void OnEnable()
    {
        GameState.Instance.OnPrepareNewGameEvent += Instance_OnPrepareNewGameEvent;
        LevelManager.Instance.eventManager.OnInitSpinEvent += EventManager_OnInitSpinEvent;
        LevelManager.Instance.eventManager.OnContinueButtonPressedEvent += EventManager_OnContinueButtonPressedEvent;
    }

    private void EventManager_OnContinueButtonPressedEvent()
    {
        transform.DOScale(Vector3.zero, 0.3f);
    }

    private void EventManager_OnInitSpinEvent()
    {
        
    }

    private void Instance_OnPrepareNewGameEvent()
    {
        transform.localScale = Vector3.zero;
        cardInfoContinueButton.SetActive(false);
        cardInfoGiveUpButton.SetActive(false);
    }

    private void OnDisable()
    {
        GameState.Instance.OnPrepareNewGameEvent -= Instance_OnPrepareNewGameEvent;
        LevelManager.Instance.eventManager.OnInitSpinEvent -= EventManager_OnInitSpinEvent;
        LevelManager.Instance.eventManager.OnContinueButtonPressedEvent -= EventManager_OnContinueButtonPressedEvent;
    }

    private void Start()
    {
        normalColor = cardInfoImageBackGround.color;
        startPos = transform.position;
        transform.localScale = Vector3.zero;
        cardInfoContinueButton.SetActive(false);
        cardInfoGiveUpButton.SetActive(false);
    }
    private void StartAnim()
    {
        transform.position = startPos;
        if(!isBomb)
        {
            posTr = UIManager.Instance.viewPlay.prizeManager.CheckListImage(cardInfoImage, GetNumber(), GetName());
        }
       
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
            return 0;
        }
    }

    private string GetName()
    {
        return cardInfoNameText.text;
    }
    private void GoToLogPosition()
    {   
        if(isBomb)
        {
            
            LevelManager.Instance.eventManager.OnSpinIsSuccesful(!isBomb);
            return;
        }

        Debug.Log(posTr.position + "PosTrPos");
        StartCoroutine(CardDelayTimer());
    }

    IEnumerator CardDelayTimer()
    {
        yield return new WaitForSeconds(0.75f);
        LevelManager.Instance.eventManager.OnSpinIsSuccesful(!isBomb);
        transform.DOMove(posTr.position, 1).OnComplete(() =>
        {

            UIManager.Instance.viewPlay.prizeManager.SetImageAndText();

        });
        transform.DOScale(Vector3.one * 0.15f, 1).OnComplete(() => transform.localScale = Vector3.zero);
    }


    public void SetInfoCard(Sprite sprite, int number, string name)
    {
        isBomb = false;
        cardInfoNameText.text = name;
        cardInfoImage.sprite = sprite;
        if (number > 0)
        {
            cardInfoText.text = number.ToString();
            cardInfoImageBackGround.color = normalColor;
            cardInfoContinueButton.SetActive(false);
            cardInfoGiveUpButton.SetActive(false);
            
        }
        else
        {
            isBomb = true;
            cardInfoImageBackGround.color = Color.red;
            cardInfoContinueButton.SetActive(true);
            cardInfoGiveUpButton.SetActive(true);
            cardInfoText.text = string.Empty;
        }
        
        StartAnim();

    }
}
