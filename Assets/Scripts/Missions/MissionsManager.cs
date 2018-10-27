using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionsManager : MonoBehaviour
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

    [SerializeField] private List<string> _missionsDescription;

    private Dictionary<ObjectiveCompletion, List<Objective>> _dicObjectives;

    //TEST PURPOSE
    private void Start()
    {
        Init();
    }


    public void Init()
    {
        _dicObjectives = new Dictionary<ObjectiveCompletion, List<Objective>>();
        _dicObjectives.Add(ObjectiveCompletion.MAIN, new List<Objective>());
        _dicObjectives.Add(ObjectiveCompletion.SECONDARY_1, new List<Objective>());
        _dicObjectives.Add(ObjectiveCompletion.SECONDARY_2, new List<Objective>());

        
        var objectivesInScene = FindObjectsOfType<Objective>();

        if (objectivesInScene.Length == 0)
        {
            Debug.Log("NO OBJECTIVES IN SCENE");
            enabled = false;
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
    }

    void LevelPassed()
    {
        string log = "LEVEL PASSED!\n";
        log += "\tMAIN OBJECTIVES REMAINING " + _dicObjectives[ObjectiveCompletion.MAIN].Count + "\n";
        log += "\tSECONDARY1 OBJECTIVES REMAINING " + _dicObjectives[ObjectiveCompletion.SECONDARY_1].Count + "\n";
        log += "\tSECONDARY2 OBJECTIVES REMAINING " + _dicObjectives[ObjectiveCompletion.SECONDARY_2].Count ;

        Debug.Log(log);
    }
}