using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;
using Unity.VisualScripting;

namespace TemplateFx
{
    public class Datas : MonoBehaviour
    {
        private List<UIPrizeController> prizeControllerList = new List<UIPrizeController>();
        private List<int> _safeZoneNumbers = new List<int>();
        private List<int> _superZoneNumbers = new List<int>();

        public LevelConfig config;
        
        public int level;

        public bool isWin;
       
        
        private void OnEnable()
        {
            LevelManager.Instance.eventManager.OnSpinIsSuccesfulEvent += EventManager_OnSpinIsSuccesfulEvent;
        }

        private void EventManager_OnSpinIsSuccesfulEvent(bool isYes)
        {
            if (isYes)
            {
                Debug.Log("Level = " + level + "ConfigLengt = " + config.wheelConfigs.Count);
                if(level == config.wheelConfigs.Count)
                {
                    isWin = true;
                    
                    return;
                }
                LevelManager.Instance.eventManager.OnNewSpinPrepare();
                level++;
                
                
            }
        }

        private void OnDisable()
        {      
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

        public int CheckZone(ZoneTypes type)
        {
            if(type == ZoneTypes.safe)
            {
                foreach (int number in _safeZoneNumbers)
                {
                    if(level<number)
                    {
                        return number;
                    }
                }
            }
            else if(type == ZoneTypes.super)
            {
                foreach (int number in _superZoneNumbers)
                {
                    if (level < number)
                    {
                        return number;
                    }
                }
            }
            return 0;
        }

        public void CopyZoneList(List<int> safeZoneList,List<int> superZoneList)
        {
            _safeZoneNumbers = safeZoneList;
            _superZoneNumbers = superZoneList;
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

       


    }
}
