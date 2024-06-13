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

    private void OnValidate()
    {
        // Child objeleri arayýp TextMeshProUGUI bileþenlerini bul
        _levelTexts.Clear(); // Önceki listeyi temizle

        TextMeshProUGUI[] tmps = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI tmp in tmps)
        {
            _levelTexts.Add(tmp); // Listeye ekle
        }

        // Listeye eklenen her bir metni belirli bir formatla güncelle
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
    }

    private void EventManager_OnNewSpinPrepareEvent()
    {
        ChangeNumberPos();
    }

    private void OnDisable()
    {
        LevelManager.Instance.eventManager.OnNewSpinPrepareEvent -= EventManager_OnNewSpinPrepareEvent;
    }

    void Start()
    {
        _levelTexts[level - 1].color = Color.black;
        _levelTexts[level - 1].transform.DOScale(Vector3.one * 1.2f, 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeNumberPos();
        }
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
        transform.DOLocalMoveX(crossCount, 1).SetEase(Ease.OutCubic).OnComplete(()=> LevelManager.Instance.eventManager.OnInitSpin());
        
    }
}
