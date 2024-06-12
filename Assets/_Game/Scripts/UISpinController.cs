using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISpinController : MonoBehaviour
{
    private const string uiSpinIconMask = "ui_spin_icon_image_mask";
    private const string uiSpinIcon = "ui_spin_icon_image";
    private const string uiSpinText = "ui_spin_icon_text";
    public Image spinPartImage;
    public TextMeshProUGUI spinPartText;


    private void OnValidate()
    {
        if (spinPartImage == null || spinPartText == null)
        {
            Transform[] spinTransforms = GetComponentsInChildren<Transform>(true);
            foreach (Transform spin in spinTransforms)
            {
                if (spin.name == uiSpinIconMask)
                {
                    Transform[] spinMaskTransforms = GetComponentsInChildren<Transform>(true);
                    foreach (Transform spinMask in spinMaskTransforms)
                    {
                        if (spinPartImage == null)
                        {
                            if (spinMask.name == uiSpinIcon)
                            {
                                spinPartImage = spinMask.GetComponent<Image>();
                            }
                        }
                        if (spinPartText == null)
                        {
                            if (spinMask.name == uiSpinText)
                            {
                                spinPartText = spinMask.GetComponent<TextMeshProUGUI>();
                            }

                        }
                    }
                }

            }
        }
    }
   
    
}
