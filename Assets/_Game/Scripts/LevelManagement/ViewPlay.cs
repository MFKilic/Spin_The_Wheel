using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TemplateFx
{
    public class ViewPlay : MonoBehaviour
    {
        private const string textLevelObjectName = "ui_level_text";
        private const string spinObjectName = "ui_spin";
        private const string uiCardPanelStr = "ui_card_panel_zone_bg";
        private const string uiCardPanelMapFrameStr = "ui_card_zone_map_frame";

        public UISpinManager spinManager = null;

        public Image uiCardPanelMapFrame;
    
        private TextMeshProUGUI textLevel; 
   


        private void OnValidate()
        {

            if(uiCardPanelMapFrame == null)
            {
                Transform[] children = GetComponentsInChildren<Transform>(true);

                foreach (Transform child in children)
                {
                    if (uiCardPanelMapFrame == null)
                    {
                        if (child.name == uiCardPanelStr)
                        {
                            Transform[] uiCardPanelChildren = GetComponentsInChildren<Transform>(true);
                            foreach (Transform uiChild in uiCardPanelChildren)
                            {
                                if (uiChild.name == uiCardPanelMapFrameStr)
                                {
                                    uiCardPanelMapFrame = uiChild.GetComponent<Image>();
                                }
                                if (uiCardPanelMapFrame != null)
                                {
                                    break;
                                }
                            }
                           
                        }
                        if (uiCardPanelMapFrame != null)
                        {
                            break;
                        }
                    }

                }
            }

            if(spinManager == null)
            {
                Transform[] children = GetComponentsInChildren<Transform>(true);
                foreach (Transform child in children)
                {
                    if (spinManager == null)
                    {
                        if (child.name == spinObjectName)
                        {
                            spinManager = child.GetComponent<UISpinManager>();
                        }
                        if (spinManager != null)
                        {
                            break;
                        }
                    }

                }
            }
            
            if (textLevel == null)
            {
        
                Transform[] children = GetComponentsInChildren<Transform>(true);
                foreach (Transform child in children)
                {
                    if(textLevel == null)
                    {
                        if (child.name == textLevelObjectName)
                        {
                            textLevel = child.GetComponent<TextMeshProUGUI>();
                        }
                        if(textLevel != null)
                        {
                            break;
                        }
                    }
                  
                }
            }
        }

        // Start is called before the first frame update
        public void ViewPlayStart()
        {
            Debug.Log("PlayStart");
            if (textLevel != null)
                textLevel.text = "LEVEL " + LevelManager.Instance.datas.level;

            LevelManager.Instance.eventManager.OnInitSpin();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
