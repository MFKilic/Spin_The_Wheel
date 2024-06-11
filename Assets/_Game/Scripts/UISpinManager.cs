using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpinManager : MonoBehaviour
{
    private const string uiSpinBaseName = "ui_spin_base";
    private const string uiSpinIndicatorName = "ui_spin_indicator";
    private const string uiSpinIconName = "ui_spin_icon";
    public Image uiSpinBase;
    public Image uiSpinIndicator;
    public UISpinController[] uiSpinControllers; 
    // Start is called before the first frame update
    private void OnValidate()
    {
        if (uiSpinBase == null || uiSpinIndicator == null)
        {
            Transform[] spinTransforms = GetComponentsInChildren<Transform>(true);
            Debug.Log(spinTransforms.Length);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
