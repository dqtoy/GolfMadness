using System;
using UnityEngine;

public class Objective : Collisionable
{
    [SerializeField] private string MissionId;
    [SerializeField] private MissionsManager.ObjectiveCompletion _completion;

    public Action<bool, Objective> OnObjectiveUpdated;

    public bool IsInCurrentMission()
    {
        bool objectiveInCurrentMission = ServicesManager.Instance.CurrentLevel().LevelId == MissionId;
        enabled = objectiveInCurrentMission;
        return objectiveInCurrentMission;
    }

    public virtual MissionsManager.ObjectiveType Type
    {
        get { return MissionsManager.ObjectiveType.NONE; }
    }
    
    public MissionsManager.ObjectiveCompletion Completion
    {
        get { return _completion; }
    }
}