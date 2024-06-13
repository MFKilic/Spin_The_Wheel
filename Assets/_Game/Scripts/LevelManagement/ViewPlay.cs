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
        private const string uiCardPanelZoneStr = "ui_card_panel_zone_bg";
        private const string uiCardPanelMapFrameStr = "ui_card_zone_map_frame";
        private const string uiCardStr = "ui_card";

        private const string uiCardPanelStr = "ui_card_panel";
        private const string uiCardPanelPanelMaskStr = "ui_card_panel_mask_image";
        private const string uiCardPanelPanelVerticalLayoutStr = "ui_card_panel_vertical_layout";

        public UISpinManager spinManager = null;
        public UIPrizeManager prizeManager= null;
        public UICardInfoController cardInfoController = null;

        public Image uiCardPanelMapFrame;

        [SerializeField] private TextMeshProUGUI textLevel;

       

        

        private void OnValidate()
        {

            if(cardInfoController == null)
            {
                Transform[] children = GetComponentsInChildren<Transform>(true);
                foreach (Transform child in children)
                {
                    if (cardInfoController == null)
                    {
                        if (child.name == uiCardStr)
                        {
                            cardInfoController = child.GetComponent<UICardInfoController>();
                        }
                        if (cardInfoController != null)
                        {
                            break;
                        }
                    }

                }
            }

            if(uiCardPanelMapFrame == null)
            {
                Transform[] children = GetComponentsInChildren<Transform>(true);

                foreach (Transform child in children)
                {
                    if (uiCardPanelMapFrame == null)
                    {
                        if (child.name == uiCardPanelZoneStr)
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

            if (prizeManager == null)
            {
                Transform[] children = GetComponentsInChildren<Transform>(true);
                foreach (Transform child in children)
                {
                    if (prizeManager == null)
                    {
                        if (child.name == uiCardPanelStr)
                        {
                            Transform[] uiCardPanel = child.GetComponentsInChildren<Transform>(true);
                            foreach (Transform uiCardPanelChild in uiCardPanel)
                            {
                                if (uiCardPanelChild.name == uiCardPanelPanelMaskStr)
                                {

                                    Transform[] uiCardPanelPanelMask = uiCardPanelChild.GetComponentsInChildren<Transform>(true);
                                    foreach (Transform uiCardPanelPanelMaskChild in uiCardPanelPanelMask)
                                    {
                                        if (uiCardPanelPanelMaskChild.name == uiCardPanelPanelVerticalLayoutStr)
                                        {
                                            prizeManager = uiCardPanelPanelMaskChild.GetComponent<UIPrizeManager>();
                                        }
                                    }
                                }
                            }

                            
                        }
                        if (prizeManager != null)
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
        public void ViewPlayStart()
        {
            Debug.Log("PlayStart");
            if (textLevel != null)
                textLevel.text = "LEVEL " + LevelManager.Instance.datas.level;

            LevelManager.Instance.eventManager.OnInitSpin();
        }

        public void SetInfoCard(Sprite sprite,int number)
        {
            cardInfoController.SetInfoCard(sprite,number);
        }
       
    }
}
