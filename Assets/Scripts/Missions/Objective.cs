using System;
using UnityEngine;

public class Objective : Collisionable
{
    [SerializeField] private MissionsManager.ObjectiveCompletion _completion;

    public Action<bool, Objective> OnObjectiveUpdated;
    
    public virtual MissionsManager.ObjectiveType Type
    {
        get { return MissionsManager.ObjectiveType.NONE; }
    }
    
    public MissionsManager.ObjectiveCompletion Completion
    {
        get { return _completion; }
    }
}