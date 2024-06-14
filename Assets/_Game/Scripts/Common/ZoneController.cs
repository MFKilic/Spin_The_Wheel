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
        private const string zoneTextSuperStr = "ui_card_panel_zone_super_text_number";
        private const string zoneTextSafeStr = "ui_card_panel_zone_safe_text_number";
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
            GameState.Instance.OnPrepareNewGameEvent += Instance_OnPrepareNewGameEvent;
        }

        private void Instance_OnPrepareNewGameEvent()
        {
            gameObject.SetActive(true);
        }

        private void EventManager_OnInitSpinEvent()
        {
            int nextNumber = LevelManager.Instance.datas.CheckZone(zoneType);

            if(nextNumber == 0)
            {
                gameObject.SetActive(false);
            }
            
            zoneNumberText.text = nextNumber.ToString();
        }

        private void OnDestroy()
        {
            LevelManager.Instance.eventManager.OnInitSpinEvent -= EventManager_OnInitSpinEvent;
            GameState.Instance.OnPrepareNewGameEvent -= Instance_OnPrepareNewGameEvent;
        }
    }

}
