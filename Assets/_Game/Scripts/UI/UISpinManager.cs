using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TemplateFx;

public class UISpinManager : MonoBehaviour
{
    private const string uiSpinBaseName = "ui_spin_base";
    private const string uiSpinIndicatorName = "ui_spin_indicator";
    private const string uiSpinIconName = "ui_spin_icon";

    [SerializeField] private Image uiSpinBase;
    [SerializeField] private Image uiSpinIndicator;
    [SerializeField] private UISpinController[] uiSpinControllers;

    private float spinDuration = 3f;
    private int minSpins = 3;
    private int maxSpins = 5;
    private float targetRotation;
    private Tween indicatorTween;
    private LevelManager levelManager;
    private Datas datas;
    private int chosenListIndex = 0;

    private void OnValidate()
    {
        AssignSpinComponents();
        InitializeSpinControllers();
    }

    private void AssignSpinComponents()
    {
        if (uiSpinBase == null || uiSpinIndicator == null)
        {
            Transform[] spinTransforms = GetComponentsInChildren<Transform>(true);
            foreach (Transform spin in spinTransforms)
            {
                if (uiSpinBase == null && spin.name == uiSpinBaseName)
                {
                    uiSpinBase = spin.GetComponent<Image>();
                }
                if (uiSpinIndicator == null && spin.name == uiSpinIndicatorName)
                {
                    uiSpinIndicator = spin.GetComponent<Image>();
                }
            }
        }
    }

    private void InitializeSpinControllers()
    {
        if (uiSpinControllers == null || uiSpinControllers.Length == 0)
        {
            List<UISpinController> controllersList = new List<UISpinController>();
            Transform[] childTransforms = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in childTransforms)
            {
                if (child.name.Contains(uiSpinIconName))
                {
                    UISpinController controller = child.GetComponent<UISpinController>();
                    if (controller != null)
                    {
                        controllersList.Add(controller);
                    }
                }
            }
            uiSpinControllers = controllersList.ToArray();
        }
    }

    private void OnEnable()
    {
        LevelManager.Instance.eventManager.OnInitSpinEvent += OnInitSpinEvent;
        LevelManager.Instance.eventManager.OnBombIsExplosedEvent += OnBombIsExplosedEvent;
    }

    private void OnDisable()
    {
        LevelManager.Instance.eventManager.OnInitSpinEvent -= OnInitSpinEvent;
        LevelManager.Instance.eventManager.OnBombIsExplosedEvent -= OnBombIsExplosedEvent;
    }

    private void Awake()
    {
        levelManager = LevelManager.Instance;
        datas = levelManager.datas;
    }

    private void OnBombIsExplosedEvent()
    {
        transform.DOPunchPosition(Vector3.one * 15, 0.2f, 0);
    }

    private void OnInitSpinEvent()
    {
        var config = datas.config;
        var wheelConfig = config.wheelConfigs[datas.level - 1];

        spinDuration = wheelConfig.spinDuration_value;
        minSpins = wheelConfig.minSpin_value;
        maxSpins = wheelConfig.maxSpin_value;

        uiSpinBase.sprite = wheelConfig.selectedSprite;
        uiSpinIndicator.sprite = wheelConfig.indicatorSprite;

        transform.eulerAngles = new Vector3(0, 270, 0);
        transform.DORotate(Vector3.zero, 0.5f).OnComplete(() => LevelManager.Instance.eventManager.OnPreSpin());

        for (int i = 0; i < uiSpinControllers.Length; i++)
        {
            var controller = uiSpinControllers[i];
            var slice = wheelConfig.slices[i];

            controller.SetNameString(slice.sliceName);
            controller.SetImageSprite(slice.sliceSprite_value);
            controller.SetText(slice.rewardAmount_value.ToString().CurencyToLadder());
        }
    }

    public void SpinWheel()
    {
        if (uiSpinBase == null) return;

        chosenListIndex = Random.Range(0, 8);
        targetRotation = chosenListIndex * 45;
        float randomizeFinishAngle = targetRotation + (360f * Random.Range(minSpins, maxSpins + 1)) + Random.Range(-22.5f, 22.5f);

        uiSpinBase.transform.DORotate(new Vector3(0, 0, randomizeFinishAngle), spinDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutQuart)
            .OnUpdate(OnSpinUpdate)
            .OnComplete(OnSpinComplete);

        LevelManager.Instance.eventManager.OnDuringSpin();
    }

    private void OnSpinUpdate()
    {
        float currentRotation = uiSpinBase.transform.eulerAngles.z;
        if (Mathf.Abs(currentRotation % 45) < 1)
        {
            PlaySegmentAnimation();
        }
    }

    private void OnSpinComplete()
    {
        Debug.Log("Spin complete!");
        LevelManager.Instance.eventManager.OnFinishSpin();
        uiSpinBase.transform.DORotate(new Vector3(0, 0, targetRotation), spinDuration / 4)
            .SetEase(Ease.InOutSine)
            .OnComplete(OnSpinAfter);
    }

    private void OnSpinAfter()
    {
        var chosenUISpinController = uiSpinControllers[chosenListIndex];

        chosenUISpinController.transform.DOPunchScale(Vector3.one, 0.3f, 1).OnComplete(() =>
        {
            transform.DORotate(new Vector3(0, -90, 0), 0.5f);
        });

        UIManager.Instance.viewPlay.SetInfoCard(chosenUISpinController.GetImage().sprite,
                                                chosenUISpinController.GetNumber(),
                                                chosenUISpinController.GetNameString());

        if (chosenUISpinController.GetNumber() == 0)
        {
            LevelManager.Instance.eventManager.OnBombIsExplosed();
        }

        LevelManager.Instance.eventManager.OnAfterSpin();
    }

    private void PlaySegmentAnimation()
    {
        if (indicatorTween != null)
        {
            indicatorTween.Kill();
            SoundManager.Instance.SoundPlay("Wheel");
            uiSpinIndicator.transform.eulerAngles = Vector3.zero;
        }
        
        float indicatorAnimAngle = -22.5f;
        indicatorTween = uiSpinIndicator.transform.DORotate(new Vector3(0, 0, indicatorAnimAngle), 0.1f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => uiSpinIndicator.transform.DORotate(Vector3.zero, 0.1f));
    }
}
