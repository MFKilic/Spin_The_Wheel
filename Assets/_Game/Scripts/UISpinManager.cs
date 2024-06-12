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
    public Image uiSpinBase;
    public Image uiSpinIndicator;
    public UISpinController[] uiSpinControllers;
    public float spinDuration = 3f; // Spin süresi
    public int minSpins = 3; // Minimum tur sayýsý
    public int maxSpins = 5; // Maksimum tur sayýsý
    private float _targetRotation;
    private Tween _indicatorTween;

    private void OnValidate()
    {
        if (uiSpinBase == null || uiSpinIndicator == null)
        {
            Transform[] spinTransforms = GetComponentsInChildren<Transform>(true);
            foreach (Transform spin in spinTransforms)
            {
                if (uiSpinBase == null)
                {
                    if (spin.name == uiSpinBaseName)
                    {
                        uiSpinBase = spin.GetComponent<Image>();
                    }
                }

                if (uiSpinIndicator == null)
                {
                    if (spin.name == uiSpinIndicatorName)
                    {
                        uiSpinIndicator = spin.GetComponent<Image>();
                    }

                }
            }
        }
        if (uiSpinControllers == null)
        {
            List<UISpinController> controllersList = new List<UISpinController>();
            Transform[] childTransforms = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in childTransforms)
            {
                // Child'ýn adý "ui_spin_icon" ise, UISpinController bileþenini alýp listeye ekle
                if (child.name.Contains(uiSpinIconName))
                {
                    UISpinController controller = child.GetComponent<UISpinController>();
                    if (controller != null)
                    {
                        controllersList.Add(controller);
                    }
                }
            }
            // Listeyi diziye dönüþtür
            uiSpinControllers = controllersList.ToArray();
        }

    }

    private void OnEnable()
    {
        LevelManager.Instance.eventManager.OnInitSpinEvent += EventManager_OnInitSpinEvent;
    }

    private void EventManager_OnInitSpinEvent()
    {
        Debug.Log("Init");
        var config = LevelManager.Instance.datas.config;
        var wheelConfig = config.wheelConfigs[0];
        spinDuration = wheelConfig.spinDuration_value;
        minSpins = wheelConfig.minSpin_value;
        maxSpins = wheelConfig.maxSpin_value;
        uiSpinBase.sprite = wheelConfig.selectedSprite;
        uiSpinIndicator.sprite = wheelConfig.indicatorSprite;
        LevelManager.Instance.eventManager.OnPreSpin();

    }

    private void OnDisable()
    {
        LevelManager.Instance.eventManager.OnInitSpinEvent -= EventManager_OnInitSpinEvent;
    }

    void Start()
    {

    }



    // Çarký döndürme fonksiyonu
    public void SpinWheel()
    {
        if (uiSpinBase != null)
        {
            // Rastgele bir dilim seç
            int randomSlice = Random.Range(0, 8);
            // Bu dilimin ortasýnda duracak þekilde dereceyi hesapla
            _targetRotation = (randomSlice * 45);
            float randomizeFinishAngle = _targetRotation + (360f * Random.Range(minSpins, maxSpins + 1)) + Random.Range(-22.5f, 22.5f);

            // Döndürme süresi içinde çarký döndür
            uiSpinBase.transform.DORotate(new Vector3(0, 0, randomizeFinishAngle), spinDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuart)
                .OnUpdate(OnSpinUpdate) // Döndürme sýrasýnda çaðrýlacak fonksiyon
                .OnComplete(OnSpinComplete); // Döndürme tamamlandýðýnda çaðrýlacak fonksiyon
            LevelManager.Instance.eventManager.OnDuringSpin();
        }
    }

    // Çark dönerken çaðrýlacak fonksiyon
    private void OnSpinUpdate()
    {
        float currentRotation = uiSpinBase.transform.eulerAngles.z;
        


        if (Mathf.Abs(currentRotation % 45) < 1) // Yeterince yakýn bir deðer
        {
            PlaySegmentAnimation();
        }
    }

    // Çark döndüðünde çaðrýlacak fonksiyon
    private void OnSpinComplete()
    {
        Debug.Log("Spin complete!");
        LevelManager.Instance.eventManager.OnFinishSpin();
        uiSpinBase.transform.DORotate(new Vector3(0, 0, _targetRotation), (spinDuration / 4)).SetEase(Ease.InOutSine).OnComplete(() => LevelManager.Instance.eventManager.OnAfterSpin());
    }

    private void PlaySegmentAnimation()
    {

        if (_indicatorTween != null)
        {
            _indicatorTween.Kill();
            uiSpinIndicator.transform.eulerAngles = Vector3.zero;
        }
        float indicatorAnimAngle = -22.5f;
        _indicatorTween = uiSpinIndicator.transform.DORotate(new Vector3(0, 0, indicatorAnimAngle), 0.1f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => uiSpinIndicator.transform.DORotate(Vector3.zero, 0.1f));
        Debug.Log("Segment animation played!");
        // Buraya segment animasyonunu ekleyin
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpinWheel();
        }
    }
}
