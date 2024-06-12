using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TemplateFx
{
    public class ViewPlay : MonoBehaviour
    {
        private const string textLevelObjectName = "ui_level_text";
    
        private TextMeshProUGUI textLevel; 
   


        private void OnValidate()
        {
            
            if (textLevel == null)
            {
        
                Transform[] children = GetComponentsInChildren<Transform>(true);
                foreach (Transform child in children)
                {
                    if(textLevel == null)
                    {
                        if (child.name == textLevelObjectName)
                        {
                            textLevel = child.GetComponent<TextMeshProUGUI>();
                        }
                        if(textLevel != null)
                        {
                            break;
                        }
                    }
                  
                }
            }
        }

        // Start is called before the first frame update
        public void ViewPlayStart()
        {
            Debug.Log("PlayStart");
            if (textLevel != null)
                textLevel.text = "LEVEL " + LevelManager.Instance.datas.level;

            LevelManager.Instance.eventManager.OnInitSpin();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
