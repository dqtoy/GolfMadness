using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrespassObjective : Objective
{
    public override MissionsManager.ObjectiveType Type
    {
        get { return MissionsManager.ObjectiveType.ENTER; }
    }

    protected override void CollisionableTriggerExit(Collider other)
    {
        var elementalPlayer = other.gameObject.GetComponent<PlayerController>();
        if (elementalPlayer != null)
        {
            OnObjectiveUpdated(true, this);
        }
    }
}