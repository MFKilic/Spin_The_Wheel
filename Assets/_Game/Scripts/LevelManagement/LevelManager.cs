using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace TemplateFx
{
    [DefaultExecutionOrder(-1)]
    public class LevelManager : Singleton<LevelManager>
    {
        public EventManager eventManager;
        public Datas datas;

        private void OnValidate()
        {
            if (eventManager == null)
            {
                eventManager = gameObject.GetComponent<EventManager>();
               
            }

            if(datas == null)
            {
                datas = gameObject.GetComponent<Datas>();
            }

        }

        private void Start()
        {
            GameState.Instance.OnInitGame();
        }
    }

}

