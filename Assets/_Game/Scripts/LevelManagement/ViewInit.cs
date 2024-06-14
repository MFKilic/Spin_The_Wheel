using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TemplateFx
{

    public class ViewInit : MonoBehaviour
    {
        // Start is called before the first frame update
        public void ViewInitStart(int number = 2)
        {
            gameObject.SetActive(false);
            LevelManager.Instance.datas.SetLevel(number-1);
           
            GameState.Instance.OnPrepareNewGame();
            if(number != 2)
            {
                LevelManager.Instance.eventManager.OnSkipLevel();
                LevelManager.Instance.datas.SetLevel(number);
            }
            UIManager.Instance.viewPlay.gameObject.SetActive(true);
            UIManager.Instance.viewPlay.ViewPlayStart();
        }

     
    }

}

