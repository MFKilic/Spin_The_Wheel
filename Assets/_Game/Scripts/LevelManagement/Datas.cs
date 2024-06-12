using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;

namespace TemplateFx
{
    public class Datas : MonoBehaviour
    {
        private const string levelString = "Level";

        public LevelConfig config;
        
        public int level;
       
        
        private void OnEnable()
        {
            GameState.Instance.OnPrepareNewGameEvent += OnPrepareNewGameEvent;
            GameState.Instance.OnFinishGameEvent += OnFinishGameEvent;
        }

        private void OnDisable()
        {
            GameState.Instance.OnPrepareNewGameEvent -= OnPrepareNewGameEvent;
            GameState.Instance.OnFinishGameEvent -= OnFinishGameEvent;
        }

        private void Awake()
        {
            level = PlayerPrefs.GetInt(levelString, 1);

        }

        private void OnValidate()
        {

        }

        private void OnPrepareNewGameEvent()
        {
            level = PlayerPrefs.GetInt(levelString, 1);
        }

        private void OnFinishGameEvent(LevelFinishStatus levelFinishStatus)
        {
            if (levelFinishStatus == LevelFinishStatus.WIN)
            {
                PlayerPrefs.SetInt(levelString, level + 1);
                level = PlayerPrefs.GetInt(levelString, 1);
            }
            else
            {
                ///
            }
        }


    }
}
