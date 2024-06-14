using System.Collections;
using System.Collections.Generic;
using TemplateFx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIButtonController : MonoBehaviour, IClickable
{
    public enum ButtonTypes
    {
        Spin, Exit, GiveUp, Continue, Quit, TryAgain
    }

    [SerializeField] private ButtonTypes buttonType;

    [SerializeField] private Image buttonImage;

    [SerializeField] private Color startColor;
    public void OnClick()
    {
        switch (buttonType)
        {
            case ButtonTypes.Spin:
                UIManager.Instance.viewPlay.spinManager.SpinWheel();
                break;
            case ButtonTypes.Exit:
                GameState.Instance.OnFinishGame(LevelFinishStatus.WIN);
                break;
            case ButtonTypes.Continue:
                LevelManager.Instance.eventManager.OnContinueButtonPressed();
                LevelManager.Instance.eventManager.OnSpinIsSuccesful(true);
                break;
            case ButtonTypes.GiveUp:
                LevelManager.Instance.eventManager.OnGiveUpButtonPressed();
                GameState.Instance.OnFinishGame(LevelFinishStatus.LOSE);
                break;
            case ButtonTypes.TryAgain:
                GameState.Instance.OnPrepareNewGame();
                LevelManager.Instance.eventManager.OnInitSpin();
                Debug.Log("TryAgain");
                break;
            case ButtonTypes.Quit:
                Application.Quit();
                Debug.Log("Quit");
                break;


        }

        transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 0, 0.2f);
        buttonImage.color = Color.gray;
        buttonImage.raycastTarget = false;
        Debug.Log("CLICKED");
    }

    private void OnEnable()
    {
        buttonImage.color = startColor;
        buttonImage.raycastTarget = true;
        LevelManager.Instance.eventManager.OnPreSpinEvent += EventManager_OnPreSpinEvent;
        LevelManager.Instance.eventManager.OnContinueButtonPressedEvent += EventManager_OnContinueButtonPressedEvent;
        LevelManager.Instance.eventManager.OnGiveUpButtonPressedEvent += EventManager_OnGiveUpButtonPressedEvent;
    }

    private void EventManager_OnGiveUpButtonPressedEvent()
    {
        buttonImage.color = Color.gray;
        buttonImage.raycastTarget = false;
    }

    private void EventManager_OnContinueButtonPressedEvent()
    {
        buttonImage.color = Color.gray;
        buttonImage.raycastTarget = false;
    }

    private void EventManager_OnPreSpinEvent()
    {
        switch (buttonType)
        {
            case ButtonTypes.Spin:

              
                break;
            case ButtonTypes.Exit:

                break;
            case ButtonTypes.Continue:

                break;
            case ButtonTypes.GiveUp:

                break;

        }
        buttonImage.color = startColor;
        buttonImage.raycastTarget = true;
    }

    private void OnDisable()
    {
        LevelManager.Instance.eventManager.OnPreSpinEvent -= EventManager_OnPreSpinEvent;
        LevelManager.Instance.eventManager.OnContinueButtonPressedEvent -= EventManager_OnContinueButtonPressedEvent;
        LevelManager.Instance.eventManager.OnGiveUpButtonPressedEvent -= EventManager_OnGiveUpButtonPressedEvent;
    }

    private void OnValidate()
    {
        if (buttonImage == null)
        {
            buttonImage = GetComponent<Image>();
        }

        startColor = buttonImage.color;
    }


}
