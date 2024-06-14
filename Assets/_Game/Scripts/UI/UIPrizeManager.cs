#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TemplateFx;
using DG.Tweening;

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

    private void OnValidate()
    {
        if(uiPrizeRectTransform == null)
        {
            uiPrizeRectTransform = GetComponent<RectTransform>();
        }
        if (uiPrizeControllers == null || uiPrizeControllers.Count == 0)
        {
            uiPrizeControllers.Clear();

            Transform[] childTransforms = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in childTransforms)
            {
                UIPrizeController controller = child.GetComponent<UIPrizeController>();
                if (controller != null)
                {
                    uiPrizeControllers.Add(controller);
                }
            }
        }

#if UNITY_EDITOR
        uiPrizePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
#endif
    }

    private void OnEnable()
    {
        GameState.Instance.OnPrepareNewGameEvent += Instance_OnPrepareNewGameEvent;
        LevelManager.Instance.eventManager.OnBombIsExplosedEvent += EventManager_OnBombIsExplosedEvent;
    }

    private void EventManager_OnBombIsExplosedEvent()
    {
        transform.DOPunchPosition(Vector3.one * 6, 0.3f, 0);
    }

    private void Instance_OnPrepareNewGameEvent()
    {
        foreach (UIPrizeController controller in uiPrizeControllers)
        {
            controller.SetImageSprite(null);
            controller.SetText(0);
            LevelManager.Instance.datas.CopyPrizeList(uiPrizeControllers);
        }
    }

    private void OnDisable()
    {
        GameState.Instance.OnPrepareNewGameEvent -= Instance_OnPrepareNewGameEvent;
        LevelManager.Instance.eventManager.OnBombIsExplosedEvent -= EventManager_OnBombIsExplosedEvent;
    }



    public Transform CheckListImage(Image image, int index, string str)
    {
        bool isImageAddedList = false;
        choosenImage = null;
        choosenNumber = 0;
        choosenName = string.Empty;
        
        foreach (UIPrizeController controller in uiPrizeControllers)
        {
            if (controller.GetImage().sprite == image.sprite)
            {
                choosenName = str;
                choosenNumber = index;     
                choosenController = controller;
                isImageAddedList = true;
                break;
            }
            else if (controller.GetImage().sprite == null)
            {
                choosenName = str;
                choosenImage = image;
                choosenNumber = index;
                choosenController = controller;
                isImageAddedList = true;
                break;
            }
         
        }

        if (!isImageAddedList)
        {
            GameObject go = Instantiate(uiPrizePrefab, transform);
            UIPrizeController controller = go.GetComponent<UIPrizeController>();

            if (controller != null)
            {
                choosenImage = image;
                choosenNumber = index;
                choosenName = str;
                choosenController = controller;
                uiPrizeControllers.Add(controller);
            }
        }

        Debug.Log(choosenController.transform.position + "ChoosenPos");

        LevelManager.Instance.datas.CopyPrizeList(uiPrizeControllers);

        return choosenController.transform;
    }

    public void SetImageAndText()
    {
        if(choosenImage != null)
        {
            choosenController.SetImageSprite(choosenImage.sprite);
        }

        if(choosenNumber != 0)
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
        //else if (uiPrizeRectTransform.position.y > (uiPrizeControllers.Count * 35) - startYPos)
        //{
        //    uiPrizeRectTransform.position = new Vector2(uiPrizeRectTransform.position.x, (uiPrizeControllers.Count * 35) - startYPos);
        //}
       // Debug.Log("Pos = "+uiPrizeControllers.Count * 35 + "Pos Y = "+ uiPrizeRectTransform.position.y);
    }
}
