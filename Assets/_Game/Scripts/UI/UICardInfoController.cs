using System.Collections;
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
        if (cardInfoImage == null || cardInfoText == null || cardInfoNameText == null ||
            cardInfoGiveUpButton == null || cardInfoContinueButton == null || cardInfoImageBackGround == null)
        {
            AssignMissingComponents();
        }
    }

    private void AssignMissingComponents()
    {
        if (cardInfoImageBackGround == null)
        {
            cardInfoImageBackGround = GetComponent<Image>();
        }

        Transform[] childTransforms = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in childTransforms)
        {
            if (cardInfoGiveUpButton == null && child.name == cardInfoGiveUpButtonStr)
            {
                cardInfoGiveUpButton = child.gameObject;
            }
            if (cardInfoContinueButton == null && child.name == cardInfoContinueButtonStr)
            {
                cardInfoContinueButton = child.gameObject;
            }
            if (cardInfoNameText == null && child.name == cardInfoNameTextStr)
            {
                cardInfoNameText = child.GetComponent<TextMeshProUGUI>();
            }
            if (cardInfoImage == null && child.name == cardInfoImageStr)
            {
                cardInfoImage = child.GetComponent<Image>();
            }
            if (cardInfoText == null && child.name == cardInfoTextStr)
            {
                cardInfoText = child.GetComponent<TextMeshProUGUI>();
            }
        }
    }

    private void OnEnable()
    {
        GameState.Instance.OnPrepareNewGameEvent += Instance_OnPrepareNewGameEvent;
        LevelManager.Instance.eventManager.OnContinueButtonPressedEvent += EventManager_OnContinueButtonPressedEvent;
    }

    private void OnDisable()
    {
        LevelManager.Instance.eventManager.OnContinueButtonPressedEvent -= EventManager_OnContinueButtonPressedEvent;
    }

    private void OnDestroy()
    {
        GameState.Instance.OnPrepareNewGameEvent -= Instance_OnPrepareNewGameEvent;
    }


    private void Start()
    {
        normalColor = cardInfoImageBackGround.color;
        startPos = transform.position;
        transform.localScale = Vector3.zero;
        cardInfoContinueButton.SetActive(false);
        cardInfoGiveUpButton.SetActive(false);
    }

    private void EventManager_OnContinueButtonPressedEvent()
    {
        transform.DOScale(Vector3.zero, 0.3f);
    }

    private void Instance_OnPrepareNewGameEvent()
    {
        transform.localScale = Vector3.zero;
        cardInfoContinueButton.SetActive(false);
        cardInfoGiveUpButton.SetActive(false);
    }

    private void StartAnim()
    {
        SoundManager.Instance.SoundPlay("CardFlip");
        transform.position = startPos;
        if (!isBomb)
        {
            posTr = UIManager.Instance.viewPlay.prizeManager.CheckListImage(cardInfoImage, GetNumber(), GetName());
        }
        transform.localScale = new Vector3(0, 1, 1);
        transform.DOScaleX(1, 0.5f).OnComplete(GoToLogPosition);
    }

    public int GetNumber()
    {
        if (int.TryParse(cardInfoText.text, out int number))
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
        if (isBomb)
        {
            SoundManager.Instance.SoundPlay("Bomb");
            LevelManager.Instance.eventManager.OnSpinIsSuccesful(!isBomb);
            return;
        }
        StartCoroutine(CardDelayTimer());
    }

    private IEnumerator CardDelayTimer()
    {
        yield return new WaitForSeconds(0.75f);
        LevelManager.Instance.eventManager.OnSpinIsSuccesful(!isBomb);
        transform.DOMove(posTr.position, 1).OnComplete(() =>
        {
            SoundManager.Instance.SoundPlay("CardFlip");
            UIManager.Instance.viewPlay.prizeManager.SetImageAndText();
        });
        transform.DOScale(Vector3.one * 0.15f, 1).OnComplete(() => transform.localScale = Vector3.zero);
    }

    public void SetInfoCard(Sprite sprite, int number, string name)
    {
        isBomb = number <= 0;
        cardInfoNameText.text = name;
        cardInfoImage.sprite = sprite;
        cardInfoText.text = isBomb ? string.Empty : number.ToString();
        cardInfoImageBackGround.color = isBomb ? Color.red : normalColor;
        cardInfoContinueButton.SetActive(isBomb);
        cardInfoGiveUpButton.SetActive(isBomb);
        StartAnim();
    }
}
