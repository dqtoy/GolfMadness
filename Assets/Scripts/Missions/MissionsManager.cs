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
    private Dictionary<ObjectiveCompletion, MissionUIController> _uiControllers;

    private LevelData _levelData;

    public void InitLevelObjectives()
    {
        _levelData = ServicesManager.Instance.CurrentLevel();
        _uiControllers = new Dictionary<ObjectiveCompletion, MissionUIController>();
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

            if (objective.Type == ObjectiveType.NONE)
            {
                Debug.Log("OBJECT WITHOUT VALID OBJECTIVE :  " + objective.name, objective.gameObject);
                continue;
            }

            objective.OnObjectiveUpdated = OnObjectiveUpdated;
            _dicObjectives[objective.Completion].Add(objective);
        }
    }

    void OnObjectiveUpdated(bool completed, Objective objective)
    {
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
                LevelPassed();
            }
        }
        else
        {
            Debug.Log("NOTHING HAPPENS: " + objective.Type.ToString() + "   " + objective.Completion.ToString());
        }
        
        TryUpdateUI(objective.Completion, listObjectives.Count == 0);
    }

    void LevelPassed()
    {
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
            _uiControllers[obj].UpdateState(completed);
        }
    }

    public void InitUI(List<MissionUIController> listUIController)
    {
        _uiControllers.Add(ObjectiveCompletion.MAIN, listUIController[0]);  
        _uiControllers.Add(ObjectiveCompletion.SECONDARY_1, listUIController[1]);
        _uiControllers.Add(ObjectiveCompletion.SECONDARY_2, listUIController[2]);
        
        _uiControllers[ObjectiveCompletion.MAIN].Init(
            _dicObjectives[ObjectiveCompletion.MAIN].Count > 0,
            _levelData.MainMissionDescription);
        
        _uiControllers[ObjectiveCompletion.SECONDARY_1].Init(
            _dicObjectives[ObjectiveCompletion.SECONDARY_1].Count > 0,
            _levelData.MainMissionDescription);
        
        _uiControllers[ObjectiveCompletion.SECONDARY_2].Init(
            _dicObjectives[ObjectiveCompletion.SECONDARY_2].Count > 0,
            _levelData.MainMissionDescription);

    }
}