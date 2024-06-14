using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


namespace TemplateFx
{

    public class ViewFinish : MonoBehaviour
    {
        [SerializeField] private GameObject _winPanelObject;
        [SerializeField] private GameObject _losePanelObject;
        [SerializeField] private UIFinishCardManager _cardManager;
        private const string uiWinStr = "ui_win";
        private const string uiLoseStr = "ui_lose";
        private const string uiCardManagerStr = "ui_win_horizontal_layout";

        private void OnValidate()
        {
            if (_winPanelObject == null || _losePanelObject == null)
            {
                Transform[] childTranforms = GetComponentsInChildren<Transform>(true);
                foreach (Transform child in childTranforms)
                {

                    if (_winPanelObject == null)
                    {
                        if (child.name == uiWinStr)
                        {
                            _winPanelObject = child.gameObject;
                        }

                    }
                    if (_losePanelObject == null)
                    {
                        if (child.name == uiLoseStr)
                        {
                            _losePanelObject = child.gameObject;
                        }

                    }

                }
            }
            if(_cardManager == null && _winPanelObject != null)
            {
                Transform[] childTranforms = _winPanelObject.GetComponentsInChildren<Transform>(true);
                foreach (Transform child in childTranforms)
                {
                    if(_cardManager == null)
                    {
                        if(child.name == uiCardManagerStr)
                        {
                            _cardManager = child.GetComponent<UIFinishCardManager>();
                        }
                    }
                }
            }
        }

        public void ManuelStart()
        {
            if(GameState.Instance.GetLevelStatus() == LevelFinishStatus.WIN)
            {
                _winPanelObject.SetActive(true);
                _losePanelObject.SetActive(false);
                _winPanelObject.transform.localScale = new Vector3(1, 0, 1);
                _winPanelObject.transform.DOScaleY(1, 0.4f);
                _cardManager.CreateCards();
            }
            else
            {
                _winPanelObject.SetActive(false);
                _losePanelObject.SetActive(true);
                _losePanelObject.transform.localScale = new Vector3(1, 0, 1);
                _losePanelObject.transform.DOScaleY(1, 0.4f);
            }
        }
        

        
      
    }


}

