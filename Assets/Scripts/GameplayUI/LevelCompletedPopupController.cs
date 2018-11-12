using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletedPopupController : UIController
{
    [SerializeField] private List<GameObject> _stars;
    
    public override void OnAppeared()
    {
        var completed = ServicesManager.Instance.PlayerModel.GetCurrentCompletedStarsFromLevel(ServicesManager.Instance.PlayerModel
            .LoadedLevelIndex);

        for (int i = 0; i < completed.Count; i++)
        {
            if (i < _stars.Count)
            {
                _stars[i].SetActive(completed[i]);
            }
        }
    }

    public void OnAccept()
    {
        ServicesManager.Instance.MissionsManager.MissionsActive = false;
        ServicesManager.Instance.UIStackController.PopAll();
        SceneManager.LoadScene(ServicesManager.MainScene);
    }
}