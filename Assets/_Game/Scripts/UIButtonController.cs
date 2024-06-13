using System.Collections;
using System.Collections.Generic;
using TemplateFx;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour,IClickable
{
    [SerializeField] private Image buttonImage;
    public void OnClick()
    {
        UIManager.Instance.viewPlay.spinManager.SpinWheel();
        buttonImage.color = Color.gray;
        buttonImage.raycastTarget = false;
        Debug.Log("CLICKED");
    }

    private void OnEnable()
    {
        LevelManager.Instance.eventManager.OnPreSpinEvent += EventManager_OnPreSpinEvent;
    }

    private void EventManager_OnPreSpinEvent()
    {
        buttonImage.color = Color.white;
        buttonImage.raycastTarget = true;
    }

    private void OnDisable()
    {
        LevelManager.Instance.eventManager.OnPreSpinEvent -= EventManager_OnPreSpinEvent;
    }

    private void OnValidate()
    {
        if(buttonImage == null)
        {
            buttonImage = GetComponent<Image>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
