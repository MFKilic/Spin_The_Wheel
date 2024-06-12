using System.Collections;
using System.Collections.Generic;
using TemplateFx;
using UnityEngine;

public class UIButtonController : MonoBehaviour,IClickable
{
    public void OnClick()
    {
        UIManager.Instance.viewPlay.spinManager.SpinWheel();
        Debug.Log("CLICKED");
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
