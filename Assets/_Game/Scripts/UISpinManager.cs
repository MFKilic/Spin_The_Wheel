using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DoTween kütüphanesini kullanmak için gerekli

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
            float targetRotation = 360f * Random.Range(minSpins, maxSpins + 1) + (randomSlice * 45);

            // Döndürme süresi içinde çarký döndür
            uiSpinBase.transform.DORotate(new Vector3(0, 0, -targetRotation), spinDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuart)
                .OnComplete(OnSpinComplete); // Döndürme tamamlandýðýnda çaðrýlacak fonksiyon
        }
    }

    // Çark döndüðünde çaðrýlacak fonksiyon
    private void OnSpinComplete()
    {
        Debug.Log("Spin complete!");
        // Buraya çark döndükten sonra yapýlacak iþlemleri ekleyin
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SpinWheel();
        }
    }
}
