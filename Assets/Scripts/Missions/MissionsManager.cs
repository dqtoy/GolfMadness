using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionsManager
{
    public enum ObjectiveType
    {
        NONE,
        ENTER,
        DESTROY,
        MOVE
    }

    public enum ObjectiveCompletion
    {
        MAIN        = 0,
        SECONDARY_1 = 1,
        SECONDARY_2 = 3
    }
    
    private Dictionary<ObjectiveCompletion, List<Objective>> _dicObjectives;
    private Dictionary<ObjectiveCompletion, List< MissionUIController>> _uiControllers;

    private LevelData _levelData;

    public bool MissionsActive;

    
    public void InitLevelObjectives()
    {
        MissionsActive = true;
        _levelData = ServicesManager.Instance.CurrentLevel();
        _uiControllers = new Dictionary<ObjectiveCompletion, List<MissionUIController>>();
        _dicObjectives = new Dictionary<ObjectiveCompletion, List<Objective>>();
        _dicObjectives.Add(ObjectiveCompletion.MAIN, new List<Objective>());
        _dicObjectives.Add(ObjectiveCompletion.SECONDARY_1, new List<Objective>());
        _dicObjectives.Add(ObjectiveCompletion.SECONDARY_2, new List<Objective>());
        
        var objectivesInScene = GameObject.FindObjectsOfType<Objective>();

        if (objectivesInScene.Length == 0)
        {
            Debug.Log("NO OBJECTIVES IN SCENE");
            return;
        }

        for (int i = 0; i < objectivesInScene.Length; i++)
        {
            var objective = objectivesInScene[i];

       /*     if (objective.Type == ObjectiveType.NONE)
            {
                Debug.Log("OBJECT WITHOUT VALID OBJECTIVE :  " + objective.name, objective.gameObject);
                continue;
            }*/

            if (!objective.IsInCurrentMission())
            {
                Debug.Log("Objective not in current mission ", objective);
                continue;
            }

            objective.OnObjectiveUpdated = OnObjectiveUpdated;
            _dicObjectives[objective.Completion].Add(objective);
        }
    }

    void OnObjectiveUpdated(bool completed, Objective objective)
    {
        if (!MissionsActive)
        {
            return;
        }

        var listObjectives = _dicObjectives[objective.Completion];
        bool objectiveInList = listObjectives.Contains(objective);
        
        if (!completed && !objectiveInList)
        {
            Debug.Log("OBJECTIVE GAINED AGAIN: " + objective.Type.ToString() + "   " + objective.Completion.ToString());
            listObjectives.Add(objective);
        }
        else if(completed && objectiveInList)
        {
            Debug.Log("OBJECTIVE COMPLETED: " + objective.Type.ToString() + "   " + objective.Completion.ToString());
            listObjectives.Remove(objective);
            if (objective.Completion == ObjectiveCompletion.MAIN && listObjectives.Count == 0)
            {
                ServicesManager.Instance.UIStackController.ThrowCoroutine(LevelPassedCorroutine());
            }
        }
        else
        {
            Debug.Log("NOTHING HAPPENS: " + objective.Type.ToString() + "   " + objective.Completion.ToString());
        }
        
        TryUpdateUI(objective.Completion, listObjectives.Count == 0);
    }

    public void ForceWin()
    {
        ServicesManager.Instance.UIStackController.ThrowCoroutine(LevelPassedCorroutine()); 
    }

    public void ForceLose()
    {
        SoundManager.Instance.PlayDefat();
        ServicesManager.Instance.UIStackController.Push("UIPrefabs/LosePopup");
    }
    
    private IEnumerator LevelPassedCorroutine()
    {
        yield return null;
        LevelPassed();
    }

    void LevelPassed()
    {
        SoundManager.Instance.PlayVictory();
        
        var completedList = new List<bool>()
        {
            _dicObjectives[ObjectiveCompletion.MAIN].Count == 0,
            _dicObjectives[ObjectiveCompletion.SECONDARY_1].Count == 0,
            _dicObjectives[ObjectiveCompletion.SECONDARY_2].Count == 0
        };
        ServicesManager.Instance.PlayerModel.SetLevelStars(completedList);
        ServicesManager.Instance.UIStackController.Push("UIPrefabs/CompletedLevelPopup");
        
        //TODO: remove 
        string log = "LEVEL PASSED!\n";
        log += "\tMAIN OBJECTIVES REMAINING " + _dicObjectives[ObjectiveCompletion.MAIN].Count + "\n";
        log += "\tSECONDARY1 OBJECTIVES REMAINING " + _dicObjectives[ObjectiveCompletion.SECONDARY_1].Count + "\n";
        log += "\tSECONDARY2 OBJECTIVES REMAINING " + _dicObjectives[ObjectiveCompletion.SECONDARY_2].Count ;

        Debug.Log(log);
    }

    void TryUpdateUI(ObjectiveCompletion obj, bool completed)
    {
        if (_uiControllers.ContainsKey(obj))
        {
            for (int i = 0; i < _uiControllers[obj].Count; i++)
            {
                if (_uiControllers[obj][i] != null)
                {
                    _uiControllers[obj][i].UpdateState(completed);
                }
            }
        }
    }

    public void InitUI(List<MissionUIController> listUIController)
    {
        if (!_uiControllers.ContainsKey(ObjectiveCompletion.MAIN))
        {
             
            _uiControllers.Add(ObjectiveCompletion.MAIN, new List<MissionUIController>());  
            _uiControllers.Add(ObjectiveCompletion.SECONDARY_1, new List<MissionUIController>());
            _uiControllers.Add(ObjectiveCompletion.SECONDARY_2, new List<MissionUIController>());
        }

        _uiControllers[ObjectiveCompletion.MAIN].Add(listUIController[0]);  
        _uiControllers[ObjectiveCompletion.SECONDARY_1].Add(listUIController[1]);  
        _uiControllers[ObjectiveCompletion.SECONDARY_2].Add(listUIController[2]);  
        
        listUIController[0].Init(
            _dicObjectives[ObjectiveCompletion.MAIN].Count > 0,
            _levelData.MainMissionDescription);
        
        listUIController[1].Init(
            _dicObjectives[ObjectiveCompletion.SECONDARY_1].Count > 0,
            _levelData.SecondaryMissionDescription);
        
        listUIController[2].Init(
            _dicObjectives[ObjectiveCompletion.SECONDARY_2].Count > 0,
            _levelData.ThirdMissionDescription);

    }

    private void OnDestroy()
    {
        MissionsActive = false;
    }
}