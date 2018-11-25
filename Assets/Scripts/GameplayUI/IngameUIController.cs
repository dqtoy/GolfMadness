using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIController : UIController
{
    [SerializeField] private List<MissionUIController> _missionUiControllers;
    [SerializeField] private TextMeshProUGUI _triesText;
    [SerializeField] private Image _powerMetter;
    [SerializeField] private GameObject _powerMetterObj;
    

    private TrajectoryLine _trajectoryLine;
    
    public override void OnAppeared()
    {
        ServicesManager.Instance.MissionsManager.InitUI(_missionUiControllers);
        InitTries();
        _trajectoryLine = FindObjectOfType<TrajectoryLine>();
        _trajectoryLine.SubscribeToOnPowerChanged(UpdatePower);

        FindObjectOfType<PlayerController>().OnBallMoves += OnBallMoving;
        UpdatePower(0f);
    }

    void InitTries()
    {
        var levelData = ServicesManager.Instance.CurrentLevel();
        ServicesManager.Instance.TriesManager.OnTriesUpdated += OnTryUpdate;
        ServicesManager.Instance.TriesManager.Init(levelData);
        
    }

    public void OnTryUpdate(int amount)
    {
        _triesText.text = amount.ToString();
    }

    public void UpdatePower(float newPowervalue)
    {
        _powerMetter.fillAmount = newPowervalue;
    }

    public void OnPressBack()
    {
        ServicesManager.Instance.UIStackController.Push("UIPrefabs/PausePopup");
    }
    
    public void OnBallMoving(bool moving)
    {
       // _powerMetterObj.SetActive(!moving);
    }

    private void OnDestroy()
    {
        
        ServicesManager.Instance.TriesManager.OnTriesUpdated -= OnTryUpdate;
    }
}