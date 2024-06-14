using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using TemplateFx;

public class UILevelController : MonoBehaviour
{
   [SerializeField]  private List<TextMeshProUGUI> _levelTexts = new List<TextMeshProUGUI>();
    private int level = 1;
    private int singlePlusCount = -40;
    private int changePlusCount = -45;
    private int doublePlusCount = -50;
    private Vector3 startPos;
    bool isSpin;

    private void OnValidate()
    {
     
        _levelTexts.Clear(); 

        TextMeshProUGUI[] tmps = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI tmp in tmps)
        {
            _levelTexts.Add(tmp); 
        }

        TextColorChange();
      
    }

    private void TextColorChange()
    {
        for (int i = 0; i < _levelTexts.Count; i++)
        {

            _levelTexts[i].text = (i + 1).ToString();
            if ((i + 1) % 30 == 0)
            {
                _levelTexts[i].color = Color.yellow;
            }
            else if ((i + 1) % 5 == 0)
            {
                _levelTexts[i].color = Color.green;
            }
            else
            {
                _levelTexts[i].color = Color.white;
            }
        }
    }
    private void OnEnable()
    {
        LevelManager.Instance.eventManager.OnNewSpinPrepareEvent += EventManager_OnNewSpinPrepareEvent;
        GameState.Instance.OnPrepareNewGameEvent += Instance_OnPrepareNewGameEvent;
        LevelManager.Instance.eventManager.OnBombIsExplosedEvent += EventManager_OnBombIsExplosedEvent;
    }

    private void EventManager_OnBombIsExplosedEvent()
    {
        transform.DOPunchPosition(Vector3.one * 15, 0.2f, 0);
    }

    private void Instance_OnPrepareNewGameEvent()
    {
        TextColorChange();
        _levelTexts[0].color = Color.black;
        _levelTexts[0].transform.DOScale(Vector3.one * 1.2f, 1);
        transform.position = startPos;
    }

   
    private void EventManager_OnNewSpinPrepareEvent()
    {

       ChangeNumberPos();
    }

    private void OnDisable()
    {
        LevelManager.Instance.eventManager.OnNewSpinPrepareEvent -= EventManager_OnNewSpinPrepareEvent;
        GameState.Instance.OnPrepareNewGameEvent -= Instance_OnPrepareNewGameEvent;
        LevelManager.Instance.eventManager.OnBombIsExplosedEvent -= EventManager_OnBombIsExplosedEvent;
    }

    void Awake()
    {
        startPos = transform.position;
      
    }

   

    public void ChangeNumberPos()
    {
        level = LevelManager.Instance.datas.level;
        int crossCount = singlePlusCount;
        if (level < 9)
        {
            crossCount = singlePlusCount * level;
        }
        else if (level == 9)
        {
            crossCount = changePlusCount + (singlePlusCount * 8);
        }
        else
        {
            crossCount = (doublePlusCount * (level - 9)) + (changePlusCount + (singlePlusCount * 8));
        }
        _levelTexts[level - 1].transform.DOScale(Vector3.one, 1);
        Color oldTextColor = _levelTexts[level - 1].color;
        _levelTexts[level - 1].color = new Color(oldTextColor.r, oldTextColor.g, oldTextColor.b, 0.5f);

        _levelTexts[level].transform.DOScale(Vector3.one * 1.2f, 0.5f);

        if ((level+1) % 30 == 0)
        {
            UIManager.Instance.viewPlay.uiCardPanelMapFrame.color = Color.yellow;
            _levelTexts[level].color = Color.yellow;
        }
        else if ((level+1) % 5 == 0)
        {
            UIManager.Instance.viewPlay.uiCardPanelMapFrame.color = Color.green;
            _levelTexts[level].color = Color.green;
        }
        else
        {

            UIManager.Instance.viewPlay.uiCardPanelMapFrame.color = Color.white;
            _levelTexts[level].color = Color.black;
        }
        transform.DOLocalMoveX(crossCount, 0.5f).SetEase(Ease.OutCubic).OnComplete(()=> LevelManager.Instance.eventManager.OnInitSpin());
        
    }
}
