using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainSceneController : UIController
{
    private const float kSpaceCell = -136;

    [SerializeField] private MainSceneCellController _prefab;
    [SerializeField] private RectTransform _scrollContent;
    
    public override void OnAppeared()
    {
        var listScenes = ServicesManager.Instance.LevelDataCollection.LevelDataList;

        for (int i = 0; i < listScenes.Count; i++)
        {
            var obj = Instantiate(_prefab.gameObject);
            obj.transform.SetParent(_scrollContent.transform,false);
            obj.name = i.ToString();
           
            
            //TODO init with level data
        }
    }

    void OnPressCell(LevelData levelToLoad)
    {
        
    }
}