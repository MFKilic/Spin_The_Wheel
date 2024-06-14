using System.Collections;
using System.Collections.Generic;
using TemplateFx;
using UnityEditor;
using UnityEngine;

public class UIFinishCardManager : MonoBehaviour
{
    private const string prefabPath = "Assets/_Game/Prefabs/ui_card_end.prefab";
    [SerializeField] private GameObject finishCardPrefab;
    private List<UIFinisCardController> controllerList = new List<UIFinisCardController>();
    // Start is called before the first frame update

    private void OnValidate()
    {
#if UNITY_EDITOR
        finishCardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
#endif

    }
    
    public void CreateCards()
    {
        if(controllerList.Count > 0)
        {
            foreach(var controller in controllerList)
            {
                Debug.Log(controller.name);
                Destroy(controller.gameObject);
            }
            controllerList.Clear();
        }
        var list = LevelManager.Instance.datas.GetPrizeList();

        for(int i = 0; i < list.Count; i++)
        {
            var card = list[i];

            if(card.GetImage().sprite != null)
            {
                GameObject go = Instantiate(finishCardPrefab, transform);
                UIFinisCardController controller = go.GetComponent<UIFinisCardController>();

                controller.SetNameText(card.GetName());
                controller.SetImage(card.GetImage().sprite);
                controller.SetNumberText(card.GetText().text);

                controllerList.Add(controller);
            }

            

        }
    }
}
