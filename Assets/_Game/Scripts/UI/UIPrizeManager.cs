#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TemplateFx;
using DG.Tweening;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class UIPrizeManager : MonoBehaviour
{
    [SerializeField] private List<UIPrizeController> uiPrizeControllers = new List<UIPrizeController>();
    private const string prefabPath = "Assets/_Game/Prefabs/ui_card_panel_prize.prefab";
    [SerializeField] private GameObject uiPrizePrefab;
    [SerializeField] private RectTransform uiPrizeRectTransform;

    private UIPrizeController choosenController = null;
    private Image choosenImage = null;
    private int choosenNumber = 0;
    private float startYPos = 0;
    private string choosenName = string.Empty;
    private Vector2 spriteSize;

    private void OnValidate()
    {
        ValidateRectTransform();
        ValidatePrizeControllers();
#if UNITY_EDITOR
        LoadPrefab();
#endif
    }

    private void ValidateRectTransform()
    {
        if (uiPrizeRectTransform == null)
        {
            uiPrizeRectTransform = GetComponent<RectTransform>();
        }
    }

    private void ValidatePrizeControllers()
    {
        if (uiPrizeControllers == null || uiPrizeControllers.Count == 0)
        {
            uiPrizeControllers.Clear();
            foreach (Transform child in GetComponentsInChildren<Transform>(true))
            {
                UIPrizeController controller = child.GetComponent<UIPrizeController>();
                if (controller != null)
                {
                    uiPrizeControllers.Add(controller);
                }
            }
        }
    }

#if UNITY_EDITOR
    private void LoadPrefab()
    {
        uiPrizePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
    }
#endif

    private void OnEnable()
    {
        GameState.Instance.OnPrepareNewGameEvent += Instance_OnPrepareNewGameEvent;
        LevelManager.Instance.eventManager.OnBombIsExplosedEvent += EventManager_OnBombIsExplosedEvent;
    }

    private void OnDisable()
    {
       
        LevelManager.Instance.eventManager.OnBombIsExplosedEvent -= EventManager_OnBombIsExplosedEvent;
    }
    private void OnDestroy()
    {
        GameState.Instance.OnPrepareNewGameEvent -= Instance_OnPrepareNewGameEvent;
    }
    private void EventManager_OnBombIsExplosedEvent()
    {
        transform.DOPunchPosition(Vector3.one * 6, 0.3f, 0);
    }

    private void Instance_OnPrepareNewGameEvent()
    {
        foreach (UIPrizeController controller in uiPrizeControllers)
        {
            controller.SetImageSprite(null,Vector2.zero);
            controller.SetText(0);
            LevelManager.Instance.datas.CopyPrizeList(uiPrizeControllers);
        }
    }

    public Transform CheckListImage(Image image, int index, string str,Vector2 spriteSizeV2,Vector3 finishCardVector)
    {
        bool isSpriteFound = false;
        ResetChosenValues();
        foreach (UIPrizeController controller in uiPrizeControllers)
        {
            if (controller.GetImage().sprite == image.sprite || controller.GetImage().sprite == null)
            {
                SetChosenValues(controller, image, index, str, spriteSizeV2,finishCardVector);
                isSpriteFound = true;
                break;
            }
        }
        if (!isSpriteFound)
        {
            CreateNewPrizeController(image, index, str, spriteSizeV2,finishCardVector);
        }
        LevelManager.Instance.datas.CopyPrizeList(uiPrizeControllers);
        return choosenController.transform;
    }

    private void ResetChosenValues()
    {
        choosenImage = null;
        choosenNumber = 0;
        choosenName = string.Empty;
    }

    private void SetChosenValues(UIPrizeController controller, Image image, int index, string str, Vector2 spriteSizeV2, Vector3 finishCardVector)
    {
        choosenName = str;
        choosenNumber = index;
        choosenImage = image;
        choosenController = controller;
        spriteSize = spriteSizeV2;
        controller.SetFinishCardVector(finishCardVector);
    }

    private void CreateNewPrizeController(Image image, int index, string str, Vector2 spriteSizeV2, Vector3 finishCardVector)
    {
        GameObject go = Instantiate(uiPrizePrefab, transform);
        UIPrizeController controller = go.GetComponent<UIPrizeController>();
        if (controller != null)
        {
            SetChosenValues(controller, image, index, str, spriteSizeV2, finishCardVector);
            uiPrizeControllers.Add(controller);
        }
    }

    public void SetImageAndText()
    {
        if (choosenImage != null)
        {
            choosenController.SetImageSprite(choosenImage.sprite,spriteSize);
        }
        if (choosenNumber != 0)
        {
            choosenController.SetPrizeName(choosenName);
            choosenController.SetText(choosenNumber);
        }
    }

    void Start()
    {
        startYPos = uiPrizeRectTransform.position.y;
    }

    void Update()
    {
        if (uiPrizeRectTransform.position.y < startYPos)
        {
            uiPrizeRectTransform.position = new Vector2(uiPrizeRectTransform.position.x, startYPos);
        }
    }
}
