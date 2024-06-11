using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DoTween k�t�phanesini kullanmak i�in gerekli

public class UISpinManager : MonoBehaviour
{
    private const string uiSpinBaseName = "ui_spin_base";
    private const string uiSpinIndicatorName = "ui_spin_indicator";
    private const string uiSpinIconName = "ui_spin_icon";
    public Image uiSpinBase;
    public Image uiSpinIndicator;
    public UISpinController[] uiSpinControllers;
    public float spinDuration = 3f; // Spin s�resi
    public int minSpins = 3; // Minimum tur say�s�
    public int maxSpins = 5; // Maksimum tur say�s�

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
                // Child'�n ad� "ui_spin_icon" ise, UISpinController bile�enini al�p listeye ekle
                if (child.name.Contains(uiSpinIconName))
                {
                    UISpinController controller = child.GetComponent<UISpinController>();
                    if (controller != null)
                    {
                        controllersList.Add(controller);
                    }
                }
            }
            // Listeyi diziye d�n��t�r
            uiSpinControllers = controllersList.ToArray();
        }

    }

    void Start()
    {

    }

    // �ark� d�nd�rme fonksiyonu
    public void SpinWheel()
    {
        if (uiSpinBase != null)
        {
            // Rastgele bir dilim se�
            int randomSlice = Random.Range(0, 8);
            // Bu dilimin ortas�nda duracak �ekilde dereceyi hesapla
            float targetRotation = 360f * Random.Range(minSpins, maxSpins + 1) + (randomSlice * 45);

            // D�nd�rme s�resi i�inde �ark� d�nd�r
            uiSpinBase.transform.DORotate(new Vector3(0, 0, -targetRotation), spinDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuart)
                .OnComplete(OnSpinComplete); // D�nd�rme tamamland���nda �a�r�lacak fonksiyon
        }
    }

    // �ark d�nd���nde �a�r�lacak fonksiyon
    private void OnSpinComplete()
    {
        Debug.Log("Spin complete!");
        // Buraya �ark d�nd�kten sonra yap�lacak i�lemleri ekleyin
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SpinWheel();
        }
    }
}
