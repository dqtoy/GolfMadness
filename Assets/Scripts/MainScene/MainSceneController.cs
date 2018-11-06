using UnityEngine;
using UnityEngine.SceneManagement;

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
           
           obj.GetComponent<MainSceneCellController>().Init(listScenes[i], 
               ServicesManager.Instance.PlayerModel.GetCurrentCompletedStarsFromLevel(i), OnPressCell); 
        }
        
        _prefab.gameObject.SetActive(false);
    }

    void OnPressCell(LevelData levelToLoad)
    {
        if (!string.IsNullOrEmpty(levelToLoad.Scene))
        {
            SceneManager.LoadScene(levelToLoad.Scene);
        }
        
        ServicesManager.Instance.UIStackController.Pop(this);
    }
}