using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeginPopupController : UIController
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private List<MissionUIController> _missionUiControllers;

    [SerializeField] private GameObject _missionsContainer;
    [SerializeField] private GameObject _bucketContainer;

    private TouchManager _touchManager;

    public override void OnAppeared()
    {
        bool isBucketLevel = FindObjectOfType<BucketManager>();
        
        _missionsContainer.SetActive(!isBucketLevel);
        _bucketContainer.SetActive(isBucketLevel);
        var levelData = ServicesManager.Instance.CurrentLevel();
        ServicesManager.Instance.MissionsManager.InitUI(_missionUiControllers);
        _text.text = levelData.Title;
        
        _touchManager = FindObjectOfType<TouchManager>();
        _touchManager.InputActive = false;
    }

    public void OnAccept()
    {
        ServicesManager.Instance.UIStackController.Pop(this);
    }

    public override void OnDisappeared()
    {
        _touchManager.InputActive = true;
    }
}