using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TemplateFx
{
    public enum ZoneTypes
    {
        safe,super
    }
    public class ZoneController : MonoBehaviour
    {
        private const string zoneTextSuperStr = "ui_card_panel_zone_super_text";
        private const string zoneTextSafeStr = "ui_card_panel_zone_safe_text";
        [SerializeField] private ZoneTypes zoneType;
        [SerializeField] private TextMeshProUGUI zoneNumberText;

        private void OnValidate()
        {
            Transform[] childTranforms = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in childTranforms)
            {
                if(zoneType == ZoneTypes.safe)
                {
                    if (child.name == zoneTextSafeStr)
                    {
                        zoneNumberText = child.GetComponent<TextMeshProUGUI>();
                    }
                }
                else if(zoneType == ZoneTypes.super)
                {
                    if (child.name == zoneTextSuperStr)
                    {
                        zoneNumberText = child.GetComponent<TextMeshProUGUI>();
                    }
                }
                
            }
        }

        private void OnEnable()
        {
            LevelManager.Instance.eventManager.OnInitSpinEvent += EventManager_OnInitSpinEvent;
        }

        private void EventManager_OnInitSpinEvent()
        {
            int nextNumber = LevelManager.Instance.datas.CheckZone(zoneType);
            zoneNumberText.text = nextNumber.ToString();
        }

        private void OnDestroy()
        {
            LevelManager.Instance.eventManager.OnInitSpinEvent -= EventManager_OnInitSpinEvent;
        }
    }

}
