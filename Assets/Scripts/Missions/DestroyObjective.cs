using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjective : Objective
{
    public override MissionsManager.ObjectiveType Type
    {
        get { return MissionsManager.ObjectiveType.DESTROY; }
    }

    private void OnDestroy()
    {
        if (OnObjectiveUpdated != null)
        {
            OnObjectiveUpdated(true, this);
        }
    }
}