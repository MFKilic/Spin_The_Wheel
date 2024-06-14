using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;

namespace TemplateFx
{
    public class Datas : MonoBehaviour
    {
        private List<UIPrizeController> prizeControllerList = new List<UIPrizeController>();

        public LevelConfig config;
        
        public int level;
       
        
        private void OnEnable()
        {
            GameState.Instance.OnPrepareNewGameEvent += OnPrepareNewGameEvent;
            GameState.Instance.OnFinishGameEvent += OnFinishGameEvent;
            LevelManager.Instance.eventManager.OnSpinIsSuccesfulEvent += EventManager_OnSpinIsSuccesfulEvent;
        }

        private void EventManager_OnSpinIsSuccesfulEvent(bool isYes)
        {
            if (isYes)
            {

                LevelManager.Instance.eventManager.OnNewSpinPrepare();
                level++;
                Debug.Log("Level = " + level + "ConfigLengt = " + config.wheelConfigs.Count);
                
            }
        }

        private void OnDisable()
        {
            GameState.Instance.OnPrepareNewGameEvent -= OnPrepareNewGameEvent;
            GameState.Instance.OnFinishGameEvent -= OnFinishGameEvent;
            LevelManager.Instance.eventManager.OnSpinIsSuccesfulEvent -= EventManager_OnSpinIsSuccesfulEvent;
        }

        private void Awake()
        {
            SetLevel(1);

        }

        private void OnValidate()
        {
            if (config == null)
            {
                config = Resources.Load<LevelConfig>("NewLevelConfig");
            }
            
        }

        private void OnPrepareNewGameEvent()
        {
            
        }

        public void SetLevel(int number)
        {
            level = number;
        }

        public void CopyPrizeList(List<UIPrizeController> uIPrizeControllers)
        {
            prizeControllerList = uIPrizeControllers;
        }

        public List<UIPrizeController> GetPrizeList()
        {
            return prizeControllerList;
        }

        private void OnFinishGameEvent(LevelFinishStatus levelFinishStatus)
        {
            if (levelFinishStatus == LevelFinishStatus.WIN)
            {
               
            }
            else
            {
                ///
            }
        }


    }
}
