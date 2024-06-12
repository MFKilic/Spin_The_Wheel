using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClickManager : MonoBehaviour
{
    private Camera uiCamera; // UI öðelerini görmek için kullanýlan kamera


    private void OnValidate()
    {
        // UI öðelerini tespit etmek için kullanýlan kamerayý belirleyin
        uiCamera = Camera.main; // UI öðelerini ana kameradan tespit ediyorsanýz, bunu kullanabilirsiniz.
    }
    private void Awake()
    {
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare tuþuna basýldýysa
        {
            RaycastAndClick();
        }
    }

    private void RaycastAndClick()
    {
        // UI için ray oluþtur
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            // IClickable arayüzüne sahip bileþenleri kontrol et
            IClickable clickable = result.gameObject.GetComponent<IClickable>();
            if (clickable != null)
            {
                clickable.OnClick();
                break; // Ýlk bulduðunuza týklayýn, gerekirse bu satýrý kaldýrabilirsiniz
            }
        }
    }
}
