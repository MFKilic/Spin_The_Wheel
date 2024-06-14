using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISpinController : MonoBehaviour
{
    private const string uiSpinIconMask = "ui_spin_icon_image_mask";
    private const string uiSpinIcon = "ui_spin_icon_image";
    private const string uiSpinText = "ui_spin_icon_text";
    [SerializeField] private Image spinPartImage;
    [SerializeField] private TextMeshProUGUI spinPartText;
    private string spinPartName;


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

    public void SetNameString(string name)
    {
        spinPartName = name;
    }

    public string GetNameString()
    {
        return spinPartName;
    }

    public void SetImageSprite(Sprite sprite)
    {
        spinPartImage.sprite = sprite;
    }

    public Image GetImage()
    {
        return spinPartImage;
    }

    public void SetText(string text)
    {
      
        spinPartText.text = text;
        if(GetNumber() == 0)
        {
            spinPartText.text = string.Empty;
        }
    }

    public TextMeshProUGUI GetText()
    {
        return spinPartText;
    }

    public int GetNumber()
    {
        int number;
        if (int.TryParse(spinPartText.text, out number))
        {
            return number;
        }
        else
        {
            Debug.Log("Text is not a valid number");
            return 0; 
        }
    }
   
    
}
