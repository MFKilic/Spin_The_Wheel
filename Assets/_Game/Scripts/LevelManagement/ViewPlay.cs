using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace TemplateFx
{
    public class ViewPlay : MonoBehaviour
    {
        private const string uiCardSpinStr = "ui_spin";
        private const string uiCardPanelZoneStr = "ui_card_panel_zone_bg";
        private const string uiCardPanelMapFrameStr = "ui_card_zone_map_frame";
        private const string uiCardStr = "ui_card";
        private const string uiCardInfoPanelStr = "ui_card_info_panel";
        private const string uiCardPanelPanelMaskStr = "ui_card_info_panel_mask_image";
        private const string uiCardPanelPanelVerticalLayoutStr = "ui_card_info_panel_vertical_layout";
        private const string uiWinCardStr = "ui_win_card";

        public UISpinManager spinManager = null;
        public UIPrizeManager prizeManager = null;
        public UICardInfoController cardInfoController = null;
        public Image uiCardPanelMapFrame;
        [SerializeField] private Image uiWinCard;

        private void OnValidate()
        {
            InitializeComponent(ref uiWinCard, uiWinCardStr);
            InitializeComponent(ref cardInfoController, uiCardStr);
            InitializeComponent(ref uiCardPanelMapFrame, uiCardPanelMapFrameStr, uiCardPanelZoneStr);
            InitializeComponent(ref spinManager, uiCardSpinStr);
            InitializePrizeManager();
        }

        private void InitializeComponent<T>(ref T component, string componentName, string parentName = null) where T : Component
        {
            if (component == null)
            {
                Transform[] children = GetComponentsInChildren<Transform>(true);
                foreach (Transform child in children)
                {
                    if (parentName == null || child.name == parentName)
                    {
                        Transform[] targetChildren = parentName == null ? children : child.GetComponentsInChildren<Transform>(true);
                        foreach (Transform targetChild in targetChildren)
                        {
                            if (targetChild.name == componentName)
                            {
                                component = targetChild.GetComponent<T>();
                                if (component != null) break;
                            }
                        }
                        if (component != null) break;
                    }
                }
            }
        }

        private void InitializePrizeManager()
        {
            if (prizeManager == null)
            {
                Transform[] children = GetComponentsInChildren<Transform>(true);
                foreach (Transform child in children)
                {
                    if (child.name == uiCardInfoPanelStr)
                    {
                        Transform[] uiCardPanelChildren = child.GetComponentsInChildren<Transform>(true);
                        foreach (Transform uiCardPanelChild in uiCardPanelChildren)
                        {
                            if (uiCardPanelChild.name == uiCardPanelPanelMaskStr)
                            {
                                Transform[] maskChildren = uiCardPanelChild.GetComponentsInChildren<Transform>(true);
                                foreach (Transform maskChild in maskChildren)
                                {
                                    if (maskChild.name == uiCardPanelPanelVerticalLayoutStr)
                                    {
                                        prizeManager = maskChild.GetComponent<UIPrizeManager>();
                                        if (prizeManager != null) break;
                                    }
                                }
                                if (prizeManager != null) break;
                            }
                        }
                        if (prizeManager != null) break;
                    }
                }
            }
        }

        public void Win()
        {
            uiWinCard.gameObject.SetActive(true);
            uiWinCard.transform.localScale = Vector3.zero;
            uiWinCard.transform.DOScale(Vector3.one, 0.5f);
        }

        public void ViewPlayStart()
        {
            Debug.Log("PlayStart");
            uiWinCard.gameObject.SetActive(false);
            LevelManager.Instance.eventManager.OnInitSpin();
        }

        public void SetInfoCard(Sprite sprite, int number, string name,Vector2 spriteSize)
        {
            cardInfoController.SetInfoCard(sprite, number, name,spriteSize);
        }
    }
}
