using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClickManager : MonoBehaviour
{
    private Camera uiCamera; // UI ��elerini g�rmek i�in kullan�lan kamera


    private void OnValidate()
    {
        // UI ��elerini tespit etmek i�in kullan�lan kameray� belirleyin
        uiCamera = Camera.main; // UI ��elerini ana kameradan tespit ediyorsan�z, bunu kullanabilirsiniz.
    }
    private void Awake()
    {
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare tu�una bas�ld�ysa
        {
            RaycastAndClick();
        }
    }

    private void RaycastAndClick()
    {
        // UI i�in ray olu�tur
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            // IClickable aray�z�ne sahip bile�enleri kontrol et
            IClickable clickable = result.gameObject.GetComponent<IClickable>();
            if (clickable != null)
            {
                clickable.OnClick();
                break; // �lk buldu�unuza t�klay�n, gerekirse bu sat�r� kald�rabilirsiniz
            }
        }
    }
}
