using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterObjective : Objective
{
    public override MissionsManager.ObjectiveType Type
    {
        get { return MissionsManager.ObjectiveType.ENTER; }
    }

    protected override void CollisionableTriggerEnter(Collider other)
    {
        if (Collider.isTrigger)
        {
            OnObjectiveUpdated(true, this);
        }
    }

    protected override void CollisionableCollisionEnter(Collision other)
    {
        if (!Collider.isTrigger)
        {
            OnObjectiveUpdated(true, this);
        }    }
}